using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WisdomLight.ViewModel
{
    public class EditableCollection<T> : NotifyPropertyChanged
    {
        public EditableCollection() { }

        public EditableCollection(ObservableCollection<T> fields,
            T addendum, ICommand add, ICommand drop)
        {
            Fields = fields;
            Addendum = addendum;
            AddCommand = add;
            DropCommand = drop;
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

        private T _addendum;
        public T Addendum
        {
            get => _addendum;
            set
            {
                _addendum = value;
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
