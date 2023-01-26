using System.Collections;
using System.Windows;
using System.Windows.Controls.Primitives;
using WisdomLight.View.Binds.Attach.Selection.Synchronization;

namespace WisdomLight.View.Binds.Attach.Selection
{
    public static class MultiSelection
    {
        public static readonly DependencyProperty
            SyncSelectedItems = DependencyProperty.RegisterAttached(
            nameof(SyncSelectedItems), typeof(IList), typeof(MultiSelection),
            new PropertyMetadata(null, OnSyncSelectedItemsChanged));

        private static readonly DependencyProperty
            SynchronizationManagerProperty = DependencyProperty.RegisterAttached(
            nameof(SynchronizationManager), typeof(SynchronizationManager),
            typeof(MultiSelection), new PropertyMetadata(null));


        #region Synchronized SelectedItems Members
        /// <summary>
        /// Gets the synchronized selected items.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns>The list that is acting as the sync list.</returns>
        public static IList GetSyncSelectedItems(DependencyObject dependencyObject)
        {
            return (IList)dependencyObject.GetValue(SyncSelectedItems);
        }

        /// <summary>
        /// Sets the synchronized selected items.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="value">The value to be set as synchronized items.</param>
        public static void SetSyncSelectedItems(DependencyObject dependencyObject, IList value)
        {
            dependencyObject.SetValue(SyncSelectedItems, value);
        }
        #endregion


        #region SynchronizationManager Members
        private static SynchronizationManager GetSynchronizationManager(DependencyObject dependencyObject)
        {
            return (SynchronizationManager)dependencyObject.GetValue(SynchronizationManagerProperty);
        }

        private static void SetSynchronizationManager(DependencyObject dependencyObject, SynchronizationManager value)
        {
            dependencyObject.SetValue(SynchronizationManagerProperty, value);
        }
        #endregion


        #region Synchronized Items Changed
        /// <summary>
        /// Check if property is being set on a ListBox
        /// </summary>
        private static void SynchronizeLists(DependencyObject dependencyObject)
        {
            if (dependencyObject is not Selector selector)
                return;

            SynchronizationManager synchronizer = GetSynchronizationManager(dependencyObject);
            if (synchronizer == null)
            {
                synchronizer = new SynchronizationManager(selector);
                SetSynchronizationManager(dependencyObject, synchronizer);
            }

            synchronizer.StartSynchronizingList();
        }

        private static void OnSyncSelectedItemsChanged
            (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != null)
            {
                SynchronizationManager synchronizer = GetSynchronizationManager(dependencyObject);
                synchronizer.StopSynchronizing();

                SetSynchronizationManager(dependencyObject, null);
            }

            if ((e.NewValue as IList) != null)
                SynchronizeLists(dependencyObject);
        }
        #endregion
    }
}
