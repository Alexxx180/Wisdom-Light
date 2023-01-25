using System.IO;
using WisdomLight.ViewModel.Components.Building.Extensions.Decorators;

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
                .Lists("From", OriginalPath)
                .Lists("To", NewPath)
                .Lists("Description", Message);
        }
    }
}
