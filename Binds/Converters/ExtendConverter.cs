﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using static WisdomLight.Customing.Converters;

namespace WisdomLight.Binds.Converters
{
    public class ExtendConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool extended = value.ToBool();
            return extended ?
                Visibility.Visible :
                Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}