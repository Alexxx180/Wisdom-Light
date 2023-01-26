using System.Collections.Generic;
using System.Collections.ObjectModel;
using WisdomLight.Model;
using WisdomLight.ViewModel.Components.Data.Units.Collections;

namespace WisdomLight.ViewModel.Components.Building.Collections
{
    public class EditableBuilder<T> : IEditableBuilder<T> where T : ICloneable<T>
    {
        private EditableCollection<T> _editables;
        private ObservableCollection<T> _fields;
        private List<T> _selectedItems;

        public T _additor;

        public EditableCollection<T> Build()
        {
            _editables = new EditableCollection<T>
            {
                Fields = _fields,
                SelectedItems = _selectedItems
            };
            return _editables;
        }

        public IEditableBuilder<T> Reset()
        {
            _selectedItems = null;
            _fields = null;
            return this;
        }

        public IEditableBuilder<T> Fields()
        {
            _fields = new ObservableCollection<T>();
            return this;
        }

        public IEditableBuilder<T> SelectedItems()
        {
            _selectedItems = new List<T>();
            return this;
        }

        public IEditableBuilder<T> Additor(T item)
        {
            _additor = item;
            return this;
        }
    }
}
