using System.Collections.Generic;
using System.Collections.ObjectModel;
using WisdomLight.Model;
using WisdomLight.ViewModel.Components.Data.Units.Collections;

namespace WisdomLight.ViewModel.Components.Building.Collections
{
    public class EditableBuilder<T> : IEditableBuilder<T> where T : ICloneable<T>
    {
        private EditableCollection<T> _editables;
        private protected ObservableCollection<T> _fields;
        private protected List<T> _selectedItems;

        public T _additor;

        public virtual EditableCollection<T> Build()
        {
            _editables = new EditableCollection<T>
            {
                Fields = _fields,
                SelectedItems = _selectedItems
            };
            return _editables;
        }

        public virtual IEditableBuilder<T> Reset()
        {
            _selectedItems = null;
            _fields = null;
            return this;
        }

        public virtual IEditableBuilder<T> Fields()
        {
            _fields = new ObservableCollection<T>();
            return this;
        }

        public virtual IEditableBuilder<T> SelectedItems()
        {
            _selectedItems = new List<T>();
            return this;
        }

        public virtual IEditableBuilder<T> Additor(T item)
        {
            _additor = item;
            return this;
        }
    }
}
