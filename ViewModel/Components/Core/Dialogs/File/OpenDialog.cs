using System.Windows.Forms;
using System.IO;
using WisdomLight.Model.Results.Confirming;

namespace WisdomLight.ViewModel.Components.Core.Dialogs.File
{
    public class OpenDialog : FileDialog
    {
        public ReConfirmer Result { get; private protected set; }

        public override void ShowDialog()
        {
            using OpenFileDialog dialog = new OpenFileDialog
            {
                Title = Title,
                FileName = FileName,
                Filter = Filter,
                FilterIndex = FilterIndex,
                InitialDirectory = InitialDirectory,
                CheckFileExists = true,
                CheckPathExists = true
            };
            bool status = dialog.ShowDialog() == DialogResult.OK;
            byte selected = (byte)(dialog.FilterIndex - 1);

            string path = Path.GetDirectoryName(dialog.FileName);

            Result = new ReConfirmer(selected, dialog.SafeFileName, path, status);
        }
    }
}
