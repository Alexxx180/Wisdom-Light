using System.IO;
using WisdomLight.ViewModel.Components.Building.Extensions.Decorators;

namespace WisdomLight.Model.Exceptions.IO
{
    public class RenameException : MoveException, IDetails
    {
        public string Directory { get; set; }

        public RenameException(IOException exception, string path,
            string original, string next) : base(exception, original, next)
        {
            Directory = path;
            NewPath = next;
        }

        public override string Details()
        {
            return "!Rename Error!"
                .Lists("In", Directory)
                .Lists("From", OriginalPath)
                .Lists("To", NewPath)
                .Lists("Description", Message);
        }
    }
}
