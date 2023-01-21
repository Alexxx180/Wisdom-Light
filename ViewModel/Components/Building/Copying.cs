using System.Collections.Generic;
using System.Linq;
using WisdomLight.Model;

namespace WisdomLight.ViewModel.Components.Building
{
    public static class Copying
    {
        public static List<T> Clone<T>(this List<T> listToClone) where T : ICloneable<T>
        {
            return listToClone.Select(item => item.Clone()).ToList();
        }
    }
}
