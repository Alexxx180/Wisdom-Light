using System.Windows.Input;
using WisdomLight.Model.Results.Confirming;
using WisdomLight.View;
using WisdomLight.ViewModel.Components.Core.Commands;
using WisdomLight.ViewModel.Components.Core.Dialogs.Traditional.Manager;
using WisdomLight.ViewModel.Components.Core.Processors.Serialization.Objects;

namespace WisdomLight.ViewModel.Components.Building.Components.Filler.Blocks
{
    public abstract class FillerWindow : FillerBase
    {
        private const string DefaultWindowName = "Новый документ";
        private IWindowService _windows;

        private ICommand _newCommand;
        private ICommand _openCommand;
        private ICommand _closeCommand;
        private bool _canClose;

        public FillerWindow(IWindowService windows) : base()
        {
            _windows = windows;
        }

        private protected virtual FillerBuilder2 ViewModel()
        {
            return Reset().NewFile().CanClose().Close();
        }

        public override FillerBuilder2 Close()
        {
            _closeCommand = new RelayCommand(argument => _viewModel.Close?.Invoke(), can => _viewModel.CanClose);
            return this;
        }

        public override FillerBuilder2 CanClose()
        {
            _canClose = true;
            return this;
        }

        public override FillerBuilder2 NewFile()
        {
            _newCommand = new RelayCommand(
                argument =>
                {
                    string location = _viewModel.Data.Location;
                    _viewModel = ViewModel().Template().Build();
                    _viewModel.Data.Name = DefaultWindowName;
                    _viewModel.Data.Location = location;
                    _windows.ShowWindow(_viewModel);
                }
            );
            return this;
        }

        public override FillerBuilder2 Open()
        {
            _openCommand = new RelayCommand(
                argument =>
                {
                    FileFiller serializer = _viewModel.Data.Serializer;
                    ReConfirmer dialog = TemplateManager.Open(_viewModel.Data.Location, serializer.Current);
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

        public override FillerBuilder2 Template()
        {
            _ = base.Template();
            return this;
        }

        public override FillerBuilder2 Reset()
        {
            _ = base.Reset();
            _newCommand = null;
            _openCommand = null;
            _closeCommand = null;
            _canClose = false;
            return this;
        }

        public override FileViewModel Build()
        {
            FileViewModel viewModel = base.Build();
            viewModel.NewCommand = _newCommand;
            viewModel.OpenCommand = _openCommand;
            viewModel.CloseCommand = _closeCommand;
            viewModel.CanClose = _canClose;
            return viewModel;
        }
    }
}
