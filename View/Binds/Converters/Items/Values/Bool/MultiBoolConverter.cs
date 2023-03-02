using System;
using System.Globalization;
using System.Windows.Data;

namespace WisdomLight.View.Binds.Converters.Items.Values.Bool
{
    public class MultiBoolConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool chain = true;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] is bool condition)
                    chain &= condition;
            }
            return chain;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
