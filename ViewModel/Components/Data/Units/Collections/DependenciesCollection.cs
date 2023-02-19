using System.Collections.Generic;
using System.Collections.ObjectModel;
using WisdomLight.Model;
using WisdomLight.ViewModel.Components.Data.Units;

namespace WisdomLight.ViewModel.Components.Core.Dialogs
{
    public class DependenciesCollection : ObservableCollection<DependenciesNode>, ICloneable<DependenciesCollection>
    {
        public new void Add(DependenciesNode node)
        {
            node.No = Count;
            base.Add(node);
        }

        public new void Remove(DependenciesNode node)
        {
            for (int i = node.No; i < Count; i++)
                this[i].No--;

            base.Remove(node);
        }

        public IEnumerable<string> GetNames()
        {
            for (int i = 0; i < Count; i++)
                yield return this[i].Name;
        }

        public DependenciesCollection Clone()
        {
            DependenciesCollection nodes = new DependenciesCollection();
            for (int i = 0; i < Count; i++)
            {
                nodes.Add(this[i].RecurseClone());
            }
            return nodes;
        }
    }
}
