using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using WisdomLight.Model;
using WisdomLight.Model.Results.Confirming;
using WisdomLight.View;
using WisdomLight.ViewModel.Components.Building.Bank;
using WisdomLight.ViewModel.Components.Building.Components.Filler.Tabs;
using WisdomLight.ViewModel.Components.Building.Filler.Templates;
using WisdomLight.ViewModel.Components.Core.Commands;
using WisdomLight.ViewModel.Components.Core.Dialogs;
using WisdomLight.ViewModel.Components.Core.Processors.Export.Documents;
using WisdomLight.ViewModel.Components.Core.Processors.Serialization.Objects;
using WisdomLight.ViewModel.Components.Data;
using WisdomLight.ViewModel.Components.Data.Units;
using WisdomLight.ViewModel.Components.Data.Units.Fields;
using WisdomLight.ViewModel.Components.Data.Units.Fields.Tools;
using ExportOptions = WisdomLight.ViewModel.Components.Building.Bank.Export;

namespace WisdomLight.ViewModel.Components.Building.Filler
{
    public class FillerBuilder : IFillerBuilder
    {
        #region Classic
        private const string QueryPlaceHolder = "Выберите зависимость";
        private readonly StringBuilder _path;

        private IWindowService _windows;
        private IDialogService<DependenciesViewModel> _dependenciesDialog;

        private FileViewModel _viewModel;
        private TemplateViewModel _data;
        private List<FileDocument> _exporters;
        private DependenciesViewModel _dependencies;

        private ICommand _newCommand;
        private ICommand _openCommand;
        private ICommand _closeCommand;
        private bool _canClose;

        private ICommand _saveCommand;
        private ICommand _saveAsCommand;
        private ICommand _exportCommand;

        public ICommand _addInformation;
        public ICommand _dropInformation;

        public ICommand _addLink;
        public ICommand _dropLink;
        public ICommand _openLink;

        public ICommand _addQuery;
        public ICommand _dropQuery;
        public ICommand _openQuery;

        private ITemplateBuilder _template;

        public FillerBuilder(IWindowService windows,
            IDialogService<DependenciesViewModel> dependencies)
        {
            _path = new StringBuilder();
            _windows = windows;
            _dependenciesDialog = dependencies;
            _template = new TemplateBuilder();
        }

        public IFillerBuilder OpenLink()
        {
            _openLink = new RelayCommand(
                argument =>
                {
                    ReConfirmer confirmer = DialogManager.Template(Defaults.Runtime);
                    if (!confirmer.Result)
                        return;

                    for (int i = 0; i < _viewModel.Data.Links.SelectedItems.Count; i++)
                    {
                        _viewModel.Data.Links.SelectedItems[i].Set(confirmer);
                    }
                }
            );
            return this;
        }

        public IFillerBuilder SetDependencies(DependenciesViewModel dependencies)
        {
            _dependencies = dependencies;
            _template.SetDependencies(dependencies);
            return this;
        }

        public IFillerBuilder OpenQuery()
        {
            _openQuery = new RelayCommand(argument =>
                _dependenciesDialog.ShowDialog(_dependencies, (result, selection) =>
                {
                    if (!result)
                        return;
                    if (!selection.SelectedDependency.IsDependency)
                        return;

                    QueryProcessor(selection);
                    _path.Clear();
                })
            );
            return this;
        }

        private void QueryProcessor(DependenciesViewModel viewModel)
        {
            for (int i = 0; i < _data.Queriers.SelectedItems.Count; i++)
            {
                _data.Queriers.SelectedItems[i].Clear();
            }

            DependenciesNode current = viewModel.SelectedDependency;
            _path.Append(current.Name);
            for (int i = 0; i < _data.Queriers.SelectedItems.Count; i++)
            {
                _data.Queriers.SelectedItems[i].Push(current.No);
            }

            current = current.Parent;
            while (current != null)
            {
                _path.Insert(0, $"{current.Name}/");
                for (int i = 0; i < _data.Queriers.SelectedItems.Count; i++)
                {
                    _data.Queriers.SelectedItems[i].Push(current.No);
                }
                current = current.Parent;
            }

            for (int i = 0; i < _data.Queriers.SelectedItems.Count; i++)
            {
                _data.Queriers.SelectedItems[i].Name = _path.ToString();
            }
        }

        private IFillerBuilder ViewModel()
        {
            return Reset().NewFile().Open().Save().SaveAs().Export().CanClose().Close().Add().Drop().OpenLink();
        }

        public IFillerBuilder Add()
        {
            _addInformation = new RelayCommand(argument => _viewModel.Data.Information.Add(InformationAdditor()));
            _addLink = new RelayCommand(argument => _viewModel.Data.Links.Add(new DocumentLinker(string.Empty)));
            _addQuery = new RelayCommand(argument => _viewModel.Data.Queriers.Add(new Querier(QueryPlaceHolder)));
            return this;
        }

        public IFillerBuilder Drop()
        {
            _dropInformation = new RelayCommand(argument => _viewModel.Data.Information.RemoveSelected());
            _dropLink = new RelayCommand(argument => _viewModel.Data.Links.RemoveSelected());
            _dropQuery = new RelayCommand(argument => _viewModel.Data.Queriers.RemoveSelected());
            return this;
        }

