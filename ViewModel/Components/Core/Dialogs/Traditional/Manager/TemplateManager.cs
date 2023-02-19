using WisdomLight.Model;
using WisdomLight.Model.Results.Confirming;
using static WisdomLight.ViewModel.Components.Building.Extensions.Decorators.Filters;

namespace WisdomLight.ViewModel.Components.Core.Dialogs.Traditional.Manager
{
    public static class TemplateManager
    {
        private static readonly string _filter;
        private const string OPEN = "Выберите бланк";
        private const string SAVE = "Сохранить бланк как...";

        static TemplateManager()
        {
            _filter = Option("Шаблон данных JSON", "*.json");
        }

        public static ReConfirmer Open(string initial, byte type = 1)
        {
            return DialogManager.Open(initial, OPEN, _filter, type);
        }

        public static KeyConfirmer Save(string initial, string name, byte filterIndex = 1)
        {
            return DialogManager.Save(initial, SAVE, name, _filter, filterIndex);
        }
    }
}
