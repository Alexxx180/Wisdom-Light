using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using WisdomLight.Controls;

namespace WisdomLight.Customing
{
    public static class Decorators
    {
        public static Color Rgb(byte red, byte green, byte blue)
        {
            return Color.FromRgb(red, green, blue);
        }

        public static void SetActive(this FrameworkElement element, bool isEnabled)
        {
            element.Visibility = isEnabled ?
                Visibility.Visible :
                Visibility.Hidden;
            element.IsEnabled = isEnabled;
        }

        public static void Refresh<T>
            (this IList<T> list, IEnumerable<T> value)
        {
            list.Clear();
            foreach (T item in value)
            {
                list.Add(item);
            }
        }

        public static void Refresh<T>
            (this IList<T> list, IEnumerable<IRawData<T>> value)
        {
            list.Clear();
            foreach (IRawData<T> item in value)
            {
                list.Add(item.Raw());
            }
        }

        public static List<T> GetRaw<T>
            (this IEnumerable<IRawData<T>> list)
        {
            List<T> elements = new List<T>();
            foreach (IRawData<T> item in list)
            {
                elements.Add(item.Raw());
            }
            return elements;
        }

        public static List<T> Zip<T>
            (this List<List<T>> original)
        {
            List<T> archive = new List<T>();
            for (ushort i = 0; i < original.Count; i++)
            {
                archive.AddRange(original[i]);
            }
            return archive;
        }

        public static T OmniTernar<T>
            (T fallBackValue, bool[] conditions, T[] values)
        {
            T result = fallBackValue;
            for (int i = 0; i < conditions.Length; i++)
            {
                if (conditions[i])
                {
                    result = values[i];
                    break;
                }
            }
            return result;
        }

        public static uint Sum(this ushort[] numbers)
        {
            uint sum = 0;
            for (ushort i = 0; i < numbers.Length; i++)
            {
                sum += numbers[i];
            }
            return sum;
        }
    }
}