namespace WisdomLight.ViewModel.Components.Core.Dialogs
{
    public abstract class Dialog : ITraditionalDialogService
    {
        public string Title { get; set; }
        public string InitialDirectory { get; set; }

        public abstract void ShowDialog();
    }
}
