using System.Collections.Generic;
using WisdomLight.ViewModel.Components.Core.Processors.Export.Paths;
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

        private EditableCollection<DocumentLinker> _links;
        public EditableCollection<DocumentLinker> Links
        {
            get => _links;
            set
            {
                _links = value;
                OnPropertyChanged();
            }
        }

        private EditableCollection<Querier> _queriers;
        public EditableCollection<Querier> Queriers
        {
            get => _queriers;
            set
            {
                _queriers = value;
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
                //Path = Paths[value ? 1 : 0];
                OnPropertyChanged();
            }
        }

        public IPathDirector Path { get; private set; }
        public IPathDirector[] Paths { get; protected internal set; }

        public string Location { get; protected internal set; }
        public string FileName { get; protected internal set; }
    }
}
