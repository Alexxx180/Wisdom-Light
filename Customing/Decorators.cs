using System.Windows;
using System.Collections.Generic;
using WisdomLight.Controls;

namespace WisdomLight.Customing
{
    public static class Decorators
    {
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
    }
}