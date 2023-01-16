using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WisdomLight.ViewModel.Customing;

namespace WisdomLight.ViewModel.Data.Files.Writers.Processors.Json.Objects
{
    public class FileFiller : JsonProcessor
    {
        internal static void SaveRuntime(string name, FileViewModel program)
        {
            string fullName = name.ToPath(App.Runtime).ToFile("json");
            Log.Debug($"Saving runtime: {fullName}");
            ProcessJsonAny(fullName, program);
        }

        internal static Document LoadDocument(string name)
        {
            Log.Debug($"Loading document: {Runtime}{name}");
            return ReadJson<Document>(name);
        }

        internal static Pair<string, T> LoadRuntime<T>(string name)
        {
            Log.Debug($"Loading runtime: {Runtime}{name}");
            return !File.Exists(Runtime + name) ? null :
                ReadJson<Pair<string, T>>(Runtime + name);
        }
    }
}
