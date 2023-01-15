using WisdomLight.View.Binds.Converters.Items;

namespace WisdomLight.View.Binds.Attach.Selection.Synchronization.Lists
{
    public class ListsConverting
    {
        private readonly IItemConverter _masterTargetConverter;

        public ListsConverting(IItemConverter masterTargetConverter)
        {
            _masterTargetConverter = masterTargetConverter;
        }

        protected internal object ConvertFromMasterToTarget(object masterListItem)
        {
            return _masterTargetConverter == null ?
                masterListItem : _masterTargetConverter.Convert(masterListItem);
        }

        protected internal object ConvertFromTargetToMaster(object targetListItem)
        {
            return _masterTargetConverter == null ?
                targetListItem : _masterTargetConverter.ConvertBack(targetListItem);
        }
    }
}
