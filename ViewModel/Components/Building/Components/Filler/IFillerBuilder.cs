using WisdomLight.ViewModel.Components.Data;

namespace WisdomLight.ViewModel.Components.Building.Filler
{
    public interface IFillerBuilder
    {
        public IFillerBuilder SetDependencies(DependenciesViewModel dependencies);

        public IFillerBuilder OpenQuery();

        public IFillerBuilder Template();

        public IFillerBuilder NewFile();

        public IFillerBuilder Open();

        public IFillerBuilder Save();

        public IFillerBuilder SaveAs();

        public IFillerBuilder Export();

        public IFillerBuilder Close();

        public IFillerBuilder CanClose();

        public IFillerBuilder Add();

        public IFillerBuilder Drop();

        public IFillerBuilder OpenLink();

        public IFillerBuilder Reset();

        public FileViewModel Build();
    }
}
