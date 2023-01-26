using System.Windows;
using System.Windows.Data;

namespace WisdomLight.View.Binds.Converters.Items.Values.Visible
{
    public class InverseVisibilityConverter : BooleanVisibilityConverter, IValueConverter
    {
        private protected override bool IsVisible(object value)
        {
            return !base.IsVisible(value);
        }

        private protected override bool IsTrue(object value)
        {
            return (Visibility)value == Visibility.Collapsed;
        }
    }
}
