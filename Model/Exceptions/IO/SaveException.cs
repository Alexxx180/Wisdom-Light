using System.IO;
using WisdomLight.ViewModel.Customing;

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

        public virtual string Details()
        {
            return "!Save Error!"
                .Form("To", OriginalPath)
                .Form("Description", Message);
        }
    }
}
