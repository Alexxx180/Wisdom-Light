using System.Collections.Generic;

namespace WisdomLight.ViewModel.Components.Data.Units.Collections
{
    public interface IDocuments
    {
        public IEnumerable<string> GetNextDocument();
    }
}
