namespace WisdomLight.Model
{
    public struct KeyConfirmer
    {
        public KeyConfirmer(byte key, string path, bool result)
        {
            Key = key;
            Path = path;
            Result = result;
        }

        public byte Key { get; set; }
        public bool Result { get; set; }
        public string Path { get; set; }
    }
}
