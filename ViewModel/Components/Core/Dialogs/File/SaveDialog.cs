using System.Windows.Forms;
using WisdomLight.Model;

namespace WisdomLight.ViewModel.Data.Files.Dialogs
{
    public class SaveDialog : FileDialog
    {
        public KeyConfirmer Result { get; private protected set; }

        public override void ShowDialog()
        {
            using SaveFileDialog dialog = new SaveFileDialog
            {
                Title = Title,
                FileName = FileName,
                Filter = Filter,
                FilterIndex = FilterIndex,
                InitialDirectory = InitialDirectory
            };
            bool status = dialog.ShowDialog() == DialogResult.OK;
            byte selected = (byte)(dialog.FilterIndex - 1);

            Result = new KeyConfirmer(selected, dialog.FileName, status);
        }
    }
}
