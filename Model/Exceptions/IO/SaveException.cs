using System.IO;
using WisdomLight.ViewModel.Customing;

namespace WisdomLight.Model.Exceptions.IO
{
    public class SaveException : IOException, IDetails
    {
        public string OriginalPath { get; set; }

        public SaveException(string message, string original) : base(message)
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
