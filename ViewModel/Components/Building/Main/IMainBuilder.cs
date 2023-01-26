namespace WisdomLight.ViewModel.Components.Building.Main
{
    public interface IMainBuilder
    {
        public IMainBuilder Preferences();

        public IMainBuilder Add();

        public IMainBuilder Drop();

        public IMainBuilder NewFile();

        public IMainBuilder Open();

        public IMainBuilder Import();
        
        public IMainBuilder Close();

        public IMainBuilder CanClose();

        public IMainBuilder Reset();

        public MainViewModel Build();
    }
}
