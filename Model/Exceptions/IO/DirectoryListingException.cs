using System.IO;
using WisdomLight.ViewModel.Customing;

namespace WisdomLight.Model.Exceptions.IO
{
    public class DirectoryListingException : SaveException
    {
        public DirectoryListingException(IOException exception, string original)
            : base(exception, original)
        {
            OriginalPath = original;
        }

        public override string Details()
        {
            return "!Directory Search Error!"
                .Form("In", OriginalPath)
                .Form("Description", Message);
        }
    }
}
