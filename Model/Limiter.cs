namespace WisdomLight.Model
{
    public struct Limiter
    {
        public Limiter(ushort min, ushort initial, ushort max)
        {
            Min = min;
            Initial = initial;
            Max = max;
        }

        public Limiter(ushort min, ushort initial) : this(min, initial, initial) { }

        public Limiter(ushort initial) : this(initial, initial, initial) { }

        public ushort Min { get; }
        public ushort Initial { get; }
        public ushort Max { get; }
    }
}
