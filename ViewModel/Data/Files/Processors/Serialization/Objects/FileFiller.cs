namespace WisdomLight.ViewModel.Data.Files.Processors.Serialization.Objects
{
    public class FileFiller
    {
        private FileProcessor Processor { get; set; }
        private FileProcessor[] _processors;

        internal void SetAvailable(params FileProcessor[] processors)
        {
            _processors = processors;
        }

        internal void Change(int selection)
        {
            Processor = _processors[selection];
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
