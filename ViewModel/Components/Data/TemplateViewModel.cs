using Newtonsoft.Json;
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
        [JsonIgnore]
        protected internal IDocuments Documents { get; set; }

        public List<ParagraphExtracting> Extracting { get; set; }
        public FileFiller Serializer { get; set; }

        private LinkerList _links;
        public LinkerList Links
        {
            get => _links;
            set
            {
                _links = value;
                OnPropertyChanged();
                if (!IsRelative)
                    Documents = Links;
            }
        }

        private RouterCollection _queriers;
        public RouterCollection Queriers
        {
            get => _queriers;
            set
            {
                _queriers = value;
                OnPropertyChanged();
                if (IsRelative)
                    Documents = Queriers;
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
                Documents = value ? Queriers : Links;
                OnPropertyChanged();
            }
        }

        public string Location { get; protected internal set; }
        public string FileName { get; protected internal set; }
    }
}
