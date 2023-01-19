using System.Collections.ObjectModel;
using WisdomLight.ViewModel.Commands;
using System.Collections.Generic;
using WisdomLight.ViewModel.Files.Fields;
using WisdomLight.ViewModel.Data.Collections;
using WisdomLight.ViewModel.Data.Files.Fields;
using WisdomLight.ViewModel.Data.Files.Fields.Tools;
using WisdomLight.ViewModel.Data;
using WisdomLight.ViewModel.Data.Files.Processors.Serialization.Objects;
using System.Windows.Input;
using System.Text.Json.Serialization;

namespace WisdomLight.ViewModel
{
    public class FileViewModel : NameLabel, IDefender
    {
        [JsonInclude]
        public FileFiller Serializer { get; private set; }

        private EditableCollection<DocumentLinker> _documents;
        public EditableCollection<DocumentLinker> Documents
        {
            get => _documents;
            set
            {
                _documents = value;
                OnPropertyChanged();
            }
        }

        private EditableCollection<FieldSelector> _information;
        public EditableCollection<FieldSelector> Information
        {
            get => _information;
            set
            {
                _information = value;
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

        public FileViewModel() { }

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

            ObservableCollection<DocumentLinker> documents = new ObservableCollection<DocumentLinker>();

            Documents = new EditableCollection<DocumentLinker>(
                documents,
                new List<DocumentLinker>(),
                new EditCommands(
                    new RelayCommand(
                        argument =>
                        {
                            DocumentLinker linker = new DocumentLinker
                            {
                                Name = "",
                                Type = ""
                            };
                            documents.Add(linker);
                        }
                    ),
                    new RelayCommand(argument => Documents.RemoveSelected())
                )
            );


            ObservableCollection<FieldSelector> fieldsEditing = new ObservableCollection<FieldSelector>();

            Information = new EditableCollection<FieldSelector>(
                fieldsEditing,
                new List<FieldSelector>(),
                new EditCommands(
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
                            field.Source = current;
                            fieldsEditing.Add(field);
                        }
                    ),
                    new RelayCommand(argument => Information.RemoveSelected())
                )
            );
        }

        public ICommand NewCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SaveAsCommand { get; }
        public ICommand CloseCommand { get; }
    }
}