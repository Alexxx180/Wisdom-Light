﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace WisdomLight.View.Binds.Converters.Items.Values.Bool
{
    public class InverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}
