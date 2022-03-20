using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using static WisdomLight.Controls.EditHelper;

namespace WisdomLight.Controls
{
    public partial class EditEvents
    {
        #region Naming Members
        private static readonly Regex _naming = new Regex(@"^[\w\-. ]+$");

        private void Naming(object sender, TextCompositionEventArgs e)
        {
            CheckForText(e, _naming);
        }

        private void PastingNaming(object sender, DataObjectPastingEventArgs e)
        {
            CheckForPastingText(sender, e, _naming);
        }
        #endregion

        #region Extending Members
        private void WrapFields(object sender, RoutedEventArgs e)
        {
            DetermineWrap(sender);
        }
        #endregion
    }
}