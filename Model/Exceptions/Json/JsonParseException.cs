using Newtonsoft.Json;
using WisdomLight.ViewModel.Components.Building.Extensions.Decorators;

namespace WisdomLight.Model.Exceptions.Json
{
    public class JsonParseException : JsonException, IDetails
    {
        public string OriginalPath { get; set; }

        public JsonParseException(JsonException exception, string original)
            : base(exception.Message)
        {
            OriginalPath = original;
        }

        public virtual string Details()
        {
            return $"!JSON Parse Error!"
                .Lists("From", OriginalPath)
                .Lists("Description", Message);
        }
    }
}
