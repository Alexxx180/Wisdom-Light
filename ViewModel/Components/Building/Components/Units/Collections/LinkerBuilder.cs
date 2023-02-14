using WisdomLight.ViewModel.Components.Building.Collections;
using WisdomLight.ViewModel.Components.Data.Units;
using WisdomLight.ViewModel.Components.Data.Units.Collections;

namespace WisdomLight.ViewModel.Components.Building.Components.Units.Collections
{
    public class LinkerBuilder : EditableBuilder<DocumentLinker>
    {
        private LinkerList _editables;

        public override LinkerList Build()
        {
            _editables = new LinkerList
            {
                Fields = _fields,
                SelectedItems = _selectedItems
            };
            return _editables;
        }

        public override LinkerBuilder Reset()
        {
            _ = base.Reset();
            return this;
        }

        public override LinkerBuilder Fields()
        {
            _ = base.Fields();
            return this;
        }

        public override LinkerBuilder SelectedItems()
        {
            _ = base.SelectedItems();
            return this;
        }

        public override LinkerBuilder Additor(DocumentLinker item)
        {
            _ = base.Additor(item);
            return this;
        }
    }
}
