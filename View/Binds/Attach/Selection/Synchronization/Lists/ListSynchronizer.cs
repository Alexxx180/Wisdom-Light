using System.Collections;
using WisdomLight.View.Binds.Attach.Selection.Synchronization.Lists.Actions;
using WisdomLight.View.Binds.Converters.Items;

namespace WisdomLight.View.Binds.Attach.Selection.Synchronization.Lists
{
    public class ListSynchronizer : ActionsManager, ISynchronization
    {
        public ListSynchronizer(IList master, IList target,
            ListsActions actions, ListsConverting converting)
            : base(master, target, actions, converting) { }

        public ListSynchronizer(IList master, IList target, ListsActions actions)
            : this(master, target, actions, new ListsConverting(new NoConverter())) { }

        /// <summary>
        /// Starts synchronizing the lists.
        /// </summary>
        public void StartSynchronizing()
        {
            _listener.ListenForChangeEvents(_masterList);
            _listener.ListenForChangeEvents(_targetList);
            _listener.SetListValuesFromSource(_masterList, _targetList, _converter.ConvertFromMasterToTarget);

            // This is the case with a ListBox SelectedItems collection:
            // only items from the ItemsSource can be included in SelectedItems
            if (!TargetAndMasterCollectionsAreEqual())
            {
                _listener.SetListValuesFromSource(_targetList, _masterList, _converter.ConvertFromTargetToMaster);
            }
        }

        /// <summary>
        /// Stop synchronizing the lists.
        /// </summary>
        public void StopSynchronizing()
        {
            _listener.StopListeningForChangeEvents(_masterList);
            _listener.StopListeningForChangeEvents(_targetList);
        }
    }
}
