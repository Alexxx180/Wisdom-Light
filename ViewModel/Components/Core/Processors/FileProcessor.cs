using System.IO;
using WisdomLight.Model.Exceptions.IO;
using WisdomLight.ViewModel.Components.Building.Extensions.Paths;
using WisdomLight.ViewModel.Components.Building.Extensions.Paths.Files;

namespace WisdomLight.ViewModel.Components.Core.Processors
{
    public abstract class FileProcessor : Saver
    {
        /// <summary>
        /// Delete file
        /// </summary>
        /// <param name="path">Original full file path</param>
        /// <exception cref="MoveException">Renaming failure</exception>
        internal static void Delete(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (IOException exception)
            {
                throw new SaveException(exception, path);
            }
        }

        /// <summary>
        /// Move files
        /// </summary>
        /// <param name="original">Original full file name</param>
        /// <param name="next">New full file name</param>
        /// <param name="extension"></param>
        /// <exception cref="MoveException">Renaming failure</exception>
        internal static void Move(string original, string next)
        {
            try
            {
                File.Move(original, next, true);
            }
            catch (IOException exception)
            {
                throw new MoveException(exception, original, next);
            }
        }

        /// <summary>
        /// Rename files on the same file path
        /// </summary>
        /// <param name="path">Path to files</param>
        /// <param name="original">Original file name</param>
        /// <param name="next">New file name</param>
        /// <exception cref="MoveException">Renaming failure</exception>
        internal static void Rename(string path, string original, string next)
        {
            string name = original.ToPath(path);
            if (File.Exists(name))
                Move(name, next.ToPath(path));
        }

        /// <summary>
        /// Rename files without losing extension
        /// </summary>
        /// /// <param name="path">Path to files</param>
        /// <param name="original">Original name</param>
        /// <param name="next">New name</param>
        /// <param name="extension">Files extension</param>
        /// <exception cref="MoveException">Renaming failure</exception>
        public static void Rename(string path, string original, string next, string extension)
        {
            Rename(path, original.ToFile(extension), next.ToFile(extension));
        }

        /// <summary>
        /// Fix file extension if broken extension provided
        /// </summary>
        /// <param name="path">Full file path</param>
        public abstract string FixExtension(string path);

        /// <summary>
        /// Deserialize object from file
        /// </summary>
        /// <param name="path">Original full file path</param>
        /// <exception cref="ReadException">Reading failure</exception>
        protected internal abstract T Read<T>(string path);

        /// <summary>
        /// Serialize object into file
        /// </summary>
        /// <param name="path">Original full file path</param>
        /// <exception cref="SaveException">Reading failure</exception>
        protected internal abstract void Write<T>(string path, T serializable);
    }
}
