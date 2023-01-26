using System.IO;
using WisdomLight.Model.Exceptions.IO;

namespace WisdomLight.ViewModel.Components.Core.Processors
{
    public class DirectoryProcessor
    {
        internal static string[] LoadTemplateNames(string folder)
        {
            string[] templates;
            try
            {
                templates = Directory.GetFiles(folder);
            }
            catch (IOException exception)
            {
                throw new DirectoryListingException(exception, folder);
            }
            return templates;
        }
    }
}
