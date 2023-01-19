namespace WisdomLight.ViewModel.Data.Files.Fields.Tools.Building.Filler
{
    public interface IFillerBuilder
    {
        public IFillerBuilder NewFile();

        public IFillerBuilder Open();

        public IFillerBuilder Save();

        public IFillerBuilder SaveAs();

        public IFillerBuilder Close();

        public IFillerBuilder Reset();

        public FileViewModel Build();
    }
}
