namespace WisdomLight.ViewModel.Fields
{
    public interface IExpression : ICloneable<IExpression>
    {
        public string Name { get; }
        public string Value { get; }
        public string Type { get; }
    }
}
