using WisdomLight.ViewModel.Components.Data;

namespace WisdomLight.ViewModel.Components.Building.Filler.Templates
{
    public interface ITemplateBuilder
    {
        public ITemplateBuilder Defend();

        public ITemplateBuilder Relate();

        public ITemplateBuilder Documents();

        public ITemplateBuilder Information();

        public ITemplateBuilder Serializer();

        public ITemplateBuilder Extracting();

        public ITemplateBuilder Reset();

        public TemplateViewModel Build();
    }
}
