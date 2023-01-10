using System.Collections.Generic;

namespace WisdomLight.ViewModel.Fields
{
    public class FieldSelector : Field
    {
        public List<IExpression> Expressions { get; }

        public FieldSelector(List<IExpression> expressions)
        {
            Expressions = expressions;
        }
    }
}
