using WisdomLight.Model;
using WisdomLight.Model.Results.Confirming;
using static WisdomLight.ViewModel.Components.Building.Extensions.Decorators.Filters;

namespace WisdomLight.ViewModel.Components.Core.Dialogs.Traditional.Manager
{
    public class DocumentManager
    {
        private static readonly string _filter;
        private const string OPEN = "Выберите документ";
        private const string EXPORT = "Экспортировать указанные документы в...";

        static DocumentManager()
        {
            _filter = Option("Документ Microsoft Word", "*.docx");
        }

        public static ReConfirmer Open(string initial, byte type = 1)
        {
            return DialogManager.Open(initial, OPEN, _filter, type);
        }

        public static Confirmer Export()
        {
            return DialogManager.Export(EXPORT);
        }
    }
}
