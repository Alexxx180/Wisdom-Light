using System.Collections.ObjectModel;
using WisdomLight.ViewModel.Fields;
using WisdomLight.ViewModel.Commands;
using System.Windows.Input;
using System.Collections.Generic;

namespace WisdomLight.ViewModel
{
    public class FileViewModel : NotifyPropertyChanged
    {
        #region Documents
        private ObservableCollection<string> _documents;
        public ObservableCollection<string> Documents
        {
            get => _documents;
            set
            {
                _documents = value;
                OnPropertyChanged();
            }
        }

        public bool CanAddDocuments => (Documents != null ? Documents.Count : 0) < byte.MaxValue;
        #endregion

        #region Information Members
        private ObservableCollection<Field> _information;
        public ObservableCollection<Field> Information
        {
            get => _information;
            set
            {
                _information = value;
                OnPropertyChanged();
            }
        }

        private FieldSelector _informationType;
        public FieldSelector InformationType
        {
            get => _informationType;
            set
            {
                _informationType = value;
                OnPropertyChanged();
            }
        }

        public bool CanAddInformation => (Information != null ? Information.Count : 0) < byte.MaxValue;
        #endregion

        #region Blanks Members
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
            Information = new ObservableCollection<Field>();
            Documents = new ObservableCollection<string>();

            InformationType = new FieldSelector(expressions)
            {
                Command = new RelayCommand(argument =>
                {
                    Information.Add(new Field
                    {
                        Expression = (IExpression)argument,
                        Command = new RelayCommand(argument =>
                        {
                            _ = Information.Remove((Field)argument);
                            OnPropertyChanged(nameof(CanAddInformation));
                        })
                    });
                    OnPropertyChanged(nameof(CanAddInformation));
                })
            };

            AddDocument = new RelayCommand(argument => {
                Documents.Add(argument.ToString());
                OnPropertyChanged(nameof(CanAddDocuments));
            });

            DropDocument = new RelayCommand(argument => {
                Documents.Remove(argument.ToString());
                OnPropertyChanged(nameof(CanAddDocuments));
            });
        }

        public ICommand AddDocument { get; }
        public ICommand DropDocument { get; }
    }
}