using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WisdomLight.Model;

namespace WisdomLight.ViewModel.Data.Collections
{
    public class EditableCollection<T> : NotifyPropertyChanged where T : ICloneable<T>
    {
        public EditableCollection() { }

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
            Fields.Add(field.Clone());
        }

        public ICommand AddCommand { get; protected internal set; }
        public ICommand DropCommand { get; protected internal set; }
    }
}
