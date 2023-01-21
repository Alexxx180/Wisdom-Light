using WisdomLight.ViewModel.Data;
using WisdomLight.ViewModel.Data.Collections;
using WisdomLight.ViewModel.Data.Files.Fields.Tools;
using WisdomLight.ViewModel.Data.Files.Processors.Serialization.Objects;

namespace WisdomLight.ViewModel.Components
{
    public class TemplateViewModel : NameLabel, IDefender
    {
        public FileFiller Serializer { get; set; }

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

        private bool _isRelative;
        public bool IsRelative
        {
            get => _isRelative;
            set
            {
                _isRelative = value;
                OnPropertyChanged();
            }
        }

        public string Location { get; protected internal set; }
    }
}
