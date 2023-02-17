using System;
using System.Windows.Input;
using WisdomLight.Model;
using WisdomLight.ViewModel.Components.Core.Commands;
using WisdomLight.ViewModel.Components.Core.Dialogs;
using WisdomLight.ViewModel.Components.Data.Units;

namespace WisdomLight.ViewModel.Components.Data
{
    public class DependenciesViewModel : NotifyPropertyChanged, ICloseable
    {
        public DependenciesViewModel()
        {
            Width = new Limiter(200, 380, 640);
            Height = new Limiter(200, 320, 480);
        }

        private DependenciesCollection _dependencies;
        public DependenciesCollection Dependencies
        {
            get => _dependencies;
            set
            {
                _dependencies = value;
                OnPropertyChanged();
            }
        }

        private DependenciesNode _selectedDependency;
        public DependenciesNode SelectedDependency
        {
            get => _selectedDependency;
            set
            {
                _selectedDependency = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsDependencySelected));
            }
        }

        public bool IsDependencySelected => SelectedDependency is not null;
        
        public string QueryPath(Querier courier)
        {
            int first = courier.Pop();
            DependenciesNode node = Dependencies[first];
            foreach (int index in courier)
            {
                node = node[index];
            }
            courier.Push(first);
            return node.DependencyPath;
        }

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

        public Limiter Width { get; }
        public Limiter Height { get; }

        public ICommand CloseCommand { get; protected internal set; }
    }
}
