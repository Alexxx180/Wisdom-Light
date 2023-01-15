using System;
using System.Collections;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using WisdomLight.View.Binds.Attach.Selection.Synchronization.Lists.Actions;
using WisdomLight.View.Binds.Attach.Selection.Synchronization.Lists;
using WisdomLight.View.Binds.Converters.Items;

namespace WisdomLight.View.Binds.Attach.Selection.Synchronization
{
    internal class SynchronizationManager
    {
        private readonly Selector _multiSelector;
        private ISynchronization _synchronizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizationManager"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        internal SynchronizationManager(Selector selector)
        {
            _multiSelector = selector;
        }

        /// <summary>
        /// Starts synchronizing the list.
        /// </summary>
        public void StartSynchronizingList()
        {
            IList list = MultiSelection.GetSyncSelectedItems(_multiSelector);

            if (list != null)
            {
                _synchronizer = new ListSynchronizer(GetSelectedItemsCollection(_multiSelector), list,
                    new ListsActions(), new ListsConverting(new NoConverter()));
                _synchronizer.StartSynchronizing();
            }
        }

        /// <summary>
        /// Stops synchronizing the list.
        /// </summary>
        public void StopSynchronizing()
        {
            _synchronizer.StopSynchronizing();
        }

        public static IList GetSelectedItemsCollection(Selector selector)
        {
            if (selector is MultiSelector)
            {
                return (selector as MultiSelector).SelectedItems;
            }
            else if (selector is ListBox)
            {
                return (selector as ListBox).SelectedItems;
            }
            else
            {
                throw new InvalidOperationException("Target object has no SelectedItems property to bind.");
            }
        }
    }
}
