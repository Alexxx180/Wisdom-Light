using System.Collections.ObjectModel;
using WisdomLight.ViewModel.Components.Building.Bank;
using WisdomLight.ViewModel.Components.Core.Processors.Serialization.Objects;
using WisdomLight.ViewModel.Components.Data.Units;

namespace WisdomLight.ViewModel.Components.Data
{
    public class PreferencesViewModel : NameLabel
    {
        public FileFiller Serializer { get; set; }

        public PreferencesViewModel()
        {
            Name = "Облегченная Мудрость";
        }

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

        private ObservableCollection<string> _links;
        public ObservableCollection<string> Links
        {
            get => _links;
            set
            {
                _links = value;
                OnPropertyChanged();
            }
        }

        public string SelectedLocation
        {
            get;
            set;
        }

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
                    Defaults.Runtime : UserLocation;
                OnPropertyChanged();
            }
        }
    }
}
