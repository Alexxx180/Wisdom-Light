using System;
using WisdomLight.ViewModel.Components.Core.Commands;
using WisdomLight.ViewModel.Components.Data;

namespace WisdomLight.ViewModel.Components.Core.Dialogs
{
    public class DependenciesDialog : IDialogService<DependenciesViewModel>
    {
        public void ShowDialog(DependenciesViewModel dependencies,
            Action<bool, DependenciesViewModel> callback)
        {
            Dialog(dependencies, callback);
        }

        private static void Dialog(DependenciesViewModel dependencies,
            Action<bool, DependenciesViewModel> callback)
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
