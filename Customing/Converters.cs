using System;
using System.Collections.Generic;
using WisdomLight.Controls;
using WisdomLight.Model;

namespace WisdomLight.Customing
{
    public static class Converters
    {
        public static bool ToBool(this object value)
        {
            return Convert.ToBoolean(value);
        }

        public static byte ToByte(this object value)
        {
            return Convert.ToByte(value);
        }

        public static int ToInt(this object value)
        {
            return Convert.ToInt32(value);
        }

        public static ushort ToUShort(this object value)
        {
            return Convert.ToUInt16(value);
        }

        public static uint ToUInt(this object value)
        {
            return Convert.ToUInt32(value);
        }

        public static ulong ToULong(this object value)
        {
            return Convert.ToUInt64(value);
        }

        public static string ToBase64(this byte[] value)
        {
            return Convert.ToBase64String(value);
        }

        public static ushort ConvertHours(this object hours)
        {
            return ParseHours(hours.ToString());
        }

        public static ushort ParseHours(this string hours)
        {
            return ushort.TryParse(hours, out ushort result) ?
                result : 0.ToUShort();
        }

        /// <summary>
        /// Format number to fit general prefix number.
        /// </summary>
        /// <param name="value">String value.</param>
        /// <returns>General competetion prefix no.</returns>
        public static string ToGeneralNo(this uint value)
        {
            return string.Format("{0:00}", value);
        }

        public static string ToGeneralNo(this IFormattable value)
        {
            return string.Format("{0:00}", value);
        }

        public static
            Dictionary<TName, TValue> ToDictionary<TName, TValue>
            (this IEnumerable<IRawData<Pair<TName, TValue>>> list)
        {
            Dictionary<TName, TValue>
                elements = new
                Dictionary<TName, TValue>();
            foreach (IRawData<Pair<TName, TValue>> item in list)
            {
                Pair<TName, TValue> pair = item.Raw();
                elements.Add(pair.Name, pair.Value);
            }
            return elements;
        }
    }
}