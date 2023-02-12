using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WisdomLight.ViewModel.Components.Data.Units;

namespace WisdomLight.ViewModel.Components.Core.Dialogs
{
    public class DependenciesCollection : ObservableCollection<DependenciesNode>
    {
        private DependenciesNode _parent;

        public DependenciesCollection()
        {
            _parent = null;
        }

        public DependenciesCollection(DependenciesNode parent)
        {
            _parent = parent;
        }

        public new void Add(DependenciesNode node)
        {
            node.Parent = _parent;
            node.No = Count;
            base.Add(node);
        }

        public new void Remove(DependenciesNode node)
        {
            for (int i = node.No; i < Count; i++)
                this[i].No--;

            base.Remove(node);
        }
    }
}
