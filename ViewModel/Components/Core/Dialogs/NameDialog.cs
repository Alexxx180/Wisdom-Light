using System;
using WisdomLight.ViewModel.Components.Core.Commands;
using WisdomLight.ViewModel.Components.Data;

namespace WisdomLight.ViewModel.Components.Core.Dialogs
{
    public class NameDialog : IDialogService<NameViewModel>
    {
        public void ShowDialog(NameViewModel dependencies,
            Action<bool, NameViewModel> callback)
        {
            Dialog(dependencies, callback);
        }

        private static void Dialog(NameViewModel dependencies,
            Action<bool, NameViewModel> callback)
        {
            DialogWindow dialog = new DialogWindow();
            dependencies.Close = () => dialog.Close();
            dependencies.CloseCommand = new RelayCommand(
                argument =>
                {
                    dialog.DialogResult = true;
                    dependencies.Close?.Invoke();
                }
            );
            dependencies.CanClose = true;

            dialog.Content = dependencies;

            EventHandler close = null;
            close = (s, e) =>
            {
                callback(dialog.DialogResult.Value, dependencies);
                dialog.Closed -= close;
            };
            dialog.Closed += close;
            _ = dialog.ShowDialog();
        }
    }
}
