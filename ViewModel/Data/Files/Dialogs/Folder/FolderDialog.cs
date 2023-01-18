using System.Windows.Forms;
using static System.Environment;
using WisdomLight.Model;
using WisdomLight.ViewModel.Customing;
using static WisdomLight.ViewModel.Customing.Decorators;

namespace WisdomLight.ViewModel.Data.Files.Dialogs.Folder
{
    public class FolderDialog : Dialog
    {
        public Confirmer Result { get; private protected set; }

        public override void ShowDialog()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                Description = Title,
                UseDescriptionForTitle = true,
                SelectedPath = GetFolderPath(SpecialFolder.DesktopDirectory).Close(),
                ShowNewFolderButton = true
            };
            DialogResult status = dialog.ShowDialog();
            Result = new Confirmer(dialog.SelectedPath, status);
        }
    }
}
