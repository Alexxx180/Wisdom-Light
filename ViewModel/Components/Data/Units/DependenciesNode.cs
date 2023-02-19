using System.ComponentModel;
using Newtonsoft.Json;
using WisdomLight.ViewModel.Components.Core.Dialogs;

namespace WisdomLight.ViewModel.Components.Data.Units
{
    public class DependenciesNode : NameLabel, INotifyPropertyChanged
    {
        [JsonIgnore]
        public DependenciesNode Parent { get; private set; }

        public DependenciesNode()
        {
            IsDependency = false;
        }

        public DependenciesNode this[int index]
        {
            get => Nodes[index];
            set => Nodes[index] = value;
        }

        private int _no;
        public int No
        {
            get => _no;
            set
            {
                _no = value;
                OnPropertyChanged();
            }
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

        public DependenciesNode Clone()
        {
            return new DependenciesNode
            {
                Nodes = new DependenciesCollection()
            };
        }

        public void Relate()
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                DependenciesNode node = Nodes[i];
                node.Parent = this;
                node.Relate();
            }
        }

        public void Add(DependenciesNode node)
        {
            node.Parent = this;
            Nodes.Add(node);
        }

        public void Add(string name)
        {
            DependenciesNode next = new DependenciesNode
            {
                Name = name,
                Nodes = new DependenciesCollection()
            };
            Add(next);
        }

        public void Drop()
        {
            Parent.Nodes.Remove(this);
        }

        public DependenciesCollection Nodes { get; set; }
    }
}
