using System;

namespace WisdomLight.ViewModel.Fields
{
    public class DateExpression : NameLabel, IExpression
    {
        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Value));
            }
        }

        public string Value => Date.ToString();

        public IExpression Clone()
        {
            return new DateExpression
            {
                Date = Date
            };
        }
    }
}
