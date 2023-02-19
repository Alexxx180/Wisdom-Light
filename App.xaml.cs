using System.IO;
using System.IO.Compression;
using System.Windows;
using WisdomLight.View;
using WisdomLight.ViewModel.Components;
using WisdomLight.ViewModel.Components.Building.Bank;
using WisdomLight.ViewModel.Components.Building.Main;
using WisdomLight.ViewModel.Components.Core.Dialogs;
using WisdomLight.ViewModel.Components.Data;
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
            IDialogService<DependenciesViewModel> dialog = new DependenciesDialog();
            MainViewModel viewModel = new MainBuilder(windows, dialog).Preferences().NewFile().Open().Save().CanClose().Close().
                AddInformation().DropInformation().AddLink().DropLink().OpenDependency().RenameDependency().Export().Build();
            windows.ShowWindow(viewModel);

            //string dependencies = Path.Combine(Defaults.Runtime, "lol.zip");
            //using (ZipArchive zip = ZipFile.Open(dependencies, ZipArchiveMode.Create))
            //{
            //    zip.CreateEntry("lol/2/aga.txt");
            //}
        }
    }
}