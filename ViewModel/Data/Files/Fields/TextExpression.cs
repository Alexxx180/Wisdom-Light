using WisdomLight.ViewModel.Data;
using WisdomLight.ViewModel.Files.Fields;

namespace WisdomLight.ViewModel.Data.Files.Fields
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
                Value = Value,
                Type = Type
            };
        }
    }
}
