using System.IO;
using WisdomLight.ViewModel.Customing;

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
                .Form("In", Directory)
                .Form("From", OriginalPath)
                .Form("To", NewPath)
                .Form("Description", Message);
        }
    }
}
