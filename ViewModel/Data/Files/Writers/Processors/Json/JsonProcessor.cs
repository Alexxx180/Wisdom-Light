using System;
using System.IO;
using System.Text.Json;
using WisdomLight.Model;
using Serilog;
using System.Windows.Forms;
using WisdomLight.ViewModel.Customing;

namespace WisdomLight.ViewModel.Data.Files.Writers.Processors.Json
{
    public class JsonProcessor
    {
        

        

        #region Messages Members
        private static void SaveMessage(string exception)
        {
            string noLoad = "Не удалось сохранить файл.";
            string message = "\nУбедитесь, что посторонние процессы не мешают операции.\n";
            string advice = "\nПолное сообщение:\n";
            string fullMessage = message + advice + exception;
            _ = System.Windows.MessageBox.Show(fullMessage, noLoad);
        }

        internal static void LoadMessage(string exception)
        {
            string noLoad = "Сбой загрузки.";
            string message = "\nУбедитесь, что файлы не повреждены или отсутствуют.\n";
            string advice = "\nПолное сообщение:\n";
            string fullMessage = message + advice + exception;
            _ = System.Windows.MessageBox.Show(fullMessage, noLoad);
        }

        internal static void WriteMessage(string exception)
        {
            string noload = "Запись в файл невозможна";
            string message = "Файл открыт в другой " +
                    "программе или используется другим " +
                    "процессом.\nПолное сообщение:\n";
            _ = System.Windows.MessageBox.Show(message + exception, noload);
        }
        #endregion

        

        #region FileProcessing Members
        

        #region JsonProcessing Members
        private protected static T ReadJson<T>(string path)
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

        private protected static void ProcessJsonAny<T>(string path, T serilizeable)
        {
            try
            {
                TruncateFile(path);
                byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(serilizeable);
                File.WriteAllBytes(path, jsonUtf8Bytes);
            }
            catch (IOException exception)
            {
                string description = "I|O blocked can't process: ";
                Log.Error(description + exception.Message);
                LoadMessage(exception.Message);
            }
        }
        #endregion

        #region SaveLoad Members
        

        internal static string[] LoadTemplateNames()
        {
            string[] templates;
            try
            {
                templates = Directory.GetFiles(App.Runtime);
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

        

        
        #endregion
    }
}