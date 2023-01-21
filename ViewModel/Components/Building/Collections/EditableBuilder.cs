using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WisdomLight.Model;
using WisdomLight.ViewModel.Commands;
using WisdomLight.ViewModel.Data.Collections;

namespace WisdomLight.ViewModel.Data.Files.Fields.Tools.Building.Filler.Collections
{
    public class EditableBuilder<T> : IEditableBuilder<T> where T : ICloneable<T>
    {
        private EditableCollection<T> _editables;
        private ObservableCollection<T> _fields;
        private List<T> _selectedItems;

        public T _additor;

        private ICommand _addCommand;
        private ICommand _dropCommand;

        public EditableCollection<T> Build()
        {
            _editables = new EditableCollection<T>
            {
                Fields = _fields,
                SelectedItems = _selectedItems,
                AddCommand = _addCommand,
                DropCommand = _dropCommand
            };
            return _editables;
        }

        public IEditableBuilder<T> Reset()
        {
            _addCommand = null;
            _dropCommand = null;
            _selectedItems = null;
            _fields = null;
            return this;
        }

        public IEditableBuilder<T> Add()
        {
            _addCommand = new RelayCommand(argument => _editables.Add(_additor));
            return this;
        }

        public IEditableBuilder<T> Drop()
        {
            _dropCommand = new RelayCommand(argument => _editables.RemoveSelected());
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
