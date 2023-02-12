namespace WisdomLight.ViewModel.Components.Building.Main
{
    public interface IMainBuilder
    {
        public IMainBuilder Preferences();

        public IMainBuilder AddInformation();

        public IMainBuilder DropInformation();

        public IMainBuilder AddLink();

        public IMainBuilder DropLink();

        public IMainBuilder OpenDependency();

        public IMainBuilder RenameDependency();

        public IMainBuilder NewFile();

        public IMainBuilder Open();

        public IMainBuilder Import();
        
        public IMainBuilder Close();

        public IMainBuilder CanClose();

        public IMainBuilder Reset();

        public MainViewModel Build();
    }
}
