using System;
using System.Windows;
using Serilog;
using WisdomLight.ViewModel.Components.Building.Main;
using WisdomLight.ViewModel.Data.Files.Processors;
using WisdomLight.ViewModel.Data.Files.Processors.Serialization;

namespace WisdomLight
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static readonly FileProcessor[] Processors;
        public static string DefaultLocation => Environment.CurrentDirectory + @"\Resources\Runtime\";
        
        static App()
        {
            Log.Debug($"Runtime directory: {DefaultLocation}");
            Processors = new FileProcessor[]
            {
                new JsonProcessor()
            };
        }

        public App()
        {
            _mainBuilder = new MainBuilder();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Application started");
            Log.Debug("Collecting configuration info...");

            new MainWindow
            {
                ViewModel = _mainBuilder.Preferences().NewFile().Open().CanClose().Close().Build()
            }
            .Show();
        }

        private IMainBuilder _mainBuilder;
    }
}