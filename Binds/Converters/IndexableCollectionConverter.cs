using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace WisdomLight.Binds.Converters
{
    /// <summary>
    /// Converter of Observable collection, supporting auto-indexing feature
    /// </summary>
    /// <typeparam name="TSource">Type of source</typeparam>
    /// <typeparam name="TIndexability">Type of target</typeparam>
    public class IndexableCollectionConverter<TSource, TIndexability> : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<TSource> original = value as ObservableCollection<TSource>;
            IEnumerable<TIndexability> casted = original.Cast<TIndexability>();
            return new ObservableCollection<TIndexability>(casted);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<TIndexability> original = value as ObservableCollection<TIndexability>;
            IEnumerable<TSource> casted = original.Cast<TSource>();
            return new ObservableCollection<TSource>(casted);
        }
    }
}