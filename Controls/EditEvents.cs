using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using static WisdomLight.Controls.EditHelper;

namespace WisdomLight.Controls
{
    public partial class EditEvents
    {
        #region HoursEditing Members
        private static readonly Regex _hours = new Regex("^([1-9]|[1-9]\\d\\d?)$");

        private void Hours(object sender, TextCompositionEventArgs e)
        {
            CheckForText(e, _hours);
        }

        private void PastingHours(object sender, DataObjectPastingEventArgs e)
        {
            CheckForPastingText(sender, e, _hours);
        }
        #endregion

        #region Naming Members
        private static readonly Regex _naming = new Regex(@"^[A-Za-zА-Яа-я0-9\s-_]*$");

        private void Naming(object sender, TextCompositionEventArgs e)
        {
            CheckForText(e, _naming);
        }

        private void PastingNaming(object sender, DataObjectPastingEventArgs e)
        {
            CheckForPastingText(sender, e, _naming);
        }
        #endregion

        #region MemoryNo Members
        private string _memoryNo;
        private static readonly Regex _no = new Regex(@"\d+");

        private void ForgetNo(object sender, RoutedEventArgs e)
        {
            _memoryNo = MemoryNoGotFocus(sender);
        }

        private void MemoryNo(object sender, RoutedEventArgs e)
        {
            MemoryNoLostFocus(sender, _memoryNo, _no);
        }
        #endregion

        #region Extending Members
        private void ExtendItems(object sender, MouseButtonEventArgs e)
        {
            DetermineExtension(sender, e);
        }

        private void WrapFields(object sender, RoutedEventArgs e)
        {
            DetermineWrap(sender);
        }
        #endregion
    }
}