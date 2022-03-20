using System;

namespace WisdomLight.Customing
{
    public static class Converters
    {
        public static bool ToBool(this object value)
        {
            return Convert.ToBoolean(value);
        }

        public static int ToInt(this object value)
        {
            return Convert.ToInt32(value);
        }
    }
}