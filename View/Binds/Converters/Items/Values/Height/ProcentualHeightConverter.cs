using System;
using System.Globalization;
using System.Windows.Data;

namespace WisdomLight.View.Binds.Converters.Items.Values.Height
{
    public class ProcentualHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is not string argument)
                return value;

            if (value is not double size)
                return value;

            if (!double.TryParse(argument, out double coefficient))
                return value;

            size /= coefficient;
            return size > 0 ? size : 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is not string argument)
                return value;

            if (value is not double size)
                return value;

            if (!double.TryParse(argument, out double coefficient))
                return value;

            size *= coefficient;
            return size <= 0 ? 1 : size;
        }
    }
}