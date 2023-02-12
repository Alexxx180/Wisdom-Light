using System.Collections.Generic;
using WisdomLight.ViewModel.Components.Building.Collections;
using WisdomLight.ViewModel.Components.Core.Processors.Serialization.Objects;
using WisdomLight.ViewModel.Components.Data;
using WisdomLight.ViewModel.Components.Data.Units;
using WisdomLight.ViewModel.Components.Data.Units.Fields.Tools;
using WisdomLight.ViewModel.Components.Core.Processors.Export.Units.Texts.Extracting;

namespace WisdomLight.ViewModel.Components.Building.Filler.Templates
{
    public class TemplateBuilder : ITemplateBuilder
    {
        private TemplateViewModel _viewModel;

        private List<ParagraphExtracting> _extracting;
        
        private FileFiller _serializer;

        private IEditableBuilder<Querier> _queriers;
        private IEditableBuilder<DocumentLinker> _links;
        private IEditableBuilder<FieldSelector> _information;

        private bool _isDefended;
        private bool _isRelative;

        public TemplateBuilder()
        {
            _links = new EditableBuilder<DocumentLinker>();
            _queriers = new EditableBuilder<Querier>();
            _information = new EditableBuilder<FieldSelector>();
        }

        public TemplateViewModel Build()
        {
            _viewModel = new TemplateViewModel
            {
                Links = _links.Build(),
                Information = _information.Build(),
                Queriers = _queriers.Build(),
                Extracting = _extracting,
                IsDefended = _isDefended,
                IsRelative = _isRelative,
                Serializer = _serializer
            };
            return _viewModel;
        }

        public ITemplateBuilder Reset()
        {
            _viewModel = null;
            _extracting = null;
            _isDefended = false;
            _isRelative = false;
            _links.Reset();
            _queriers.Reset();
            _information.Reset();
            return this;
        }

        public ITemplateBuilder Defend()
        {
            _isDefended = true;
            return this;
        }

        public ITemplateBuilder Relate()
        {
            _isRelative = true;
            return this;
        }

        public ITemplateBuilder Links()
        {
            _links.Fields().SelectedItems();
            return this;
        }

        public ITemplateBuilder Queriers()
        {
            _queriers.Fields().SelectedItems();
            return this;
        }

        public ITemplateBuilder Information()
        {
            _information.Fields().SelectedItems();
            return this;
        }

        public ITemplateBuilder Extracting()
        {
            _extracting = new List<ParagraphExtracting>
            {
                ParagraphExtracting.PLAIN,
                ParagraphExtracting.CELLS
            };
            return this;
        }

        public ITemplateBuilder Serializer()
        {
            _serializer = new FileFiller()
            {
                Current = 0
            };
            return this;
        }
    }
}
