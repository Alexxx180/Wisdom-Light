using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using static WisdomLight.Customing.Converters;

namespace WisdomLight.Binds.Converters
{
    public class BooleanVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isVisible = value.ToBool();
            return isVisible ?
                Visibility.Visible :
                Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
