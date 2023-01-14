using WisdomLight.Model;

namespace WisdomLight.ViewModel.Files.Fields
{
    public interface IExpression : ICloneable<IExpression>
    {
        public string Name { get; set; }
        public string Value { get; }
        public string Type { get; }
    }
}
