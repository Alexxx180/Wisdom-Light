using WisdomLight.ViewModel.Components.Core.Processors;
using WisdomLight.ViewModel.Components.Core.Processors.Serialization;

namespace WisdomLight.ViewModel.Components.Building.Bank
{
    public static class Serialization
    {
        public static FileProcessor[] Serializers()
        {
            return new FileProcessor[]
            {
                new JsonProcessor()
            };
        }
    }
}
