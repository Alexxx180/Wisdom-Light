using WisdomLight.Model.Results.Confirming;

namespace WisdomLight.ViewModel.Data.Files.Dialogs
{
    public abstract class FileDialog : Dialog
    {
        public string FileName { get; set; }
        public string Filter { get; set; }
        public byte FilterIndex { get; set; }
    }
}
