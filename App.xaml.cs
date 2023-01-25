using System;
using System.Collections.Generic;
using System.Windows;
using Serilog;
using WisdomLight.ViewModel.Components.Building.Main;
using WisdomLight.ViewModel.Components.Core.Processors;
using WisdomLight.ViewModel.Components.Core.Processors.Export.Documents;
using WisdomLight.ViewModel.Components.Core.Processors.Serialization;
using WisdomLight.ViewModel.Components.Data.Units;

namespace WisdomLight
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string DefaultLocation => Environment.CurrentDirectory + @"\Resources\Runtime\";
        internal static readonly FileProcessor[] Processors;

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
            _wordDocument = new WordDocument();
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
                ViewModel = new MainBuilder().Preferences().NewFile().Open().CanClose().Close().Build()
            }
            .Show();

            //_wordDocument.Export(new List<DocumentLinker>
            //{
            //    new DocumentLinker
            //    {
            //        Name = "Lol", Type = @"D:\Aleksandr\misc\Development\Sandbox\Selderey.docx"
            //    }
            //}, null, @"D:\Aleksandr\misc\Development\Sandbox\a");
        }

        private WordDocument _wordDocument;
    }
}