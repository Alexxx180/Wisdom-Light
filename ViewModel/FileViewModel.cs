using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WisdomLight.Model;
using WisdomLight.Controls.Expressions;
using WisdomLight.Customing;
using WisdomLight.Controls.Templates;

namespace WisdomLight.ViewModel
{
    public class FileViewModel : INotifyPropertyChanged
    {
        #region BlankFields Logic
        private Document _blanks;
        public Document Blanks
        {
            get => _blanks;
            set
            {
                _blanks = value;
                OnPropertyChanged();
            }
        }

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

        private void Refresh(string name)
        {
            OnPropertyChanged(name);
            IsChanged = true;
        }

        #region Information Members
        private ObservableCollection<MetaElement> _information;
        public ObservableCollection<MetaElement> Information
        {
            get => _information;
            set
            {
                _information = value;
                OnPropertyChanged();
            }
        }

        public void AddMetaData(Expression data)
        {
            MetaElement element = new MetaElement
            {
                ViewModel = this
            };
            element.SetElement(data);
            Information.Add(element);
            Refresh(nameof(Information));
            OnPropertyChanged(nameof(CanAddInformation));
        }

        public void DropMetaData(MetaElement meta)
        {
            _ = Information.Remove(meta);
            Refresh(nameof(Information));
            OnPropertyChanged(nameof(CanAddInformation));
        }

        public bool CanAddInformation => (Information != null ?
            Information.Count : 0) < byte.MaxValue;
        #endregion

        #region Blanks Members
        private ObservableCollection<DocumentBlank> _templates;
        public ObservableCollection<DocumentBlank> Templates
        {
            get => _templates;
            set
            {
                _templates = value;
                OnPropertyChanged();
            }
        }

        public void AddBlank(string path)
        {
            DocumentBlank element = new DocumentBlank
            {
                ViewModel = this
            };
            element.SetElement(path);
            Templates.Add(element);
            Refresh(nameof(Information));
            OnPropertyChanged(nameof(CanAddTemplates));
        }

        public void DropBlank(DocumentBlank blank)
        {
            _ = Templates.Remove(blank);
            Refresh(nameof(Information));
            OnPropertyChanged(nameof(CanAddTemplates));
        }

        public bool CanAddTemplates => (Templates != null ?
            Templates.Count : 0) < byte.MaxValue;
        #endregion

        public FileViewModel()
        {
            Information = new ObservableCollection<MetaElement>();
            Templates = new ObservableCollection<DocumentBlank>();
            Blanks = new Document();
        }

        #region Filling Logic
        internal void SetUpDocumentBlank(in Document blanks)
        {
            blanks.Information.Refresh(Information);
            blanks.FileLocations.Refresh(Templates);
        }

        public void SetFromTemplate(Document document)
        {
            Blanks.Refresh(document);

            Information.Clear();
            for (byte i = 0; i < document.Information.Count; i++)
            {
                MetaElement meta = new MetaElement
                {
                    ViewModel = this
                };
                meta.SetElement(document.Information[i]);
                Information.Add(meta);
            }

            Templates.Clear();
            for (byte i = 0; i < document.FileLocations.Count; i++)
            {
                DocumentBlank blank = new DocumentBlank
                {
                    ViewModel = this
                };
                blank.SetElement(document.FileLocations[i]);
                Templates.Add(blank);
            }
        }

        public Document MakeDocument()
        {
            SetUpDocumentBlank(Blanks);
            return Blanks;
        }
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        #endregion
    }
}