using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WisdomLight.Model;
using WisdomLight.ViewModel.Commands;
using WisdomLight.ViewModel.Data.Collections;

namespace WisdomLight.ViewModel.Data.Files.Fields.Tools.Building.Filler
{
    public class FillerBuilder : IFillerBuilder
    {
        private FileViewModel _viewModel;

        private ICommand _newCommand;
        private ICommand _openCommand;
        private ICommand _saveCommand;
        private ICommand _saveAsCommand;
        private ICommand _closeCommand;

        private EditableCollection<DocumentLinker> _documents;
        private EditableCollection<FieldSelector> _information;

        public FileViewModel Build()
        {
            _viewModel = new FileViewModel
            {
                NewCommand = _newCommand,
                OpenCommand = _openCommand,
                SaveCommand = _saveCommand,
                SaveAsCommand = _saveAsCommand,
                CloseCommand = _closeCommand
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
            return this;
        }

        public IFillerBuilder Close()
        {
            throw new NotImplementedException();
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
                    //_viewModel.Serializer.FixedSave(path, ViewModel);
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
