using System.Collections.ObjectModel;
using WisdomLight.ViewModel.Data.Files.Processors.Serialization.Objects;

namespace WisdomLight.ViewModel.Components.Data
{
    public class PreferencesViewModel : NotifyPropertyChanged
    {
        public FileFiller Serializer { get; set; }

        private ObservableCollection<string> _templates;
        public ObservableCollection<string> Templates
        {
            get => _templates;
            set
            {
                _templates = value;
                OnPropertyChanged();
            }
        }

        public string SelectedLocation { get; set; }

        private string _userLocation;
        public string UserLocation
        {
            get => _userLocation;
            set
            {
                _userLocation = value;
                OnPropertyChanged();
            }
        }
        
        private bool _isDefended;
        public bool IsDefended
        {
            get => _isDefended;
            set
            {
                _isDefended = value;
                OnPropertyChanged();
            }
        }

        private bool _isDefaultPath;
        public bool IsDefaultPath
        {
            get => _isDefaultPath;
            set
            {
                _isDefaultPath = value;
                SelectedLocation = value ?
                    App.DefaultLocation : UserLocation;
                OnPropertyChanged();
            }
        }
    }
}
