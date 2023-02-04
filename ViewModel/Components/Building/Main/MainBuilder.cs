using System;
using System.Windows.Input;
using WisdomLight.Model.Results.Confirming;
using WisdomLight.View;
using WisdomLight.ViewModel.Components.Building.Filler;
using WisdomLight.ViewModel.Components.Building.Main.Preferences;
using WisdomLight.ViewModel.Components.Core.Commands;
using WisdomLight.ViewModel.Components.Core.Dialogs;
using WisdomLight.ViewModel.Components.Data;

namespace WisdomLight.ViewModel.Components.Building.Main
{
    public class MainBuilder : IMainBuilder
    {
#warning Potential solution for re-focus after text input...
        /// <summary>
        /// if (Keyboard.FocusedElement is TextBox textBox)
        /// {
        ///     TraversalRequest tRequest = new
        ///     TraversalRequest(FocusNavigationDirection.Next);
        ///     _ = textBox.MoveFocus(tRequest);
        /// }
        /// </summary>

        private IWindowService _windows;
        private MainViewModel _viewModel;
        private PreferencesViewModel _data;

        private IPreferencesBuilder _preferencesBuilder;
        private IFillerBuilder _filler;

        public ICommand _addCommand;
        public ICommand _dropCommand;
        public ICommand _newCommand;
        public ICommand _openCommand;
        public ICommand _importCommand;
        public ICommand _searchCommand;
        public ICommand _closeCommand;

        private bool _canClose;

        public MainBuilder()
        {
            _filler = new FillerBuilder(_windows);
            _preferencesBuilder = new PreferencesBuilder();
        }

        public MainBuilder(IWindowService windows) : this()
        {
            _windows = windows;
        }

        private IFillerBuilder BaseFiller()
        {
            return _filler.Reset().NewFile().Open().Save().SaveAs().Export().CanClose().Close().Add().Drop().Choose();
        }

        public IMainBuilder Preferences()
        {
            _data = _preferencesBuilder.Serializer().Templates().Defend().DefaultPath().Build();
            return this;
        }

        public IMainBuilder Add()
        {
            throw new NotImplementedException();
        }

        public IMainBuilder Drop()
        {
            throw new NotImplementedException();
        }

        public IMainBuilder Import()
        {
            throw new NotImplementedException();
        }

        public IMainBuilder NewFile()
        {
            _newCommand = new RelayCommand(
                argument =>
                {
                    FileViewModel viewModel = BaseFiller().Template().Build();

                    viewModel.Data.Name = "Новый документ";
                    viewModel.Data.Location = _viewModel.Data.SelectedLocation;

                    _windows.ShowWindow(viewModel);
                }
            );
            return this;
        }

        public IMainBuilder Open()
        {
            _openCommand = new RelayCommand(
                argument =>
                {
                    ReConfirmer dialog = DialogManager.Open(_viewModel.Data.SelectedLocation, _viewModel.Data.Serializer.Current);
                    if (!dialog.Result)
                        return;

                    _viewModel.Data.Serializer.Current = dialog.Key;

                    FileViewModel viewModel = BaseFiller().Build();
                    
                    viewModel.Data = _viewModel.Data.Serializer.Load(dialog.FullPath);
                    viewModel.Data.Location = dialog.Path;
                    viewModel.Data.FileName = dialog.Name;

                    _windows.ShowWindow(viewModel);
                }
            );
            return this;
        }

        public IMainBuilder Close()
        {
            _closeCommand = new RelayCommand(argument => _viewModel.Close?.Invoke(), can => _viewModel.CanClose);
            return this;
        }

        public IMainBuilder CanClose()
        {
            _canClose = true;
            return this;
        }

        

        public IMainBuilder Reset()
        {
            _canClose = false;
            _viewModel = null;
            _addCommand = null;
            _dropCommand = null;
            _newCommand = null;
            _openCommand = null;
            _importCommand = null;
            _searchCommand = null;
            _closeCommand = null;
            _preferencesBuilder.Reset();
            _filler.Reset();
            return this;
        }

        public MainViewModel Build()
        {
            _viewModel = new MainViewModel
            {
                Data = _data,
                AddCommand = _addCommand,
                DropCommand = _dropCommand,
                NewCommand = _newCommand,
                OpenCommand = _openCommand,
                ImportCommand = _importCommand,
                SearchCommand = _searchCommand,
                CloseCommand = _closeCommand,
                CanClose = _canClose
            };
            return _viewModel;
        }
    }
}
