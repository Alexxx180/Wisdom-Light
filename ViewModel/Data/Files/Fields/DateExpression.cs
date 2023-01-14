using System;
using WisdomLight.ViewModel.Files.Fields;

namespace WisdomLight.ViewModel.Data.Files.Fields
{
    public class DateExpression : TypeLabel, IExpression
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
                Date = Date,
                Type = Type
            };
        }
    }
}
