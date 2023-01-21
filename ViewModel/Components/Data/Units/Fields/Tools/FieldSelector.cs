using System.Collections.Generic;
using System.Collections.ObjectModel;
using WisdomLight.Model;
using WisdomLight.ViewModel.Files.Fields;

namespace WisdomLight.ViewModel.Data.Files.Fields.Tools
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
                //Source = Expressions[Selected];
                //OnPropertyChanged(nameof(Source));
            }
        }

        public ObservableCollection<IExpression> Expressions { get; set; }

        public FieldSelector() { }

        public FieldSelector(ObservableCollection<IExpression> expressions)
        {
            Expressions = expressions;
        }

        //public IExpression Source { get; private set; }
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
            return new FieldSelector
                (new ObservableCollection<IExpression>(Expressions))
            {
                Current = Current.Clone()
            };
        }
    }
}
