using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WisdomLight.ViewModel.Components.Data.Units
{
    public class DependenciesNode : NameLabel, INotifyPropertyChanged
    {
        public DependenciesNode()
        {
            IsDependency = false;
        }

        public DependenciesNode this[int index]
        {
            get => Nodes[index];
            set => Nodes[index] = value;
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    OnPropertyChanged();
                }

                // Expand all the way up to the root.
                //if (_isExpanded && _parent != null)
                //    _parent.IsExpanded = true;
            }
        }

        private bool _isDependency;
        public bool IsDependency
        {
            get => _isDependency;
            set
            {
                if (value != _isDependency)
                {
                    _isDependency = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _dependencyPath;
        public string DependencyPath
        {
            get => _dependencyPath;
            set
            {
                IsDependency = !string.IsNullOrEmpty(value);
                if (IsDependency)
                {
                    _dependencyPath = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<DependenciesNode> Nodes { get; set; }
    }
}
