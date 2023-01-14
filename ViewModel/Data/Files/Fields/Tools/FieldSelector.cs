using System.Collections.Generic;
using WisdomLight.Model;
using WisdomLight.ViewModel.Data.Files.Fields.Tools.Editors;
using WisdomLight.ViewModel.Files.Fields;

namespace WisdomLight.ViewModel.Data.Files.Fields.Tools
{
    public class FieldSelector : NotifyPropertyChanged, ISource<IExpression>, ICloneable<FieldSelector>
    {
        public FieldSelector() { }

        public FieldSelector(List<IExpression> expressions)
        {
            Expressions = expressions;
        }

        private IExpression _source;
        public IExpression Source
        {
            get => _source;
            set
            {
                _source = value;
                OnPropertyChanged();
            }
        }

        public FieldSelector Clone()
        {
            return new FieldSelector(new List<IExpression>(Expressions))
            {
                Source = Source.Clone()
            };
        }

        public List<IExpression> Expressions { get; }
    }
}
