using WisdomLight.ViewModel.Data;
using WisdomLight.ViewModel.Files.Fields;

namespace WisdomLight.ViewModel.Data.Files.Fields
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
                No = No,
                Type = Type
            };
        }
    }
}
