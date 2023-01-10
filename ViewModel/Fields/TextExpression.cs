namespace WisdomLight.ViewModel.Fields
{
    public class TextExpression : NameLabel, IExpression
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
    }
}
