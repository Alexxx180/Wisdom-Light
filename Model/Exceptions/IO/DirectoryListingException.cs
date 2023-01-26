using System.IO;
using WisdomLight.ViewModel.Components.Building.Extensions.Decorators;

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
                .Lists("In", OriginalPath)
                .Lists("Description", Message);
        }
    }
}
