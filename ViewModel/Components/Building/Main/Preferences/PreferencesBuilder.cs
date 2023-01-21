using System.Collections.ObjectModel;
using WisdomLight.ViewModel.Components.Data;
using WisdomLight.ViewModel.Data.Files.Processors.Serialization.Objects;

namespace WisdomLight.ViewModel.Components.Building.Main.Preferences
{
    public class PreferencesBuilder : IPreferencesBuilder
    {
        private PreferencesViewModel _viewModel;
        private FileFiller _serializer;

        private ObservableCollection<string> _templates;

        private bool _isDefended;
        private bool _isDefaultPath;

        public PreferencesViewModel Build()
        {
            _viewModel = new PreferencesViewModel
            {
                Serializer = _serializer,
                Templates = _templates,
                IsDefended = _isDefended,
                IsDefaultPath = _isDefaultPath
            };
            return _viewModel;
        }

        public IPreferencesBuilder Reset()
        {
            _viewModel = null;
            _serializer = null;
            _templates = null;
            _isDefended = false;
            _isDefaultPath = false;
            return this;
        }

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

        public IPreferencesBuilder Templates()
        {
            _templates = new ObservableCollection<string>();
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
    }
}
