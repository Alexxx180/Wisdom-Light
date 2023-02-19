using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WisdomLight.View.Binds.Converters.Items.Values.Decoration
{
    public class FontWeightConverter : IValueConverter
    {
        private protected virtual bool IsDecorated(object value)
        {
            return (bool)value;
        }

        private protected virtual bool IsTrue(object value)
        {
            return (FontWeight)value == FontWeights.Bold;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return IsDecorated(value) ?
                FontWeights.Bold :
                FontWeights.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return IsTrue(value);
        }
    }
}
