namespace WisdomLight.ViewModel.Components.Data.Units.Fields
{
    public class NumberExpression : TypeLabel, IExpression
    {
        private uint _no;
        public uint No
        {
            get => _no;
            set
            {
                _no = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Value));
            }
        }

        public string Value => No.ToString();

        public IExpression Clone()
        {
            return new NumberExpression
            {
                Name = Name,
                No = No,
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
