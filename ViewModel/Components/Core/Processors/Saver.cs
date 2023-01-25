using System.IO;
using WisdomLight.Model.Exceptions.IO;

namespace WisdomLight.ViewModel.Components.Core.Processors
{
    public class Saver
    {
        /// <summary>
        /// Save raw file bytes
        /// </summary>
        /// <param name="path">Original full file path</param>
        /// <exception cref="SaveException">Saving failure</exception>
        private protected static void Save(string path, byte[] bytes)
        {
            try
            {
                File.WriteAllBytes(path, bytes);
            }
            catch (IOException exception)
            {
                throw new SaveException(exception, path);
            }
        }
    }
}
