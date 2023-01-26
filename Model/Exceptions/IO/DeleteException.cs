using System.IO;
using WisdomLight.ViewModel.Components.Building.Extensions.Decorators;

namespace WisdomLight.Model.Exceptions.IO
{
    public class DeleteException : SaveException
    {
        public DeleteException(IOException exception, string original) :
            base(exception, original) { }

        public override string Details()
        {
            return "!Delete Error!"
                .Lists("From", OriginalPath)
                .Lists("Description", Message);
        }
    }
}
