using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace WisdomLight.Controls
{
    public static class EditHelper
    {
        private static string GetProposedText(TextBox textBox, string newText)
        {
            string text = textBox.Text;
            if (textBox.SelectionStart != -1)
                text = text.Remove(textBox.SelectionStart, textBox.SelectionLength);
            text = text.Insert(textBox.CaretIndex, newText);
            return text;
        }

        public static void CheckForText(TextCompositionEventArgs e, Regex regex)
        {
            TextBox box = e.OriginalSource as TextBox;
            string full = box.Text.Insert(box.CaretIndex, e.Text);
            e.Handled = !regex.IsMatch(full);
        }

        public static void CheckForPastingText(object sender, DataObjectPastingEventArgs e, Regex regex)
        {
            if (!e.DataObject.GetDataPresent(typeof(string)))
            {
                e.CancelCommand();
                return;
            }

            TextBox textBox = sender as TextBox;
            string pastedText = e.DataObject.GetData(typeof(string)) as string;
            string proposedText = GetProposedText(textBox, pastedText);

            if (!regex.IsMatch(proposedText))
            {
                e.CancelCommand();
            }
        }

        public static string MemoryNoGotFocus(object sender)
        {
            TextBox box = sender as TextBox;
            string memoryNo = box.Text;
            box.SetCurrentValue(TextBox.TextProperty, "");
            return memoryNo;
        }

        public static void MemoryNoLostFocus(object sender, string memoryNo, Regex regex)
        {
            TextBox box = sender as TextBox;
            if (box.Text.Length <= 0)
            {
                memoryNo = regex.Match(memoryNo).Value;
                box.SetCurrentValue(TextBox.TextProperty, memoryNo);
            }
        }

        public static void DetermineExtension(object sender, MouseButtonEventArgs e)
        {
            //IExtendableItems extendable = sender as IExtendableItems;
            //extendable.ExtendItems();
            //e.Handled = true;
        }

        public static void DetermineWrap(object sender)
        {
            IWrapFields wrapFields = sender as IWrapFields;
            wrapFields.WrapFields();
        }
    }
}