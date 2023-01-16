using System;
using System.IO;
using System.Text.Json;
using WisdomLight.Model;
using Serilog;
using System.Windows.Forms;
using static System.Environment;
using WisdomLight.ViewModel.Customing;
using WisdomLight.Model.Exceptions.IO;

namespace WisdomLight.ViewModel.Data.Files.Writers.Processors
{
    public class FileProcessor
    {
        internal static void TruncateFile(string fileName)
        {
            Log.Information("Truncating file: " + fileName);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        private protected static void Save(string path, byte[] bytes)
        {
            try
            {
                File.WriteAllBytes(path, bytes);
            }
            catch (IOException exception)
            {
                throw new SaveException(exception.Message, path);
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
                throw new MoveException(exception.Message, original, next);
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
            if (!File.Exists(name))
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

        internal static FolderBrowserDialog CallLocator(string description)
        {
            return new FolderBrowserDialog
            {
                Description = description,
                UseDescriptionForTitle = true,
                SelectedPath = GetFolderPath(SpecialFolder.DesktopDirectory).Close(),
                ShowNewFolderButton = true
            };
        }
    }
}
