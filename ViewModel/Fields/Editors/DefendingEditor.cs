using System.Collections.ObjectModel;

namespace WisdomLight.ViewModel.Fields.Editors
{
    public class DefendingEditor<TData, TEditor> : NotifyPropertyChanged
    {
        public DefendingEditor(
            ObservableCollection<TData> data,
            EditableCollection<TEditor> editing)
        {
            Data = data;
            Editing = editing;
            IsDefended = false;
        }

        public void Switch()
        {
            IsDefended = !IsDefended;
        }

        private ObservableCollection<TData> _data;
        public ObservableCollection<TData> Data
        {
            get => _data;
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        private EditableCollection<TEditor> _editing;
        public EditableCollection<TEditor> Editing
        {
            get => _editing;
            set
            {
                _editing = value;
                OnPropertyChanged();
            }
        }

        private bool _isDefended;
        public bool IsDefended
        {
            get => _isDefended;
            set
            {
                _isDefended = value;
                OnPropertyChanged();
            }
        }
    }
}
