using System.IO;
using System.Text.Json;
using WisdomLight.ViewModel.Customing;
using WisdomLight.Model.Exceptions.IO;
using WisdomLight.Model.Exceptions.Json;

namespace WisdomLight.ViewModel.Data.Files.Processors.Serialization
{
    internal class JsonProcessor : FileProcessor
    {
        /// <summary>
        /// Rename JSON files on the same file path
        /// </summary>
        /// <param name="path">Path to files</param>
        /// <param name="original">Original file name</param>
        /// <param name="next">New file name</param>
        /// <exception cref="MoveException">Renaming failure</exception>
        internal static new void Rename(string path, string original, string next)
        {
            FileProcessor.Rename(path, original.ToFile(".json"), next.ToFile(".json"));
        }

        /// <summary>
        /// Deserialize object from JSON file
        /// </summary>
        /// <param name="path">Full path to file</param>
        /// <exception cref="JsonParseException">JSON Parsing failure</exception>
        /// <exception cref="ReadException">Reading failure</exception>
        protected internal override T Read<T>(string path)
        {
            T deserilizeable;

            try
            {
                byte[] fileBytes = File.ReadAllBytes(path);
                Utf8JsonReader utf8Reader = new Utf8JsonReader(fileBytes);
                deserilizeable = JsonSerializer.Deserialize<T>(ref utf8Reader);
            }
            catch (JsonException exception)
            {
                throw new JsonParseException(exception, path);
            }
            catch (IOException exception)
            {
                throw new ReadException(exception, path);
            }

            return deserilizeable;
        }

        /// <summary>
        /// Serialize object into JSON file
        /// </summary>
        /// <param name="path">Full path to file</param>
        /// <param name="serilizeable">Serializeable object</param>
        /// <exception cref="SaveException">Saving failure</exception>
        protected internal override void Write<T>(string path, T serilizeable)
        {
            byte[] jsonBytesUtf8 = JsonSerializer.SerializeToUtf8Bytes(serilizeable);
            Save(path, jsonBytesUtf8);
        }
    }
}