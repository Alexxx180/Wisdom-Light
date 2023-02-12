using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using WisdomLight.Model;
using WisdomLight.View;
using WisdomLight.ViewModel.Components.Core.Commands;
using WisdomLight.ViewModel.Components.Core.Dialogs;
using WisdomLight.ViewModel.Components.Core.Processors.Export.Documents;
using WisdomLight.ViewModel.Components.Data;
using ExportOptions = WisdomLight.ViewModel.Components.Building.Bank.Export;

namespace WisdomLight.ViewModel.Components.Building.Components.Filler.Blocks
{
    public abstract class FillerFile : FillerWindow
    {
        private List<FileDocument> _exporters;

        private ICommand _saveCommand;
        private ICommand _saveAsCommand;
        private ICommand _exportCommand;

        public FillerFile(IWindowService window) : base(window) { }

        public override FillerBuilder2 Save()
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

        public override FillerBuilder2 SaveAs()
        {
            _saveAsCommand = new RelayCommand(argument =>
            {
                KeyConfirmer dialog = DialogManager.Save(_viewModel.Data.Location, _viewModel.Data.Name, _viewModel.Data.Serializer.Current);
                if (!dialog.Result)
                    return;

                _viewModel.Data.Serializer.Current = dialog.Key;
                _viewModel.Data.Serializer.Save(dialog.Path, _viewModel.Data);
                _viewModel.Data.Location = Path.GetDirectoryName(dialog.Path);
                _viewModel.Data.FileName = Path.GetFileName(dialog.Path);
            });
            return this;
        }

        public override FillerBuilder2 Export()
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
                        _viewModel.Exporters[i].Export(_viewModel.Data.Links.Fields,
                            _viewModel.Data.Information.Fields, export.Path);
                    }
                }
            );
            return this;
        }

        private protected override FillerBuilder2 ViewModel()
        {
            _ = base.ViewModel();
            return Save().SaveAs().Export();
        }

        public override FillerBuilder2 Template()
        {
            _ = base.Template();
            return this;
        }

        public override FillerBuilder2 Reset()
        {
            _ = base.Reset();
            _saveCommand = null;
            _saveAsCommand = null;
            _exportCommand = null;
            _exporters = null;
            return this;
        }

        public override FileViewModel Build()
        {
            FileViewModel viewModel = base.Build();
            viewModel.SaveCommand = _saveCommand;
            viewModel.SaveAsCommand = _saveAsCommand;
            viewModel.ExportCommand = _exportCommand;
            viewModel.Exporters = _exporters;
            return viewModel;
        }
    }
}
