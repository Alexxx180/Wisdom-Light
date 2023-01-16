using WisdomLight.ViewModel.Customing;

namespace WisdomLight.Model.Exceptions.IO
{
    public class MoveException : SaveException, IDetails
    {
        public string NewPath { get; set; }

        public MoveException(string message, string original,
            string next) : base(message, original)
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
