using System;
using System.Collections;
using System.Collections.Specialized;
using System.Windows;

namespace WisdomLight.View.Binds.Attach.Selection.Synchronization.Lists
{
    public class ListListener : IWeakEventListener
    {
        public delegate void CollectionChangedAction(object sender, NotifyCollectionChangedEventArgs e);
        
        private CollectionChangedAction CollectionChanged { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListListener"/> class.
        /// </summary>
        /// <param name="collectionChanged">Method to call on event.</param>
        public ListListener(CollectionChangedAction collectionChanged)
        {
            CollectionChanged = collectionChanged;
        }

        /// <summary>
        /// Receives events from the centralized event manager.
        /// </summary>
        /// <param name="managerType">
        /// The type of the
        /// <see cref="WeakEventManager"/>
        /// calling this method.
        /// </param>
        /// <param name="sender">Object that originated the event.</param>
        /// <param name="e">Event data.</param>
        /// <returns>
        /// - true, if the listener handled the event.
        /// - false, if it receives an event that it does not recognize or handle.
        /// It is considered an error by the <see cref="WeakEventManager"/>
        /// handling in WPF to register a listener for an event that the listener does not handle.
        /// </returns>
        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            HandleCollectionChanged(sender as IList, e as NotifyCollectionChangedEventArgs);
            return true;
        }

        /// <summary>
        /// Listens for change events on a list.
        /// </summary>
        /// <param name="list">The list to listen to.</param>
        protected internal void ListenForChangeEvents(IList list)
        {
            if (list is INotifyCollectionChanged)
            {
                CollectionChangedEventManager.AddListener(list as INotifyCollectionChanged, this);
            }
        }

        /// <summary>
        /// Stops listening for change events.
        /// </summary>
        /// <param name="list">The list to stop listening to.</param>
        protected internal void StopListeningForChangeEvents(IList list)
        {
            if (list is INotifyCollectionChanged)
            {
                CollectionChangedEventManager.RemoveListener(list as INotifyCollectionChanged, this);
            }
        }

        private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged(sender, e);
        }

        /// <summary>
        /// Update the Target list from the Master list
        /// </summary>
        /// <param name="source">Source list</param>
        /// <param name="target">Target list</param>
        /// <param name="converter">Item converter</param>
        protected internal void SetListValuesFromSource(IList source, IList target, Converter<object, object> converter)
        {
            StopListeningForChangeEvents(target);

            target.Clear();
            foreach (object o in source)
            {
                target.Add(converter(o));
            }

            ListenForChangeEvents(target);
        }
    }
}
