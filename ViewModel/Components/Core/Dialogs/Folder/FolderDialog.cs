using System.Windows.Forms;
using WisdomLight.Model;

namespace WisdomLight.ViewModel.Components.Core.Dialogs.Folder
{
    public class FolderDialog : TraditionalDialog
    {
        public Confirmer Result { get; private protected set; }

        public override void ShowDialog()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                Description = Title,
                UseDescriptionForTitle = true,
                SelectedPath = InitialDirectory,
                ShowNewFolderButton = true
            };
            bool status = dialog.ShowDialog() == DialogResult.OK;
            Result = new Confirmer(dialog.SelectedPath, status);
        }
    }
}
