using System.Collections.ObjectModel;
using WisdomLight.ViewModel.Data;
using WisdomLight.ViewModel.Data.Files.Fields;
using WisdomLight.ViewModel.Data.Files.Fields.Tools;
using WisdomLight.ViewModel.Data.Files.Fields.Tools.Building.Filler.Collections;
using WisdomLight.ViewModel.Data.Files.Processors.Serialization.Objects;
using WisdomLight.ViewModel.Files.Fields;

namespace WisdomLight.ViewModel.Components.Building.Templates
{
    public class TemplateBuilder : ITemplateBuilder
    {
        private TemplateViewModel _viewModel;

        private FileFiller _serializer;

        private IEditableBuilder<DocumentLinker> _documents;
        private IEditableBuilder<FieldSelector> _information;

        private bool _isDefended;
        private bool _isRelative;

        public TemplateBuilder()
        {
            _documents = new EditableBuilder<DocumentLinker>()
                .Additor(new DocumentLinker { Name = "", Type = "" });
            _information = new EditableBuilder<FieldSelector>()
                .Additor(InformationAdditor());
        }

        private static FieldSelector InformationAdditor()
        {
            TextExpression current = new TextExpression() { Type = "Текст" };
            return new FieldSelector
            {
                Expressions = new ObservableCollection<IExpression>
                {
                    current,
                    new NumberExpression() { Type = "Число" },
                    new DateExpression() { Type = "Дата" }
                },
                Current = current
            };
        }

        public TemplateViewModel Build()
        {
            _viewModel = new TemplateViewModel
            {
                Documents = _documents.Build(),
                Information = _information.Build(),
                IsDefended = _isDefended,
                IsRelative = _isRelative,
                Serializer = _serializer
            };
            return _viewModel;
        }

        public ITemplateBuilder Reset()
        {
            _viewModel = null;
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
            _documents.Fields().SelectedItems().Add().Drop();
            return this;
        }

        public ITemplateBuilder Information()
        {
            _information.Fields().SelectedItems().Add().Drop();
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
