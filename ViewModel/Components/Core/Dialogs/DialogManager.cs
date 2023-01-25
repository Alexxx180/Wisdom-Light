using Serilog;
using System.Windows;
using static System.Windows.MessageBoxImage;
using MessageBox = System.Windows.MessageBox;
using WisdomLight.Model;
using WisdomLight.Model.Exceptions;
using WisdomLight.Model.Results.Confirming;
using WisdomLight.ViewModel.Components.Core.Dialogs.Folder;
using WisdomLight.ViewModel.Components.Core.Dialogs.File;
using static WisdomLight.ViewModel.Components.Building.Extensions.Decorators.Filters;

namespace WisdomLight.ViewModel.Components.Core.Dialogs
{
    public static class DialogManager
    {
        public static MessageBoxResult Message(IDetails exception)
        {
            string message = exception.Details();
            Log.Error(message);
            return MessageBox.Show(message, nameof(Error), MessageBoxButton.OK, Error);
        }

        public static KeyConfirmer Save(string defaultPath, string name, byte filterIndex = 1)
        {
            SaveDialog dialog = new SaveDialog
            {
                Title = "Сохранить как...",
                FileName = name,
                InitialDirectory = defaultPath,
                Filter = Option("Шаблон данных JSON", "*.json"),
                FilterIndex = filterIndex
            };
            dialog.ShowDialog();
            return dialog.Result;
        }

        public static ReConfirmer Open(string defaultPath, byte filterIndex = 1)
        {
            return Choose(defaultPath, Option("Шаблон данных JSON", "*.json"), filterIndex);
        }

        public static ReConfirmer Template(string defaultPath, byte filterIndex = 1)
        {
            return Choose(defaultPath, Option("Документ Microsoft Word", "*.docx"), filterIndex);
        }

        private static ReConfirmer Choose(string defaultPath, string filter, byte filterIndex)
        {
            OpenDialog dialog = new OpenDialog
            {
                Title = "Выберите шаблон данных",
                InitialDirectory = defaultPath,
                Filter = filter,
                FilterIndex = filterIndex
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