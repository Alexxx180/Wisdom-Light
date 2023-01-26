using Serilog;
using static WisdomLight.ViewModel.Components.Building.Bank.Defaults;

namespace WisdomLight.ViewModel.Components.Building.Bank
{
    public static class Setup
    {
        public static void Logger()
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day).CreateLogger();

            Log.Information("- Wisdom-Light -");
            Log.Information("Making your day.");
            Log.Debug($"Last run: '{Runtime}'");
        }
    }
}
