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

            //FewSelection<string> documentSelection = new FewSelection<string>();
            //documentSelection.SelectedItems = new List<string>();
            //documentSelection.Items = new EditableCollection<string>(
            //    pathEditing, "",
            //    new RelayCommand(
            //        argument => documentSelection.SelectedItems.Add((string)argument)
            //    ),
            //    new RelayCommand(
            //        argument => documentSelection.SelectedItems.Remove((string)argument)
            //    )
            //);
            //documentSelection.AddCommand = new RelayCommand(
            //    arg
            //);

            Documents = new DefendingEditor<string, string>(
                paths, new EditableCollection<string>(
                    pathEditing,
                    new RelayCommand(
                        argument =>
                        {
                            string path = "";
                            paths.Add(path);
                            pathEditing.Add(path);

                            //for (int i = 0; i < Documents.Editing.SelectedItems.Count; i++)
                            //{
                            //    paths.Add(Documents.Editing.SelectedItems[i]);
                            //    pathEditing.Add(Documents.Editing.SelectedItems[i]);
                            //}
                        }
                    ),
                    new RelayCommand(
                        argument =>
                        {
                            int count = Documents.Editing.SelectedItems.Count - 1;
                            while (count > -1)
                            {
                                paths.Remove(Documents.Editing.SelectedItems[count]);
                                pathEditing.Remove(Documents.Editing.SelectedItems[count]);
                                Documents.Editing.SelectedItems.RemoveAt(count);
                                count--;
                            }
                        }
                    )
                )
            );


            ObservableCollection<IExpression> fields = new ObservableCollection<IExpression>();
            ObservableCollection<FieldSelector> fieldsEditing = new ObservableCollection<FieldSelector>();

            Information = new DefendingEditor<IExpression, FieldSelector>(
                fields, new EditableCollection<FieldSelector>(
                    fieldsEditing,
                    new RelayCommand(
                        argument =>
                        {
                            FieldSelector field = new FieldSelector(
                                new List<IExpression>
                                {
                                    new TextExpression() { Type = "Текст" },
                                    new NumberExpression() { Type = "Число" },
                                    new DateExpression() { Type = "Дата" }
                                }
                            );
                            fields.Add(field.Source);
                            fieldsEditing.Add(field);
                            //for (int i = 0; i < Documents.Editing.SelectedItems.Count; i++)
                            //{
                            //    fields.Add(Documents.Editing.SelectedItems[i].Source);
                            //    fieldsEditing.Add(Documents.Editing.SelectedItems[i]);
                            //}
                        }
                    ),
                    new RelayCommand(
                        argument =>
                        {
                            int count = Information.Editing.SelectedItems.Count - 1;
                            while (count > -1)
                            {
                                fields.Remove(Information.Editing.SelectedItems[count].Source);
                                fieldsEditing.Remove(Information.Editing.SelectedItems[count]);
                                Documents.Editing.SelectedItems.RemoveAt(count);
                                count--;
                            }
                        }
                    )
                )
            );
        }
    }
}