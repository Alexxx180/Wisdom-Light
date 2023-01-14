using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using WisdomLight.ViewModel.Customing;
using static WisdomLight.ViewModel.Customing.Converters;

namespace WisdomLight.View.Binds.Converters
{
    public class BooleanVisibilityConverter : IValueConverter
    {
        private protected virtual bool IsVisible(object value)
        {
            return value.ToBool();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return IsVisible(value) ?
                Visibility.Visible :
                Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
