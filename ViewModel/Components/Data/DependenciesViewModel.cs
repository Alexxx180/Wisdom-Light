using WisdomLight.ViewModel.Components.Core.Dialogs;
using WisdomLight.ViewModel.Components.Data.Units;

namespace WisdomLight.ViewModel.Components.Data
{
    public class DependenciesViewModel : NotifyPropertyChanged
    {
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
    }
}
