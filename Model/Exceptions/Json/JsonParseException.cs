using System.Text.Json;
using WisdomLight.ViewModel.Customing;

namespace WisdomLight.Model.Exceptions.Json
{
    public class JsonParseException : JsonException, IDetails
    {
        public string OriginalPath { get; set; }

        public JsonParseException(JsonException exception, string original)
            : base(exception.Message, exception.Path, exception.LineNumber,
                  exception.BytePositionInLine, exception.InnerException)
        {
            OriginalPath = original;
        }

        public virtual string Details()
        {
            return $"!JSON Parse Error!"
                .Form("From", OriginalPath)
                .Form("Description", Message);
        }
    }
}
