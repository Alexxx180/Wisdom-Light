namespace WisdomLight.Model
{
    public struct Confirmer
    {
        public Confirmer(string path, bool result)
        {
            Path = path;
            Result = result;
        }

        public string Path { get; set; }
        public bool Result { get; set; }
    }
}
