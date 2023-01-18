using Serilog;
using System.Windows;
using static System.Windows.MessageBoxImage;
using MessageBox = System.Windows.MessageBox;
using WisdomLight.Model;
using WisdomLight.Model.Exceptions;
using static WisdomLight.ViewModel.Customing.Decorators;
using WisdomLight.ViewModel.Data.Files.Dialogs.Folder;
using WisdomLight.ViewModel.Data.Files.Dialogs;

namespace WisdomLight.ViewModel.Data.Files
{
    public static class DialogManager
    {
        public static MessageBoxResult Message(IDetails exception)
        {
            string message = exception.Details();
            Log.Error(message);
            return MessageBox.Show(message, nameof(Error), MessageBoxButton.OK, Error);
        }

        public static KeyConfirmer Save()
        {
            SaveDialog dialog = new SaveDialog
            {
                Title = "Сохранить как...",
                Filter = Filter("Шаблон данных JSON", "*.json")
            };
            dialog.ShowDialog();
            return dialog.Result;
        }

        public static KeyConfirmer Open()
        {
            OpenDialog dialog = new OpenDialog
            {
                Title = "Выберите шаблон данных",
                Filter = Filter("Шаблон данных JSON", "*.json")
            };
            dialog.ShowDialog();
            return dialog.Result;
        }

        public static Confirmer Export()
        {
            FolderDialog dialog = new FolderDialog
            {
                Title = "Экспортировать в..."
            };
            dialog.ShowDialog();
            return dialog.Result;
        }
    }
}