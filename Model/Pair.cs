namespace WisdomLight.Model
{
    /// <summary>
    /// An universal class to create any necessary objects while debugging
    /// </summary>
    /// <typeparam name="TName">Type of name</typeparam>
    /// <typeparam name="TValue">Type of value</typeparam>
    public class Pair<TName, TValue>
    {
        public Pair()
        {
            Name = default;
            Value = default;
        }

        public Pair(TName name, TValue value) {
            Name = name;
            Value = value;
        }

        public TName Name { get; set; }
        public TValue Value { get; set; }
    }
}