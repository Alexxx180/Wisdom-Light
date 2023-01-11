using System.Collections.ObjectModel;
using WisdomLight.ViewModel.Fields;
using WisdomLight.ViewModel.Fields.Editors;
using WisdomLight.ViewModel.Commands;
using System.Collections.Generic;

namespace WisdomLight.ViewModel
{
    public class FileViewModel : NotifyPropertyChanged
    {
        #region Documents
        private DefendingEditor<string, string> _documents;
        public DefendingEditor<string, string> Documents
        {
            get => _documents;
            set
            {
                _documents = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Information Members
        private DefendingEditor<IExpression, FieldSelector> _information;
        public DefendingEditor<IExpression, FieldSelector> Information
        {
            get => _information;
            set
            {
                _information = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Auto Save Logic
        private bool _isChanged;
        public bool IsChanged
        {
            get => _isChanged;
            set
            {
                _isChanged = value;
                OnPropertyChanged();
            }
        }

        public bool WasChanged()
        {
            bool memory = IsChanged;
            IsChanged = false;
            return memory;
        }
        #endregion

        public FileViewModel(List<IExpression> expressions)
        {
            ObservableCollection<string> paths = new ObservableCollection<string>();
            ObservableCollection<string> pathEditing = new ObservableCollection<string>();

            Documents = new DefendingEditor<string, string>(
                paths, new EditableCollection<string>(
                    pathEditing, "",
                    new RelayCommand(
                        argument =>
                        {
                            string item = (string)argument;
                            paths.Add(item);
                            pathEditing.Add(item);
                        }
                    ),
                    new RelayCommand(
                        argument =>
                        {
                            string item = (string)argument;
                            paths.Remove(item);
                            pathEditing.Remove(item);
                        }
                    )
                )
            );


            ObservableCollection<IExpression> fields = new ObservableCollection<IExpression>();
            ObservableCollection<FieldSelector> fieldsEditing = new ObservableCollection<FieldSelector>();

            Information = new DefendingEditor<IExpression, FieldSelector>(
                fields, new EditableCollection<FieldSelector>(
                    fieldsEditing, new FieldSelector(expressions),
                    new RelayCommand(
                        argument =>
                        {
                            FieldSelector item = (FieldSelector)argument;
                            fields.Add(item.Source);
                            fieldsEditing.Add(item);
                        }
                    ),
                    new RelayCommand(
                        argument =>
                        {
                            FieldSelector item = (FieldSelector)argument;
                            _ = fields.Remove(item.Source);
                            _ = fieldsEditing.Remove(item);
                        }
                    )
                )
            );
        }
    }
}