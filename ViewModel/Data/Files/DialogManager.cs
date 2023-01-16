using Serilog;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using WisdomLight.Model;
using WisdomLight.Model.Exceptions;
using WisdomLight.ViewModel.Customing;
using static System.Environment;
using static System.Windows.MessageBoxImage;
using MessageBox = System.Windows.MessageBox;


namespace WisdomLight.ViewModel.Data.Files
{
    public static class DialogManager
    {
        public static readonly string TemplateFilter;

        static DialogManager()
        {
            TemplateFilter = "Документ Microsoft Word (*.docx)|*.docx";
        }

        public static Confirmer AskFolder()
        {
            using FolderBrowserDialog dialog = FolderLocator("Выберите место для сохранения");
            DialogResult status = dialog.ShowDialog();
            return new Confirmer(status, dialog.SelectedPath);
        }

        public static MessageBoxResult Message(IDetails exception)
        {
            string message = exception.Details();
            Log.Error(message);
            return MessageBox.Show(message, nameof(Error), MessageBoxButton.OK, Error);
        }

        private static FolderBrowserDialog FolderLocator(string description)
        {
            return new FolderBrowserDialog
            { 
                Description = description,
                UseDescriptionForTitle = true,
                SelectedPath = GetFolderPath(SpecialFolder.DesktopDirectory).Close(),
                ShowNewFolderButton = true
            };
        }
    }
}