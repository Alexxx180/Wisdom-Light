using System.Collections.Generic;
using WisdomLight.ViewModel.Components.Data.Units;
using WisdomLight.ViewModel.Components.Data.Units.Fields;

namespace WisdomLight.ViewModel.Components.Core.Processors.Export.Documents
{
    public interface IDocument
    {
        public void Export(IList<DocumentLinker> paths, List<IExpression> expressions, string folder);
    }
}
