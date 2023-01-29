using System.IO;

namespace WisdomLight.ViewModel.Components.Core.Processors.Export.Paths
{
    public class PathBuilder
    {
        private string _sub;
        private string _buffer;

        private string _disk;
        private string _path;

        private string _userDisk;
        private string _userPath;

        private int _trim;

        public PathBuilder SetUserPath(string path)
        {
            _trim = path.IndexOf(Path.DirectorySeparatorChar);
            _userDisk = (_trim <= 0) ? "" : path.Substring(0, _trim);
            _userPath = path[_trim..];
            return this;
        }

        public PathBuilder SetPath(string path)
        {
            _trim = path.IndexOf(Path.DirectorySeparatorChar);
            _disk = (_trim <= 0) ? "" : path.Substring(0, _trim);
            _path = path[_trim..];
            return this;
        }

        public PathBuilder Next()
        {
            _trim = _userPath.IndexOf(Path.DirectorySeparatorChar);
            _sub = _userPath.Substring(0, _trim);
            return this;
        }

        public PathBuilder Trim()
        {
            _userPath = _userPath[_trim..];
            _buffer = _buffer[_trim..];
            return this;
        }

        public PathBuilder Reset()
        {
            _buffer = _userPath;
            return this;
        }

        public bool FullEntry()
        {
            _trim = _userPath.IndexOf(_path);
            return _trim != -1;
        }

        public bool NameEntry()
        {
            return _buffer.IndexOf(_sub) == 0;
        }

        //string path = "Aleksandr/Test/file.docx";
        //string actual = "Aleksandr/Program";
        public string Build()
        {
            return Path.Combine(_disk, _path, _userPath, _userDisk);
        }
    }
}
