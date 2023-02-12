using WisdomLight.ViewModel.Components.Data;

namespace WisdomLight.ViewModel.Components.Building.Components.Filler
{
    public abstract class FillerBuilder2
    {
        private protected FileViewModel _viewModel;

        public abstract FillerBuilder2 OpenQuery(DependenciesViewModel dependencies);

        public abstract FillerBuilder2 Template();

        public abstract FillerBuilder2 NewFile();

        public abstract FillerBuilder2 Open();

        public abstract FillerBuilder2 Save();

        public abstract FillerBuilder2 SaveAs();

        public abstract FillerBuilder2 Export();

        public abstract FillerBuilder2 Close();

        public abstract FillerBuilder2 CanClose();

        public abstract FillerBuilder2 Add();

        public abstract FillerBuilder2 Drop();

        public abstract FillerBuilder2 OpenLink();

        public abstract FillerBuilder2 Reset();

        public abstract FileViewModel Build();
    }
}
