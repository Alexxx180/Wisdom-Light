using WisdomLight.ViewModel.Components;

namespace WisdomLight.ViewModel.Data.Files.Processors.Serialization.Objects
{
    public class FileFiller
    {
        public FileFiller()
        {
            _processors = App.Processors;
        }

        internal void FixedSave(string path, TemplateViewModel program)
        {
            path = _processor.FixExtension(path);
            _processor.Write(path, program);
        }

        internal void Save(string path, TemplateViewModel program)
        {
            _processor.Write(path, program);
        }

        internal TemplateViewModel Load(string path)
        {
            return _processor.Read<TemplateViewModel>(path);
        }

        private byte _current;
        public byte Current
        {
            get => _current;
            set
            {
                _current = value;
                _processor = _processors[value];
            }
        }

        private FileProcessor[] _processors;
        private FileProcessor _processor;
    }
}
