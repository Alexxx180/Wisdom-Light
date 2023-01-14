using System.Collections.ObjectModel;
using WisdomLight.ViewModel.Data.Collections;

namespace WisdomLight.ViewModel.Data.Files.Fields.Tools.Editors
{
    public class DefendingEditor<TData, TEditor> : NotifyPropertyChanged
    {
        public DefendingEditor(ObservableCollection<TData> data,
            EditableCollection<TEditor> editing)
        {
            Data = data;
            Editing = editing;
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
    }
}
