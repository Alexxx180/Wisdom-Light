using System.Windows.Forms;
using System.Windows.Input;
using WisdomLight.Model;
using WisdomLight.ViewModel.Commands;
using WisdomLight.ViewModel.Components;
using WisdomLight.ViewModel.Components.Building.Templates;

namespace WisdomLight.ViewModel.Data.Files.Fields.Tools.Building.Filler
{
    public class FillerBuilder : IFillerBuilder
    {
        private FileViewModel _viewModel;
        private TemplateViewModel _data;

        private ICommand _newCommand;
        private ICommand _openCommand;
        private ICommand _saveCommand;
        private ICommand _saveAsCommand;
        private ICommand _closeCommand;

        private bool _canClose;

        private ITemplateBuilder _template;

        public FillerBuilder()
        {
            _template = new TemplateBuilder();
        }

        public FileViewModel Build()
        {
            _viewModel = new FileViewModel
            {
                Data = _data,
                NewCommand = _newCommand,
                OpenCommand = _openCommand,
                SaveCommand = _saveCommand,
                SaveAsCommand = _saveAsCommand,
                CloseCommand = _closeCommand,
                CanClose = _canClose
            };
            return _viewModel;
        }

        public IFillerBuilder Reset()
        {
            _canClose = false;
            _data = null;
            _viewModel = null;
            _newCommand = null;
            _openCommand = null;
            _saveCommand = null;
            _saveAsCommand = null;
            _closeCommand = null;
            _template.Reset();
            return this;
        }

        public IFillerBuilder Template()
        {
            _data = _template.Documents().Information().Serializer().Relate().Defend().Build();
            return this;
        }

        public IFillerBuilder NewFile()
        {
            _newCommand = new RelayCommand(
                argument =>
                {
                    _viewModel = Reset().Template().NewFile().Open().Save().SaveAs().CanClose().Close().Build();
                    new FillTemplatesWindow { ViewModel = _viewModel }.Show();
                }
            );
            return this;
        }

        public IFillerBuilder Open()
        {
            _openCommand = new RelayCommand(
                argument =>
                {
                    KeyConfirmer dialog = DialogManager.Open(_viewModel.Data.Serializer.Current);
                    if (dialog.Status.Result != DialogResult.OK)
                        return;
                    string path = dialog.Status.Path;
                    _viewModel.Data.Serializer.Current = dialog.Key;
                    
                    FileViewModel viewModel = Reset().NewFile().Open().Save().SaveAs().CanClose().Close().Build();
                    viewModel.Data = _viewModel.Data.Serializer.Load(path);

                    new FillTemplatesWindow { ViewModel = viewModel }.Show();
                }
            );
            return this;
        }

        public IFillerBuilder Save()
        {
            _saveCommand = new RelayCommand(
                argument =>
                {
                    string path = $"{_viewModel.Data.Location}\\{_viewModel.Data.Name}";
                    _viewModel.Data.Serializer.FixedSave(path, _viewModel.Data);
                }
            );
            return this;
        }

        public IFillerBuilder SaveAs()
        {
            _saveAsCommand = new RelayCommand(
                argument =>
                {
                    KeyConfirmer dialog = DialogManager.Save(_viewModel.Data.Serializer.Current);
                    if (dialog.Status.Result != DialogResult.OK)
                        return;
                    _viewModel.Data.Serializer.Current = dialog.Key;
                    _viewModel.Data.Serializer.Save(dialog.Status.Path, _viewModel.Data);
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
    }
}
