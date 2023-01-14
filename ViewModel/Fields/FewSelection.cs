using System.Collections.Generic;
using System.Windows.Input;

namespace WisdomLight.ViewModel.Fields
{
    public class FewSelection<T> : NotifyPropertyChanged
    {
        public FewSelection() { }

        public FewSelection(ICommand add, ICommand drop)
        {
            AddCommand = add;
            DropCommand = drop;
        }

        public FewSelection(EditableCollection<T> items,
            List<T> selectedItems, ICommand add, ICommand drop)
        {
            Items = items;
            SelectedItems = selectedItems;
            AddCommand = add;
            DropCommand = drop;
        }

        private EditableCollection<T> _items;
        public EditableCollection<T> Items
        {
            get => _items;
            set
            {
                _items = value;
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

        public ICommand AddCommand { get; set; }
        public ICommand DropCommand { get; set; }
    }
}
