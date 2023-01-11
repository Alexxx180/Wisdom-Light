namespace WisdomLight.ViewModel.Fields
{
    public class NumberExpression : NameLabel, IExpression
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
                No = No
            };
        }
    }
}
