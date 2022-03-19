using System;
using System.Globalization;
using System.Windows.Data;

namespace WisdomLight.Binds.Converters
{
    public class ProcentualHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string p)
            {
                if (value is double v)
                {
                    bool result = double.TryParse(p, out double param);
                    if (result)
                    {
                        double size = v / param;
                        return size > 0 ? size : 1;
                    }
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}