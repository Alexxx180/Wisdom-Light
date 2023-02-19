using Serilog;
using System.Windows;
using WisdomLight.Model.Exceptions;

namespace WisdomLight.ViewModel.Components.Core.Processors
{
    public static class Messages
    {
        private static MessageBoxResult Message(string message,
            string title, MessageBoxButton buttons, MessageBoxImage icon)
        {
            Log.Error(message);
            return MessageBox.Show(message, title, buttons, icon);
        }

        public static MessageBoxResult Error(IDetails exception)
        {
            return Message(exception.Details(), nameof(Error), MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
