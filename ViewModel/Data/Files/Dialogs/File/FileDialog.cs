using WisdomLight.Model;

namespace WisdomLight.ViewModel.Data.Files.Dialogs
{
    public abstract class FileDialog : Dialog
    {
        public KeyConfirmer Result { get; private protected set; }

        public string Filter { get; set; }
    }
}
