using System;
using System.Linq;
using System.Windows.Input;
using WisdomLight.ViewModel.Components.Core.Commands;
using WisdomLight.ViewModel.Components.Core.Dialogs;
using WisdomLight.ViewModel.Components.Data.Units;

namespace WisdomLight.ViewModel.Components.Data
{
    public class NameViewModel : NameLabel, ICloseable
    {
        private DependenciesCollection _dependencies;
        public bool IsAvailable => !string.IsNullOrEmpty(Name) && !_dependencies.GetNames().Contains(Name);

        public NameViewModel(DependenciesNode dependencies)
        {
            _dependencies = dependencies.Nodes;
            Name = dependencies.Name;
        }

        public override string Name
        {
            get => base.Name;
            set
            {
                base.Name = value;
                OnPropertyChanged(nameof(IsAvailable));
            }
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

        public ICommand CloseCommand { get; protected internal set; }
    }
}
