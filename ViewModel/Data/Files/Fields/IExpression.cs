using Newtonsoft.Json;
using WisdomLight.Model;
using WisdomLight.ViewModel.Data.Files.Fields;
using WisdomLight.ViewModel.Data.Files.Processors.Serialization.Json;

namespace WisdomLight.ViewModel.Files.Fields
{
    public interface IExpression : ICloneable<IExpression>
    {
        public string Name { get; set; }
        public string Value { get; }
        public string Type { get; set; }
    }
}
