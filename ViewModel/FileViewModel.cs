using System.Collections.ObjectModel;
using WisdomLight.ViewModel.Commands;
using System.Collections.Generic;
using WisdomLight.ViewModel.Files.Fields;
using WisdomLight.ViewModel.Data.Collections;
using WisdomLight.ViewModel.Data.Files.Fields;
using WisdomLight.ViewModel.Data.Files.Fields.Tools;
using WisdomLight.ViewModel.Data.Files.Fields.Tools.Editors;
using WisdomLight.Model;

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
        private DefendingEditor<Bridge<IExpression>, FieldSelector> _information;
        public DefendingEditor<Bridge<IExpression>, FieldSelector> Information
        {
            get => _information;
            set
            {
                _information = value;
                OnPropertyChanged();
            }
        }
        #endregion

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

        private string _originalName;

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        // Logic for selection
        // for (int i = 0; i < Documents.Editing.SelectedItems.Count; i++)
        // {
        //     paths.Add(Documents.Editing.SelectedItems[i]);
        //     pathEditing.Add(Documents.Editing.SelectedItems[i]);
        // }

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

        public FileViewModel(string fileName, List<IExpression> expressions)
        {
            _originalName = fileName;
            Name = fileName;

            ObservableCollection<string> paths = new ObservableCollection<string>();
            ObservableCollection<string> pathEditing = new ObservableCollection<string>();

            Documents = new DefendingEditor<string, string>(
                paths, new EditableCollection<string>(
                    pathEditing,
                    new List<string>(),
                    new RelayCommand(
                        argument =>
                        {
                            string path = "";
                            paths.Add(path);
                            pathEditing.Add(path);
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


            ObservableCollection<Bridge<IExpression>> fields = new ObservableCollection<Bridge<IExpression>>();
            ObservableCollection<FieldSelector> fieldsEditing = new ObservableCollection<FieldSelector>();

            Information = new DefendingEditor<Bridge<IExpression>, FieldSelector>(
                fields, new EditableCollection<FieldSelector>(
                    fieldsEditing,
                    new List<FieldSelector>(),
                    new RelayCommand(
                        argument =>
                        {
                            TextExpression current = new TextExpression() { Type = "Текст" };
                            FieldSelector field = new FieldSelector(
                                new List<IExpression>
                                {
                                    current,
                                    new NumberExpression() { Type = "Число" },
                                    new DateExpression() { Type = "Дата" }
                                }
                            );
                            field.Source = new Bridge<IExpression>(current);
                            fields.Add(field.Source);
                            fieldsEditing.Add(field);
                        }
                    ),
                    new RelayCommand(
                        argument =>
                        {
                            int count = Information.Editing.SelectedItems.Count;
                            while (count > 0)
                            {
                                fields.Remove(Information.Editing.SelectedItems[0].Source);
                                fieldsEditing.Remove(Information.Editing.SelectedItems[0]);
                                count--;
                            }
                        }
                    )
                )
            );
        }


        //private void Create_Click(object sender, RoutedEventArgs e)
        //{
        //    Save();

        //    Pair<string, bool> head = UserAgreement();
        //    if (head.Value)
        //    {
        //        //FileDocument.WriteDocuments(ViewModel, $"{head.Name}\\");
        //    }
        //}

        //private void OnClosing(object sender, CancelEventArgs e)
        //{
        //    if (Keyboard.FocusedElement is TextBox textBox)
        //    {
        //        TraversalRequest tRequest = new
        //            TraversalRequest(FocusNavigationDirection.Next);
        //        _ = textBox.MoveFocus(tRequest);
        //    }

        //    Save();
        //}

        //private void Save()
        //{
        //    //if (_originalFileName != FileName)
        //    //{
        //    //    RenameFile(_originalFileName, FileName);
        //    //    _originalFileName = FileName;
        //    //}

        //    //if (ViewModel.WasChanged())
        //    //    SavePreferences();
        //}

        //private void SavePreferences()
        //{
        //    TruncateFile(_originalFileName);
        //    SaveRuntime(FileName, ViewModel);
        //}
    }
}