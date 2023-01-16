namespace WisdomLight.ViewModel.Data.Files.Processors.Serialization.Objects
{
    public class FileFiller
    {
        private FileProcessor _processor;

        internal FileFiller(FileProcessor processor)
        {
            _processor = processor;
        }

        internal void Save(string path, FileViewModel program)
        {
            _processor.Write(path, program);
        }

        internal FileViewModel Load(string path)
        {
            return _processor.Read<FileViewModel>(path);
        }
    }
}
