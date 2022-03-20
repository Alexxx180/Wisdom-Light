using System;
using System.IO;
using System.Text.Json;
using WisdomLight.Model;
using Serilog;
using System.Windows.Forms;

namespace WisdomLight.Writers.AutoGenerating
{
    public static class Processors
    {
        private static string RuntimeDirectory => Environment.CurrentDirectory + @"\Runtime\";

        static Processors()
        {
            Log.Debug($"Runtime directory set as: {RuntimeDirectory}");
        }

        #region Messages Members
        private static void SaveMessage(string exception)
        {
            string noLoad = "Не удалось сохранить файл.";
            string message = "\nУбедитесь, что посторонние процессы не мешают операции.\n";
            string advice = "\nПолное сообщение:\n";
            string fullMessage = noLoad + message + advice + exception;
            _ = System.Windows.MessageBox.Show(fullMessage);
        }

        internal static void LoadMessage(string exception)
        {
            string noLoad = "Сбой загрузки.";
            string message = "\nУбедитесь, что файлы не повреждены или отсутствуют.\n";
            string advice = "\nПолное сообщение:\n";
            string fullMessage = noLoad + message + advice + exception;
            _ = System.Windows.MessageBox.Show(fullMessage);
        }

        internal static void WriteMessage(string exception)
        {
            string message = "Файл открыт в другой " +
                    "программе или используется другим " +
                    "процессом. Дальнейшая запись в файл" +
                    " невозможна.\nПолное сообщение:\n";
            _ = System.Windows.MessageBox.Show(message + exception);
        }
        #endregion

        internal static FolderBrowserDialog
            CallLocator(string description)
        {
            return new FolderBrowserDialog
            {
                Description = description,
                UseDescriptionForTitle = true,
                SelectedPath = Environment.GetFolderPath(
                    Environment.SpecialFolder.DesktopDirectory)
                    + Path.DirectorySeparatorChar,
                ShowNewFolderButton = true
            };
        }

        #region FileProcessing Members
        internal static void TruncateFile(string fileName)
        {
            Log.Information("Truncating file: " + fileName);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        internal static void Save(string path, byte[] bytes)
        {
            Log.Information("Saving data... to: " + path);
            try
            {
                File.WriteAllBytes(path, bytes);
            }
            catch (IOException exception)
            {
                Log.Error("Save problem: " +
                    exception.Message);
                SaveMessage(exception.Message);
            }
        }

        internal static bool IsJson(string fileName)
        {
            bool isJson = false;

            try
            {
                string extension = Path.GetExtension(fileName);
                string lowered = extension.ToLower();

                isJson = lowered == ".json";
            }
            catch (ArgumentException exception)
            {
                string description = "File corrupted. Can't get extension: ";

                Log.Error(description + exception.Message);
            }

            return isJson;
        }
        #endregion

        #region JsonProcessing Members
        private static T ReadJson<T>(string path)
        {
            Log.Information("Reading user data from: " + path);
            T deserilizeable = default;
            try
            {
                byte[] fileBytes = File.ReadAllBytes(path);
                Utf8JsonReader utf8Reader = new Utf8JsonReader(fileBytes);
                deserilizeable = JsonSerializer.Deserialize<T>(ref utf8Reader);
            }
            catch (FileNotFoundException exception)
            {
                string description = "File not found: ";

                Log.Error(description + exception.Message);
                LoadMessage(exception.Message);
            }
            catch (ArgumentException exception)
            {
                string description = "Argument is invalid: ";

                Log.Error(description + exception.Message);
                LoadMessage(exception.Message);
            }
            catch (JsonException exception)
            {
                string description = "Reading user Json problem: ";

                Log.Error(description + exception.Message);
                LoadMessage(exception.Message);
            }
            catch (IOException exception)
            {
                string description = "I|O blocked by another process: ";

                Log.Error(description + exception.Message);
                LoadMessage(exception.Message);
            }
            return deserilizeable;
        }

        private static void ProcessJsonAny<T>(string path, T serilizeable)
        {
            try
            {
                TruncateFile(path);
                byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(serilizeable);
                File.WriteAllBytes(path, jsonUtf8Bytes);
            }
            catch (IOException exception)
            {
                Log.Error("I|O blocked can't process: " +
                    exception.Message);
                LoadMessage(exception.Message);
            }
        }
        #endregion

        #region SaveLoad Members
        internal static void RenameFile(string original, string newName)
        {
            string fullOriginalName = original.ToRuntime();
            string fullNewFileName = newName.ToRuntime();
            if (File.Exists(fullOriginalName))
            {
                try
                {
                    File.Move(fullOriginalName, fullNewFileName, true);
                }
                catch (IOException exception)
                {
                    SaveMessage(exception.Message);
                }
            }
        }

        internal static string[] LoadTemplateNames()
        {
            string[] templates;
            try
            {
                templates = Directory.GetFiles(RuntimeDirectory);
            }
            catch (IOException exception)
            {
                templates = Array.Empty<string>();
                string description = "Loading exception: ";

                Log.Error(description + exception.Message);
                LoadMessage(exception.Message);
            }
            return templates;
        }

        internal static Document LoadDocument(string name)
        {
            Log.Debug($"Loading document: {RuntimeDirectory}{name}");
            return ReadJson<Document>(name);
        }

        internal static
            Pair<string, T> LoadRuntime<T>(string name)
        {
            Log.Debug($"Loading runtime: {RuntimeDirectory}{name}");
            return !File.Exists(RuntimeDirectory + name) ? null :
                ReadJson<Pair<string, T>>(RuntimeDirectory + name);
        }

        internal static void SaveRuntime
            (string name, Document program)
        {
            string fullName = name.ToRuntime();
            Log.Debug($"Saving runtime: {fullName}");
            ProcessJsonAny(fullName, program);
        }

        internal static string ToRuntime(this string name)
        {
            return $"{RuntimeDirectory}{name}.json";
        }
        #endregion
    }
}