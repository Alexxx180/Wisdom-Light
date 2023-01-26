using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WisdomLight.View.Binds.Converters.Items.Values.Visible
{
    public class BooleanVisibilityConverter : IValueConverter
    {
        private protected virtual bool IsVisible(object value)
        {
            return (bool)value;
        }

        private protected virtual bool IsTrue(object value)
        {
            return (Visibility)value == Visibility.Visible;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return IsVisible(value) ?
                Visibility.Visible :
                Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return IsTrue(value);
        }
    }
}
