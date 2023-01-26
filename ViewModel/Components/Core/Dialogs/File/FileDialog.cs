namespace WisdomLight.ViewModel.Components.Core.Dialogs.File
{
    public abstract class FileDialog : Dialog
    {
        public string FileName { get; set; }
        public string Filter { get; set; }
        public byte FilterIndex { get; set; }
    }
}
