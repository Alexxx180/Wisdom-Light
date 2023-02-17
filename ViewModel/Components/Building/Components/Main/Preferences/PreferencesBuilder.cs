using WisdomLight.ViewModel.Components.Core.Dialogs;
using WisdomLight.ViewModel.Components.Core.Processors.Serialization.Objects;
using WisdomLight.ViewModel.Components.Data;
using WisdomLight.ViewModel.Components.Data.Units;

namespace WisdomLight.ViewModel.Components.Building.Main.Preferences
{
    public class PreferencesBuilder : IPreferencesBuilder
    {
        private PreferencesViewModel _viewModel;
        private FileFiller _serializer;

        private DependenciesViewModel _generationTree;
        private DependenciesViewModel _dependencyTree;

        private bool _isDefended;
        private bool _isDefaultPath;

        public IPreferencesBuilder DefaultPath()
        {
            _isDefaultPath = true;
            return this;
        }

        public IPreferencesBuilder Defend()
        {
            _isDefended = true;
            return this;
        }

        private DependenciesViewModel Dependencies(string name)
        {
            DependenciesNode root = new DependenciesNode
            {
                Name = name
            };

            DependenciesCollection rootNodes = new DependenciesCollection(root);
            root.Nodes = rootNodes;

            return new DependenciesViewModel
            {
                Dependencies = new DependenciesCollection() { root },
                SelectedDependency = root
            };
        }

        public IPreferencesBuilder Templates()
        {
            _generationTree = Dependencies(nameof(Templates));
            return this;
        }

        public IPreferencesBuilder Documents()
        {
            _dependencyTree = Dependencies(nameof(Documents));
            return this;
        }

        public IPreferencesBuilder Serializer()
        {
            _serializer = new FileFiller()
            {
                Current = 0
            };
            return this;
        }

        public IPreferencesBuilder Reset()
        {
            _viewModel = null;
            _serializer = null;
            _generationTree = null;
            _dependencyTree = null;
            _isDefended = false;
            _isDefaultPath = false;
            return this;
        }

        public PreferencesViewModel Build()
        {
            _viewModel = new PreferencesViewModel
            {
                Serializer = _serializer,
                GenerationTree = _generationTree,
                DependencyTree = _dependencyTree,
                IsDefended = _isDefended,
                IsDefaultPath = _isDefaultPath
            };
            return _viewModel;
        }
    }
}
