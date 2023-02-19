using WisdomLight.Model;
using WisdomLight.Model.Results.Confirming;
using static WisdomLight.ViewModel.Components.Building.Extensions.Decorators.Filters;

namespace WisdomLight.ViewModel.Components.Core.Dialogs.Traditional.Manager
{
    public static class ProjectManager
    {
        private static readonly string _filter;
        private const string IMPORT = "Имортировать конфигурацию";
        private const string EXPORT = "Экспортировать конфигурацию";

        static ProjectManager()
        {
            _filter = Option("Зависимости - ZIP", "*.zip");
        }

        public static ReConfirmer Open(string initial, byte type = 1)
        {
            return DialogManager.Open(initial, IMPORT, _filter, type);
        }

        public static KeyConfirmer Export(string initial, string name, byte type = 1)
        {
            return DialogManager.Save(initial, EXPORT, name, _filter, type);
        }
    }
}
