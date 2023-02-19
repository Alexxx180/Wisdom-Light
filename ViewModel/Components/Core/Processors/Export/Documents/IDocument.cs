using System.Collections.Generic;
using WisdomLight.ViewModel.Components.Data.Units.Collections;
using WisdomLight.ViewModel.Components.Data.Units.Fields.Tools;

namespace WisdomLight.ViewModel.Components.Core.Processors.Export.Documents
{
    public interface IDocument
    {
        public void Export(IDocuments documents, IList<FieldSelector> expressions, string folder);
    }
}
