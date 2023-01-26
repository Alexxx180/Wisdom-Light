using System;
using Newtonsoft.Json;
using WisdomLight.ViewModel.Components.Building.Extensions.Decorators;

namespace WisdomLight.Model.Exceptions.Json
{
    public class ParseException : JsonException, IDetails
    {
        public string OriginalPath { get; set; }

        public ParseException(Exception exception, string original)
            : base(exception.Message)
        {
            OriginalPath = original;
        }

        public virtual string Details()
        {
            return $"!Parse Error!"
                .Lists("From", OriginalPath)
                .Lists("Description", Message);
        }
    }
}
