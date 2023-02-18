using WisdomLight.ViewModel.Components.Building.Bank;
using WisdomLight.ViewModel.Components.Core.Processors.Serialization.Objects;

namespace WisdomLight.ViewModel.Components.Data
{
    public class PreferencesViewModel : NotifyPropertyChanged
    {
        public string Name { get; }
        public FileFiller Serializer { get; set; }

        public PreferencesViewModel()
        {
            Name = "Облегченная Мудрость";
        }

        private DependenciesViewModel _generationTree;
        public DependenciesViewModel GenerationTree
        {
            get => _generationTree;
            set
            {
                _generationTree = value;
                OnPropertyChanged();
            }
        }

        private DependenciesViewModel _dependencyTree;
        public DependenciesViewModel DependencyTree
        {
            get => _dependencyTree;
            set
            {
                _dependencyTree = value;
                OnPropertyChanged();
            }
        }

        private string _selectedLocation;
        public string SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                _selectedLocation = value;
                OnPropertyChanged();
            }
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
