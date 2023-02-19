using System.Collections.Generic;

namespace WisdomLight.ViewModel.Components.Data.Units.Collections
{
    public class RouterCollection : EditableCollection<Querier>, IDocuments
    {
        protected internal DependenciesViewModel ViewModel { get; set; }

        public IEnumerable<string> GetNextDocument()
        {
            for (int i = 0; i < Fields.Count; i++)
            {
                yield return ViewModel.QueryPath(Fields[i]);
            }
        }
    }
}
