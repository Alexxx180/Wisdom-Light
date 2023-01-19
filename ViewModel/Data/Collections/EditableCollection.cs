using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WisdomLight.ViewModel.Data.Files.Fields.Tools;

namespace WisdomLight.ViewModel.Data.Collections
{
    public class EditableCollection<T> : NotifyPropertyChanged
    {
        public EditableCollection() { }

        public EditableCollection(ObservableCollection<T> fields,
            List<T> selectedItems, EditCommands tools)
        {
            Fields = fields;
            Tools = tools;
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

        public void RemoveSelected()
        {
            int count = SelectedItems.Count;
            while (count > 0)
            {
                _ = Fields.Remove(SelectedItems[0]);
                count--;
            }
        }

        public void Add(T field)
        {
            Fields.Add(field);
        }

        public EditCommands Tools { get; }
    }
}
