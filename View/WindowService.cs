using System.Windows;

namespace WisdomLight.View
{
    internal class WindowService : IWindowService
    {
        public void ShowWindow(object viewModel)
        {
            MainWindow window = new MainWindow()
            {
                Content = viewModel,
                //DataContext = viewModel
            };
            window.Show();
        }
    }
}
