using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WisdomLight.Model;
using WisdomLight.ViewModel.Commands;
using WisdomLight.ViewModel.Data.Collections;
using WisdomLight.ViewModel.Data.Files.Fields.Tools.Building.Filler.Collections;
using WisdomLight.ViewModel.Files.Fields;

namespace WisdomLight.ViewModel.Data.Files.Fields.Tools.Building.Filler
{
    public class FillerBuilder : IFillerBuilder
    {
        private FileViewModel _viewModel;

        private bool _isDefended;
        private bool _isRelative;

        private ICommand _newCommand;
        private ICommand _openCommand;
        private ICommand _saveCommand;
        private ICommand _saveAsCommand;
        private ICommand _closeCommand;

        private IEditableBuilder<DocumentLinker> _documents;
        private IEditableBuilder<FieldSelector> _information;

        private FieldSelector InformationAdditor()
        {
            TextExpression current = new TextExpression() { Type = "Текст" };
            return new FieldSelector
            {
                Expressions = new ObservableCollection<IExpression>
                {
                    current,
                    new NumberExpression() { Type = "Число" },
                    new DateExpression() { Type = "Дата" }
                },
                Current = current
            };
        }

        public FillerBuilder()
        {
            _documents = new EditableBuilder<DocumentLinker>()
                .Additor(new DocumentLinker { Name = "", Type = "" });
            _information = new EditableBuilder<FieldSelector>()
                .Additor(InformationAdditor());
        }

        public FileViewModel Build()
        {
            _information.Fields().SelectedItems().Add().Drop();
            _documents.Fields().SelectedItems().Add().Drop();

            _viewModel = new FileViewModel
            {
                Information = _information.Build(),
                Documents = _documents.Build(),
                NewCommand = _newCommand,
                OpenCommand = _openCommand,
                SaveCommand = _saveCommand,
                SaveAsCommand = _saveAsCommand,
                CloseCommand = _closeCommand,
                IsDefended = _isDefended,
                IsRelative = _isRelative
            };
            return _viewModel;
        }

        public IFillerBuilder Reset()
        {
            _viewModel = null;
            _newCommand = null;
            _openCommand = null;
            _saveCommand = null;
            _saveAsCommand = null;
            _closeCommand = null;
            _isDefended = false;
            _isRelative = false;
            _documents.Reset();
            _information.Reset();
            return this;
        }

        public IFillerBuilder Defended()
        {
            _isDefended = true;
            return this;
        }

        public IFillerBuilder Relative()
        {
            _isRelative = true;
            return this;
        }

        public IFillerBuilder Close()
        {
            _closeCommand = new RelayCommand(argument => _viewModel.Close?.Invoke(), can => _viewModel.CanClose);
            return this;
        }

        public IFillerBuilder NewFile()
        {
            _newCommand = new RelayCommand(
                argument =>
                {
                    new FillTemplatesWindow(_viewModel.CurrentLocation,
                        _viewModel.Serializer, _viewModel.IsDefended, _viewModel.IsRelative).Show();
                }
            );
            return this;
        }

        public IFillerBuilder Open()
        {
            _openCommand = new RelayCommand(
                argument =>
                {
                    KeyConfirmer dialog = DialogManager.Open(_viewModel.Serializer.Current);
                    if (dialog.Status.Result != DialogResult.OK)
                        return;
                    string path = dialog.Status.Path;
                    _viewModel.Serializer.Change(dialog.Key);
                    new FillTemplatesWindow(path, _viewModel.Serializer,
                        _viewModel.Serializer.Load(dialog.Status.Path)).Show();
                }
            );
            return this;
        }

        public IFillerBuilder Save()
        {
            _saveCommand = new RelayCommand(
                argument =>
                {
                    _viewModel.Serializer.FixedSave($"{_viewModel.CurrentLocation}\\{_viewModel.Name}", _viewModel);
                }
            );
            return this;
        }

        public IFillerBuilder SaveAs()
        {
            _saveAsCommand = new RelayCommand(
                argument =>
                {
                    KeyConfirmer dialog = DialogManager.Save(_viewModel.Serializer.Current);
                    if (dialog.Status.Result != DialogResult.OK)
                        return;
                    _viewModel.Serializer.Change(dialog.Key);
                    _viewModel.Serializer.Save(dialog.Status.Path, _viewModel);
                }
            );
            return this;
        }
    }
}
