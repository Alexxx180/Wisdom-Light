using System.IO;
using WisdomLight.ViewModel.Customing;

namespace WisdomLight.Model.Exceptions.IO
{
    public class MoveException : SaveException, IDetails
    {
        public string NewPath { get; set; }

        public MoveException(IOException exception, string original,
            string next) : base(exception, original)
        {
            NewPath = next;
        }

        public override string Details()
        {
            return $"!Move Error!"
                .Form("From", OriginalPath)
                .Form("To", NewPath)
                .Form("Description", Message);
        }
    }
}
