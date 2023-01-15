using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WisdomLight.ViewModel.Data.Collections
{
    public class EditableCollection<T> : NotifyPropertyChanged
    {
        public EditableCollection() { }

        public EditableCollection(ObservableCollection<T> fields,
            List<T> selectedItems,
            ICommand add, ICommand drop)
        {
            Fields = fields;
            AddCommand = add;
            DropCommand = drop;
            SelectedItems = selectedItems;
        }

        private ObservableCollection<T> _fields;
        public ObservableCollection<T> Fields
        {
            get => _fields;
            set
            {
                _fields = value;
                OnPropertyChanged();
            }
        }

        private List<T> _selectedItems;
        public List<T> SelectedItems
        {
            get => _selectedItems;
            set
            {
                _selectedItems = value;
                OnPropertyChanged();
            }
        }

        public void Remove(T field)
        {
            _ = Fields.Remove(field);
        }

        public void Add(T field)
        {
            Fields.Add(field);
        }

        public ICommand AddCommand { get; }
        public ICommand DropCommand { get; }
    }
}
