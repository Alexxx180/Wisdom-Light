using WisdomLight.ViewModel.Components.Building.Components.Units;
using WisdomLight.ViewModel.Components.Data;

namespace WisdomLight.ViewModel.Components.Building.Filler.Templates
{
    public interface ITemplateBuilder
    {
        public ITemplateBuilder SetDependencies(DependenciesViewModel dependencies);

        public ITemplateBuilder Defend();

        public ITemplateBuilder Relate();

        public ITemplateBuilder Links();

        public ITemplateBuilder Queriers();

        public ITemplateBuilder Information();

        public ITemplateBuilder Serializer();

        public ITemplateBuilder Extracting();

        public ITemplateBuilder Reset();

        public TemplateViewModel Build();
    }
}
