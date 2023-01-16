using System;
using static System.IO.Path;
using Serilog;

namespace WisdomLight.ViewModel.Customing
{
    public static class Decorators
    {
        public static string Form(this string text, string header, string message)
        {
            return $"{text}\n- {header}: {message}";
        }

        public static string Close(this string path)
        {
            return $"{path}{DirectorySeparatorChar}";
        }

        public static string ToPath(this string name, string path)
        {
            return $"{path}{name}";
        }

        public static string ToFile(this string path, string extension)
        {
            return $"{path}.{extension}";
        }

        public static bool IsFile(this string name, string format)
        {
            bool isFile = false;

            try
            {
                string extension = GetExtension(name);
                string lowered = extension.ToLower();
                isFile = lowered == $".{format}";
            }
            catch (ArgumentException exception)
            {
                string text = "Can't get extension: ";
                Log.Error(text + exception.Message);
            }

            return isFile;
        }

        //public static void SetActive(this FrameworkElement element, bool isEnabled)
        //{
        //    element.Visibility = isEnabled ?
        //        Visibility.Visible :
        //        Visibility.Hidden;
        //    element.IsEnabled = isEnabled;
        //}

        //public static void Refresh<T>
        //    (this IList<T> list, IEnumerable<T> value)
        //{
        //    list.Clear();
        //    foreach (T item in value)
        //    {
        //        list.Add(item);
        //    }
        //}

        //public static void Refresh<T>
        //    (this IList<T> list, IEnumerable<IRawData<T>> value)
        //{
        //    list.Clear();
        //    foreach (IRawData<T> item in value)
        //    {
        //        list.Add(item.Raw());
        //    }
        //}
    }
}