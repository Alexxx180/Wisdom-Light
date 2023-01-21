namespace WisdomLight.ViewModel.Data.Files.Dialogs
{
    public abstract class Dialog : IDialogService
    {
        public string Title { get; set; }
        public string InitialDirectory { get; set; }

        public abstract void ShowDialog();
    }
}
