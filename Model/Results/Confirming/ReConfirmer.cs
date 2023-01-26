namespace WisdomLight.Model.Results.Confirming
{
    public struct ReConfirmer
    {
        public ReConfirmer(byte key, string name, string path, bool result)
        {
            Key = key;
            Name = name;
            Path = path;
            Result = result;
        }

        public byte Key { get; set; }
        public bool Result { get; set; }

        public string Name { get; set; }
        public string Path { get; set; }

        public string FullPath => $"{Path}\\{Name}";
    }
}
