using System;
using WisdomLight.Model;
using WisdomLight.ViewModel.Components.Core.Commands;

namespace WisdomLight.ViewModel.Components.Core.Dialogs
{
    public class DialogService<T> : IDialogService<T> where T : ICloser
    {
        public virtual void ShowDialog(T viewModel, Action<bool, T> callback)
        {
            Dialog(viewModel, callback);
        }

        public static void Dialog(T viewModel, Action<bool, T> callback)
        {
            DialogWindow dialog = new DialogWindow();
            viewModel.Close = () => dialog.Close();
            viewModel.CanClose = true;
            viewModel.CloseCommand = new RelayCommand(
                argument =>
                {
                    dialog.DialogResult = true;
                    dialog.Close();
                }
            );

            dialog.Content = viewModel;

            EventHandler close = null;
            close = (s, e) =>
            {
                callback(dialog.DialogResult.Value, viewModel);
                dialog.Closed -= close;
            };
            dialog.Closed += close;
            _ = dialog.ShowDialog();
        }
    }
}
