using System.Collections.Generic;

namespace WisdomLight.ViewModel.Components.Data.Units.Collections
{
    public class LinkerList : EditableCollection<DocumentLinker>, IDocuments
    {
        public IEnumerable<string> GetNextDocument()
        {
            for (int i = 0; i < Fields.Count; i++)
            {
                yield return Fields[i].Type;
            }
        }
    }
}
