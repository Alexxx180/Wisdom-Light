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
                FilterIndex = FilterIndex,
                InitialDirectory = GetFolderPath(SpecialFolder.DesktopDirectory).Close()
            };
            DialogResult status = dialog.ShowDialog();
            byte selected = (byte)(dialog.FilterIndex - 1);
            Result = new KeyConfirmer(selected, dialog.FileName, status);
        }
    }
}
