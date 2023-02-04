using System.Windows;
using WisdomLight.View;
using WisdomLight.ViewModel.Components;
using WisdomLight.ViewModel.Components.Building.Main;
using static WisdomLight.ViewModel.Components.Building.Bank.Setup;

namespace WisdomLight
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IWindowService _windows;

        public App()
        {
            _windows = new WindowService();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            Logger();

            MainViewModel viewModel = new MainBuilder().Preferences().NewFile().Open().CanClose().Close().Build();
            _windows.ShowWindow(viewModel);
            //new MainWindow
            //{
            //    ViewModel = new MainBuilder().Preferences().NewFile().Open().CanClose().Close().Build()
            //}
            //.Show();
        }

        private void Proof()
        {
            
        }
    }
}