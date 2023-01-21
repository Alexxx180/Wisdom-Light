using System;
using System.Windows.Input;
using WisdomLight.ViewModel.Commands;
using WisdomLight.ViewModel.Components.Data;

namespace WisdomLight.ViewModel
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

        public ICommand AddCommand { get; set; }
        public ICommand DropCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand OpenCommand { get; set; }
        public ICommand ImportCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public PreferencesViewModel Preferences { get; set; }
    }
}
