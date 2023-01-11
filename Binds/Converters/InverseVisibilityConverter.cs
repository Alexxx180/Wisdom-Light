using System.Windows.Data;

namespace WisdomLight.Binds.Converters
{
    public class InverseVisibilityConverter : BooleanVisibilityConverter, IValueConverter
    {
        private protected override bool IsVisible(object value)
        {
            return !base.IsVisible(value);
        }
    }
}
