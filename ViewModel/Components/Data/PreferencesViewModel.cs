using System.Collections.ObjectModel;
using WisdomLight.ViewModel.Components.Building.Bank;
using WisdomLight.ViewModel.Components.Core.Dialogs;
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
            DependenciesNode root = new DependenciesNode
            {
                Name = "Templates",
                Nodes = new DependenciesCollection
                {
                    new DependenciesNode { Name = "#1" },
                    new DependenciesNode { Name = "#2" },
                    new DependenciesNode { Name = "#3" }
                }
            };

            DependenciesCollection nodes = new DependenciesCollection() { root };
            DependencyTree = new DependenciesViewModel
            {
                Dependencies = nodes,
                SelectedDependency = root
            };
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
