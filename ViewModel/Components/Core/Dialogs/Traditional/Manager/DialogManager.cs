using Serilog;
using System.Windows;
using static System.Windows.MessageBoxImage;
using MessageBox = System.Windows.MessageBox;
using WisdomLight.Model;
using WisdomLight.Model.Exceptions;
using WisdomLight.Model.Results.Confirming;
using WisdomLight.ViewModel.Components.Core.Dialogs.Folder;
using WisdomLight.ViewModel.Components.Core.Dialogs.File;

namespace WisdomLight.ViewModel.Components.Core.Dialogs.Traditional.Manager
{
    public static class DialogManager
    {
        public static MessageBoxResult Message(IDetails exception)
        {
            string message = exception.Details();
            Log.Error(message);
            return MessageBox.Show(message, nameof(Error), MessageBoxButton.OK, Error);
        }

        internal static KeyConfirmer Save(string initial, string title, string name, string filter, byte current = 1)
        {
            SaveDialog dialog = new SaveDialog
            {
                Title = title,
                FileName = name,
                InitialDirectory = initial,
                Filter = filter,
                FilterIndex = current
            };
            dialog.ShowDialog();
            return dialog.Result;
        }

        internal static ReConfirmer Open(string defaultPath, string title, string filter, byte filterIndex)
        {
            OpenDialog dialog = new OpenDialog
            {
                Title = title,
                InitialDirectory = defaultPath,
                Filter = filter,
                FilterIndex = filterIndex
            };
            dialog.ShowDialog();
            return dialog.Result;
        }

        internal static Confirmer Export(string title)
        {
            FolderDialog dialog = new FolderDialog
            {
                Title = title
            };
            dialog.ShowDialog();
            return dialog.Result;
        }
    }
}