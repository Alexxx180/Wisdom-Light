using System.Windows.Forms;
using static System.Environment;
using WisdomLight.Model;
using WisdomLight.ViewModel.Customing;
using static WisdomLight.ViewModel.Customing.Decorators;

namespace WisdomLight.ViewModel.Data.Files.Dialogs
{
    public class OpenDialog : FileDialog
    {
        public override void ShowDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = Title,
                Filter = Filter,
                InitialDirectory = GetFolderPath(SpecialFolder.DesktopDirectory).Close()
            };
            DialogResult status = dialog.ShowDialog();
            Result = new KeyConfirmer((byte)dialog.FilterIndex, dialog.FileName, status);
        }
    }
}
