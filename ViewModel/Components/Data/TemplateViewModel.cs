using System.Collections.Generic;
using WisdomLight.ViewModel.Components.Core.Processors.Export.Units.Texts.Extracting;
using WisdomLight.ViewModel.Components.Core.Processors.Serialization.Objects;
using WisdomLight.ViewModel.Components.Data.Units;
using WisdomLight.ViewModel.Components.Data.Units.Collections;
using WisdomLight.ViewModel.Components.Data.Units.Fields.Tools;

namespace WisdomLight.ViewModel.Components.Data
{
    public class TemplateViewModel : NameLabel, IDefender
    {
        public List<ParagraphExtracting> Extracting { get; set; }
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
        public string FileName { get; protected internal set; }
    }
}
