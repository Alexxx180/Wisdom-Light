using System;
using System.Windows;
using Serilog;
using WisdomLight.ViewModel.Data.Files.Processors;
using WisdomLight.ViewModel.Data.Files.Processors.Serialization;
using WisdomLight.ViewModel.Data.Files.Processors.Serialization.Objects;

namespace WisdomLight
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static readonly FileProcessor[] Processors;
        public static string Runtime => Environment.CurrentDirectory + @"\Resources\Runtime\";

        static App()
        {
            Log.Debug($"Runtime directory: {Runtime}");
            Processors = new FileProcessor[]
            {
                new JsonProcessor()
            };
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Application started");
            Log.Debug("Collecting configuration info...");
        }
    }
}