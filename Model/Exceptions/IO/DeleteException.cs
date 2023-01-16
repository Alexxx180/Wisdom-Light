using System.IO;
using WisdomLight.ViewModel.Customing;

namespace WisdomLight.Model.Exceptions.IO
{
    public class DeleteException : SaveException
    {
        public DeleteException(IOException exception, string original) :
            base(exception, original) { }

        public override string Details()
        {
            return "!Delete Error!"
                .Form("From", OriginalPath)
                .Form("Description", Message);
        }
    }
}
