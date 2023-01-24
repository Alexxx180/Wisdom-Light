using System.Collections.Generic;
using WisdomLight.Model;
using WisdomLight.ViewModel.Components.Building;

namespace WisdomLight.ViewModel.Components.Data.Units.Fields.Tools
{
    public class FieldSelector : NotifyPropertyChanged, ICloneable<FieldSelector>
    {
        private int _selected;
        public int Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        public List<IExpression> Expressions { get; set; }

        public FieldSelector() { }

        public FieldSelector(List<IExpression> expressions)
        {
            Expressions = expressions;
        }

        private IExpression _current;
        public IExpression Current
        {
            get => _current;
            set
            {
                _current = value;
                OnPropertyChanged();
            }
        }

        public FieldSelector Clone()
        {
            List<IExpression> expressions = Expressions.Clone();
            return new FieldSelector
            {
                Expressions = expressions,
                Selected = Selected,
                Current = expressions[Selected]
            };
        }
    }
}
