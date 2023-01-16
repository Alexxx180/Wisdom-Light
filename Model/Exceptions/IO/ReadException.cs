using System.IO;
using WisdomLight.ViewModel.Customing;

namespace WisdomLight.Model.Exceptions.IO
{
    public class ReadException : SaveException
    {
        public ReadException(IOException exception, string original)
            : base(exception, original) { }

        public override string Details()
        {
            return $"!Read Error!"
                .Form("From", OriginalPath)
                .Form("Description", Message);
        }
    }
}
