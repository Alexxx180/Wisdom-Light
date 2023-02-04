using System;
using System.Windows.Input;
using WisdomLight.ViewModel.Components.Core.Commands;
using WisdomLight.ViewModel.Components.Data;

namespace WisdomLight.ViewModel.Components
{
    public class MainViewModel : NotifyPropertyChanged, ICloseable
    {
        public MainViewModel() { }

        public Action Close { get; set; }

        private bool _canClose;
        public bool CanClose
        {
            get => _canClose;
            set
            {
                _canClose = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; protected internal set; }
        public ICommand DropCommand { get; protected internal set; }
        public ICommand NewCommand { get; protected internal set; }
        public ICommand OpenCommand { get; protected internal set; }
        public ICommand ImportCommand { get; protected internal set; }
        public ICommand SearchCommand { get; protected internal set; }
        public ICommand CloseCommand { get; protected internal set; }

        public PreferencesViewModel Data { get; set; }
    }
}
