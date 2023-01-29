using System.IO;
using System.Text;
using WisdomLight.Model;
using WisdomLight.ViewModel.Components.Building.Extensions.Paths.Files;
using WisdomLight.ViewModel.Components.Core.Processors.Serialization;
using WisdomLight.ViewModel.Components.Core.Processors.Serialization.Objects;

namespace WisdomLight.ViewModel.Components.Core.Processors.Export.Paths
{
    public class RelativePathDirector : IPathDirector
    {
        private PathBuilder _builder;
        public DependenciesFiller _filler { get; set; }

        public RelativePathDirector(string path)
        {
            _builder = new PathBuilder();
            _builder.SetPath(path);
        }

        private string GenerateFile(string path, DependencyCounter counter)
        {
            string directory = Path.GetFileNameWithoutExtension(path);
            Directory.CreateDirectory(directory);

            int count = Directory.GetFiles(directory).Length;
            string fullPath = Path.Combine(directory, count.ToString()).ToFile("json");

            _filler.Save(fullPath, counter);
            return fullPath;
        }

        private string NewDependencyPath(string path)
        {
            if (!File.Exists(path))
                return "";

            



            DependencyCounter counter = new DependencyCounter
            {
                Dependency = path,
                Count = 0
            };

            GenerateFile(path, counter);
            _filler.Save(path, counter);

            return "";
        }

        private string GetExistingDependency(string path)
        {
            return _filler.Load(path).Dependency;
        }

        private string AddExistingDependency(string path)
        {
            DependencyCounter counter = _filler.Load(path);
            counter.Count++;
            _filler.Save(path, counter);

            return counter.Dependency;
        }

        public string DirectPath(string path)
        {
            if (!File.Exists(path))
                return "";

            _builder.SetUserPath(path).Reset();
            string dependencies = AbsolutePathProcess();

            return File.Exists(dependencies) ?
                AddExistingDependency(dependencies) :
                NewDependencyPath(path);
        }

        public string RedirectPath(string path)
        {
            string dependencies = Path.GetFileNameWithoutExtension(path).ToFile(JsonProcessor.Extension);

            return File.Exists(dependencies) ?
                GetExistingDependency(dependencies) : string.Empty;
        }

        // string path = "/Aleksandr/Test/(D)/file.docx" (D:)
        // string path = "/Aleksandr/Test/(E)/file.docx" (E:)
        // string actual = "/Aleksandr/Actual/Wisdom-Light.exe" (D:)
        // int trim = path.IndexOf(actual);
        // while (trim != -1 || trim != 0) {
        //     actual = actual.GetDirectoryName()
        //     int trim = path.IndexOf(actual);
        // }
        // string path.Substring(trim)
        // string full = ;

        private string AbsolutePathProcess()
        {
            if (_builder.FullEntry())
                return _builder.Build();

            while (_builder.Next().NameEntry())
                _builder.Trim();

            return _builder.Build();
        }
    }
}
