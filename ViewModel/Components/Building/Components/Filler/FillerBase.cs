using WisdomLight.ViewModel.Components.Building.Filler.Templates;
using WisdomLight.ViewModel.Components.Data;

namespace WisdomLight.ViewModel.Components.Building.Components.Filler.Blocks
{
    public abstract class FillerBase : FillerBuilder2
    {
        private protected ITemplateBuilder _template;
        private protected TemplateViewModel _data;
        
        public FillerBase()
        {
            _template = new TemplateBuilder();
        }

        public override FillerBuilder2 Template()
        {
            _data = _template.Links().Queriers().Information().Serializer().Relate().Defend().Extracting().Build();
            return this;
        }

        public override FillerBuilder2 Reset()
        {
            _data = null;
            _viewModel = null;
            _template.Reset();
            return this;
        }

        public override FileViewModel Build()
        {
            return new FileViewModel
            {
                Data = _data
            };
        }
    }
}
