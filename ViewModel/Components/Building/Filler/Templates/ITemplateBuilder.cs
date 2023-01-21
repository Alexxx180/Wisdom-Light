namespace WisdomLight.ViewModel.Components.Building.Templates
{
    public interface ITemplateBuilder
    {
        public ITemplateBuilder Defend();

        public ITemplateBuilder Relate();

        public ITemplateBuilder Documents();

        public ITemplateBuilder Information();

        public ITemplateBuilder Serializer();

        public ITemplateBuilder Reset();

        public TemplateViewModel Build();
    }
}
