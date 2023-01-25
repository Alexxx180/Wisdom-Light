using System.IO;
using WisdomLight.ViewModel.Components.Building.Extensions.Decorators;

namespace WisdomLight.Model.Exceptions.IO
{
    public class ReadException : SaveException
    {
        public ReadException(IOException exception, string original)
            : base(exception, original) { }

        public override string Details()
        {
            return $"!Read Error!"
                .Lists("From", OriginalPath)
                .Lists("Description", Message);
        }
    }
}
