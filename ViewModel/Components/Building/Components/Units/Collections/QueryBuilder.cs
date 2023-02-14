using WisdomLight.ViewModel.Components.Building.Collections;
using WisdomLight.ViewModel.Components.Data;
using WisdomLight.ViewModel.Components.Data.Units;
using WisdomLight.ViewModel.Components.Data.Units.Collections;

namespace WisdomLight.ViewModel.Components.Building.Components.Units.Collections
{
    public class QueryBuilder : EditableBuilder<Querier>
    {
        private RouterCollection _editables;
        private DependenciesViewModel _dependencies;

        public override RouterCollection Build()
        {
            _editables = new RouterCollection
            {
                Fields = _fields,
                SelectedItems = _selectedItems,
                ViewModel = _dependencies
            };
            return _editables;
        }

        public override QueryBuilder Reset()
        {
            _ = base.Reset();
            return this;
        }

        public QueryBuilder SetDependencies(DependenciesViewModel dependencies)
        {
            _dependencies = dependencies;
            return this;
        }

        public override QueryBuilder Fields()
        {
            _ = base.Fields();
            return this;
        }

        public override QueryBuilder SelectedItems()
        {
            _ = base.SelectedItems();
            return this;
        }

        public override QueryBuilder Additor(Querier item)
        {
            _ = base.Additor(item);
            return this;
        }
    }
}