        public IFillerBuilder Template()
        {
            _data = _template.Links().Queriers().Information().Serializer().Relate().Defend().Extracting().Build();
            return this;
        }

        #region Window
        public IFillerBuilder NewFile()
        {
            _newCommand = new RelayCommand(
                argument =>
                {
                    string location = _viewModel.Data.Location;

                    _viewModel = ViewModel().Template().Build();
                    _viewModel.Data.Name = "Новый документ";
                    _viewModel.Data.Location = location;

                    _windows.ShowWindow(_viewModel);
                }
            );
            return this;
        }

        public IFillerBuilder Open()
        {
            _openCommand = new RelayCommand(
                argument =>
                {
                    FileFiller serializer = _viewModel.Data.Serializer;
                    ReConfirmer dialog = DialogManager.Open(_viewModel.Data.Location, serializer.Current);
                    if (!dialog.Result)
                        return;

                    serializer.Current = dialog.Key;

                    FileViewModel viewModel = ViewModel().Build();

                    viewModel.Data = serializer.Load(dialog.FullPath);
                    viewModel.Data.Location = dialog.Path;
                    viewModel.Data.FileName = dialog.Name;

                    _windows.ShowWindow(viewModel);
                }
            );
            return this;
        }

        public IFillerBuilder Close()
        {
            _closeCommand = new RelayCommand(argument => _viewModel.Close?.Invoke(), can => _viewModel.CanClose);
            return this;
        }

        public IFillerBuilder CanClose()
        {
            _canClose = true;
            return this;
        }
        #endregion

        #region Files
        public IFillerBuilder Save()
        {
            _saveCommand = new RelayCommand(
                argument =>
                {
                    TemplateViewModel template = _viewModel.Data;
                    string name = string.IsNullOrEmpty(template.FileName) ? template.Name : template.FileName;
                    string path = $"{template.Location}\\{name}";
                    template.Serializer.FixedSave(path, template);
                },
                can => !string.IsNullOrEmpty(_viewModel.Data.Name)
            );
            return this;
        }

        public IFillerBuilder SaveAs()
        {
            _saveAsCommand = new RelayCommand(argument => CallSaveDialog());
            return this;
        }

        public IFillerBuilder Export()
        {
            _exporters = ExportOptions.Exporters();
            _exportCommand = new RelayCommand(
                argument =>
                {
                    Confirmer export = DialogManager.Export();
                    if (!export.Result)
                        return;

                    for (byte i = 0; i < _viewModel.Exporters.Count; i++)
                    {
                        _viewModel.Exporters[i].Extract(_viewModel.Data.Extracting);
                        _viewModel.Exporters[i].Export(_viewModel.Data.Documents,
                            _viewModel.Data.Information.Fields, export.Path);
                    }
                }
            );
            return this;
        }

        private void CallSaveDialog()
        {
            KeyConfirmer dialog = DialogManager.Save(_viewModel.Data.Location, _viewModel.Data.Name, _viewModel.Data.Serializer.Current);
            if (!dialog.Result)
                return;

            _viewModel.Data.Serializer.Current = dialog.Key;
            _viewModel.Data.Serializer.Save(dialog.Path, _viewModel.Data);
            _viewModel.Data.Location = Path.GetDirectoryName(dialog.Path);
            _viewModel.Data.FileName = Path.GetFileName(dialog.Path);
        }
        #endregion

        private FieldSelector InformationAdditor()
        {
            TextExpression current = new TextExpression() { Type = "Текст" };
            return new FieldSelector(
                new List<IExpression>
                {
                    current,
                    new NumberExpression() { Type = "Число" },
                    new DateExpression() { Type = "Дата" }
                }
            )
            {
                Selected = 0
            };
        }

        #region Building
        public IFillerBuilder Reset()
        {
            _canClose = false;
            _data = null;
            _viewModel = null;
            _newCommand = null;
            _openCommand = null;
            _saveCommand = null;
            _saveAsCommand = null;
            _exportCommand = null;
            _closeCommand = null;
            _addInformation = null;
            _dropInformation = null;
            _addLink = null;
            _dropLink = null;
            _openLink = null;
            _addQuery = null;
            _dropQuery = null;
            _openQuery = null;
            _template.Reset();
            return this;
        }

        public FileViewModel Build()
        {
            _viewModel = new FileViewModel
            {
                Data = _data,
                Exporters = _exporters,
                NewCommand = _newCommand,
                OpenCommand = _openCommand,
                SaveCommand = _saveCommand,
                SaveAsCommand = _saveAsCommand,
                ExportCommand = _exportCommand,
                CloseCommand = _closeCommand,
                CanClose = _canClose,
                AddInformation = _addInformation,
                DropInformation = _dropInformation,
                AddLink = _addLink,
                DropLink = _dropLink,
                OpenLink = _openLink,
                AddQuery = _addQuery,
                DropQuery = _dropQuery,
                OpenQuery = _openQuery
            };
            return _viewModel;
        }
        #endregion

        #endregion
    }
}
