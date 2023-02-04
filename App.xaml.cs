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
        private void OnStartup(object sender, StartupEventArgs e)
        {
            Logger();

            IWindowService windows = new WindowService();
            MainViewModel viewModel = new MainBuilder(windows).Preferences().NewFile().Open().CanClose().Close().Build();
            windows.ShowWindow(viewModel);
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