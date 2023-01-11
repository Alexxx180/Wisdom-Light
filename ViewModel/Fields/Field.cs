namespace WisdomLight.ViewModel.Fields
{
    public class Field : NotifyPropertyChanged
    {
        private IExpression _expression;
        public IExpression Expression
        {
            get => _expression;
            set
            {
                _expression = value;
                OnPropertyChanged();
            }
        }
    }
}
