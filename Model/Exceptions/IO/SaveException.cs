using System;
using System.IO;
using WisdomLight.ViewModel.Components.Building.Extensions.Decorators;

namespace WisdomLight.Model.Exceptions.IO
{
    public class SaveException : IOException, IDetails
    {
        public string OriginalPath { get; set; }

        public SaveException(IOException exception, string original)
            : base(exception.Message, exception.HResult)
        {
            OriginalPath = original;
        }

        public SaveException(Exception exception, string original)
            : base(exception.Message, exception.HResult)
        {
            OriginalPath = original;
        }

        public virtual string Details()
        {
            return "!Save Error!"
                .Lists("To", OriginalPath)
                .Lists("Description", Message);
        }
    }
}
