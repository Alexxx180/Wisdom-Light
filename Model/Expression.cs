namespace WisdomLight.Model
{
    /// <summary>
    /// Data expression element
    /// </summary>
    public struct Expression
    {
        public Expression(string name, string data)
        {
            Name = name;
            Data = data;
        }

        public string Name { get; set; }
        public string Data { get; set; }
    }
}