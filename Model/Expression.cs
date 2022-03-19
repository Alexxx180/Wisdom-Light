namespace WisdomLight.Model
{
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