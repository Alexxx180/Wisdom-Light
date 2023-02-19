using WisdomLight.ViewModel.Components.Data;

namespace WisdomLight.ViewModel.Components.Building.Main.Preferences
{
    public interface IPreferencesBuilder
    {
        public IPreferencesBuilder Defend();

        public IPreferencesBuilder DefaultPath();

        public IPreferencesBuilder Templates();

        public IPreferencesBuilder Documents();

        public IPreferencesBuilder Serializer();

        public IPreferencesBuilder Reset();

        public PreferencesViewModel Build();
    }
}
