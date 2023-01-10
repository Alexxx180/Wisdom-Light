using System.Windows.Input;

namespace WisdomLight.ViewModel.Fields
{
    public class Field
    {
        public IExpression Expression { get; set; }
        public ICommand Command { get; set; }
    }
}
