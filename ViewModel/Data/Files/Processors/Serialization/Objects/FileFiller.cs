using System.Text.Json.Serialization;

namespace WisdomLight.ViewModel.Data.Files.Processors.Serialization.Objects
{
    public class FileFiller
    {
        private FileProcessor Processor { get; set; }
        private FileProcessor[] _processors;
        
        [JsonInclude]
        public byte Current { get; private set; }

        public FileFiller()
        {
            _processors = App.Processors;
        }

        public FileFiller(byte current) : this()
        {
            Current = current;
        }

        internal void Change(byte selection)
        {
            Processor = _processors[selection];
            Current = selection;
        }

        internal void Save(string path, FileViewModel program)
        {
            Processor.Write(path, program);
        }

        internal FileViewModel Load(string path)
        {
            return Processor.Read<FileViewModel>(path);
        }
    }
}
