using System;
using System.Windows.Input;
using WisdomLight.Model.Results.Confirming;
using WisdomLight.ViewModel.Components.Building.Filler;
using WisdomLight.ViewModel.Components.Building.Main.Preferences;
using WisdomLight.ViewModel.Components.Core.Commands;
using WisdomLight.ViewModel.Components.Core.Dialogs;
using WisdomLight.ViewModel.Components.Data;

namespace WisdomLight.ViewModel.Components.Building.Main
{
    public class MainBuilder : IMainBuilder
    {
        private MainViewModel _viewModel;
        private PreferencesViewModel _preferences;

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
            _filler = new FillerBuilder();
            _preferencesBuilder = new PreferencesBuilder();
        }

        public MainViewModel Build()
        {
            _viewModel = new MainViewModel
            {
                Preferences = _preferences,
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

        public IMainBuilder Preferences()
        {
            _preferences = _preferencesBuilder.Serializer().Templates().Defend().DefaultPath().Build();
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
                    FileViewModel viewModel = _filler.Reset().Template().NewFile().Open().Save().SaveAs().Export().CanClose().Close().Add().Drop().Choose().Build();

                    viewModel.Data.Location = _viewModel.Preferences.SelectedLocation;

                    new FillTemplatesWindow
                    {
                        ViewModel = viewModel
                    }
                    .Show();
                }
            );
            return this;
        }

        public IMainBuilder Open()
        {
            _openCommand = new RelayCommand(
                argument =>
                {
                    ReConfirmer dialog = DialogManager.Open(_viewModel.Preferences.SelectedLocation, _viewModel.Preferences.Serializer.Current);
                    if (!dialog.Result)
                        return;

                    _viewModel.Preferences.Serializer.Current = dialog.Key;

                    FileViewModel viewModel = _filler.Reset().NewFile().Open().Save().SaveAs().Export().CanClose().Close().Add().Drop().Choose().Build();
                    
                    viewModel.Data = _viewModel.Preferences.Serializer.Load(dialog.FullPath);
                    viewModel.Data.Location = dialog.Path;
                    viewModel.Data.FileName = dialog.Name;

                    new FillTemplatesWindow { ViewModel = viewModel }.Show();
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

        //    if (Keyboard.FocusedElement is TextBox textBox)
        //    {
        //        TraversalRequest tRequest = new
        //            TraversalRequest(FocusNavigationDirection.Next);
        //        _ = textBox.MoveFocus(tRequest);
        //    }
    }
}
