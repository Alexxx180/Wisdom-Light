namespace WisdomLight.ViewModel.Components.Data.Units.Fields
{
    public class TextExpression : TypeLabel, IExpression
    {
        private string _value;
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        public IExpression Clone()
        {
            return new TextExpression
            {
                Name = Name,
                Value = Value,
                Type = Type
            };
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is not IExpression textObj)
                return false;
            else
                return Equals(textObj);
        }

        public bool Equals(IExpression other)
        {
            if (other == null)
                return false;

            return Name == other.Name &&
                Value == other.Value &&
                Type == other.Type;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
