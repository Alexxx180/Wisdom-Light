using System;

namespace WisdomLight.ViewModel.Components.Core.Dialogs
{
    public interface IDialogService<T>
    {
        public void ShowDialog(T viewModel, Action<bool, T> result);
    }
}
