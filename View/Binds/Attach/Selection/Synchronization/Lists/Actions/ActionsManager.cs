using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using WisdomLight.View.Binds.Attach.Selection.Synchronization.Lists.Actions;

namespace WisdomLight.View.Binds.Attach.Selection.Synchronization.Lists
{
    public class ActionsManager
    {
        private protected readonly IList _masterList;
        private protected readonly IList _targetList;

        private protected delegate void ChangeListAction(IList list,
            NotifyCollectionChangedEventArgs e, Converter<object, object> converter);

        private protected readonly ListsConverting _converter;
        private protected readonly ListListener _listener;
        private readonly ListsActions _actions;

        public ActionsManager(IList master, IList target,
            ListsActions actions, ListsConverting converter)
        {
            _masterList = master;
            _targetList = target;
            _actions = actions;
            _converter = converter;
            _listener = new ListListener(DetermineChangeAction);
        }

        private protected void DetermineChangeAction(object sender, NotifyCollectionChangedEventArgs e)
        {
            IList sourceList = sender as IList;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    PerformActionOnAllLists(_actions.AddItems, sourceList, e);
                    break;
                case NotifyCollectionChangedAction.Move:
                    PerformActionOnAllLists(_actions.MoveItems, sourceList, e);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    PerformActionOnAllLists(_actions.RemoveItems, sourceList, e);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    PerformActionOnAllLists(_actions.ReplaceItems, sourceList, e);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    UpdateListsFromSource(sourceList);
                    break;
                default:
                    break;
            }
        }

        private protected void PerformActionOnAllLists(ChangeListAction action, IList sourceList, NotifyCollectionChangedEventArgs collectionChangedArgs)
        {
            if (sourceList == _masterList)
            {
                PerformActionOnList(_targetList, action, collectionChangedArgs, _converter.ConvertFromMasterToTarget);
            }
            else
            {
                PerformActionOnList(_masterList, action, collectionChangedArgs, _converter.ConvertFromTargetToMaster);
            }
        }

        private protected void PerformActionOnList(IList list, ChangeListAction action, NotifyCollectionChangedEventArgs collectionChangedArgs, Converter<object, object> converter)
        {
            _listener.StopListeningForChangeEvents(list);
            action(list, collectionChangedArgs, converter);
            _listener.ListenForChangeEvents(list);
        }

        /// <summary>
        /// Makes sure that all synchronized lists
        /// have the same values as the source list.
        /// </summary>
        /// <param name="sourceList">The source list.</param>
        private protected void UpdateListsFromSource(IList sourceList)
        {
            if (sourceList == _masterList)
            {
                _listener.SetListValuesFromSource(_masterList, _targetList, _converter.ConvertFromMasterToTarget);
            }
            else
            {
                _listener.SetListValuesFromSource(_targetList, _masterList, _converter.ConvertFromTargetToMaster);
            }
        }

        /// <summary>
        /// Check case when the target list might have
        /// its own view on which items should included
        /// </summary>
        /// <returns>
        /// - true, if lists are equal
        /// - false, otherwise
        /// </returns>
        private protected bool TargetAndMasterCollectionsAreEqual()
        {
            return _masterList.Cast<object>().SequenceEqual(_targetList.Cast<object>().Select(item => _converter.ConvertFromTargetToMaster(item)));
        }
    }
}
