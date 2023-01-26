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

        private IEditableBuilder<DocumentLinker> _documents;
        private IEditableBuilder<FieldSelector> _information;

        private bool _isDefended;
        private bool _isRelative;

        public TemplateBuilder()
        {
            _documents = new EditableBuilder<DocumentLinker>();
            _information = new EditableBuilder<FieldSelector>();
        }

        public TemplateViewModel Build()
        {
            _viewModel = new TemplateViewModel
            {
                Documents = _documents.Build(),
                Information = _information.Build(),
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
            _documents.Reset();
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

        public ITemplateBuilder Documents()
        {
            _documents.Fields().SelectedItems();
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
