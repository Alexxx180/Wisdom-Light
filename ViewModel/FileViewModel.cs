using System.Collections.ObjectModel;
using WisdomLight.ViewModel.Commands;
using System.Collections.Generic;
using WisdomLight.ViewModel.Files.Fields;
using WisdomLight.ViewModel.Data.Collections;
using WisdomLight.ViewModel.Data.Files.Fields;
using WisdomLight.ViewModel.Data.Files.Fields.Tools;
using WisdomLight.ViewModel.Data.Files.Fields.Tools.Editors;
using WisdomLight.Model;
using WisdomLight.ViewModel.Data;
using WisdomLight.ViewModel.Data.Files.Processors.Serialization.Objects;
using System.Windows.Input;

namespace WisdomLight.ViewModel
{
    public class FileViewModel : NameLabel
    {
        protected internal FileFiller Serializer { get; }

        #region Documents
        private DefendingEditor<DocumentLinker, DocumentLinker> _documents;
        public DefendingEditor<DocumentLinker, DocumentLinker> Documents
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

        /// <summary>
        /// Reseting processors to include new
        /// features in future accordingly
        /// </summary>
        public FileViewModel()
        {
            Serializer.SetAvailable(App.Processors);
        }

        public FileViewModel(FileFiller serializer,
            ICommand next, ICommand open,
            ICommand saveas, ICommand close,
            bool isDefended)
        {
            Serializer = serializer;
            NewCommand = next;
            OpenCommand = open;
            SaveAsCommand = saveas;
            CloseCommand = close;
            IsDefended = isDefended;

            ObservableCollection<DocumentLinker> paths = new ObservableCollection<DocumentLinker>();
            ObservableCollection<DocumentLinker> pathEditing = new ObservableCollection<DocumentLinker>();

            Documents = new DefendingEditor<DocumentLinker, DocumentLinker>(
                paths, new EditableCollection<DocumentLinker>(
                    pathEditing,
                    new List<DocumentLinker>(),
                    new RelayCommand(
                        argument =>
                        {
                            DocumentLinker linker = new DocumentLinker
                            {
                                Name = "",
                                Type = ""
                            };
                            
                            paths.Add(linker);
                            pathEditing.Add(linker);
                        }
                    ),
                    new RelayCommand(
                        argument =>
                        {
                            int count = Documents.Editing.SelectedItems.Count;
                            while (count > 0)
                            {
                                _ = paths.Remove(Documents.Editing.SelectedItems[0]);
                                _ = pathEditing.Remove(Documents.Editing.SelectedItems[0]);
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
                                _ = fields.Remove(Information.Editing.SelectedItems[0].Source);
                                _ = fieldsEditing.Remove(Information.Editing.SelectedItems[0]);
                                count--;
                            }
                        }
                    )
                )
            );
        }

        public ICommand NewCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SaveAsCommand { get; }
        public ICommand CloseCommand { get; }

        //    if (Keyboard.FocusedElement is TextBox textBox)
        //    {
        //        TraversalRequest tRequest = new
        //            TraversalRequest(FocusNavigationDirection.Next);
        //        _ = textBox.MoveFocus(tRequest);
        //    }
    }
}