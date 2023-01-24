using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using WisdomLight.ViewModel.Components.Core.Processors.Export.Units.Texts.Matching.Replace;

namespace WisdomLight.ViewModel.Components.Core.Processors.Export.Texts.Matching.Replace
{
    /// <summary>
    /// Wrap replacer
    /// </summary>
    public class WrapReplacer : TextPreparer, ITextReplacer
    {
        /// <summary>
        /// Make a wrap replacement:
        /// Finish single-line replacement
        /// to end of text to replace with
        /// </summary>
        /// <param name="text">Current replacement text element.</param>
        /// <param name="current">Current element index.</param>
        /// /// <param name="lines">Array of replacement text lines.</param>
        /// <returns>Text index of last inserted text chunk.</returns>
        public int Replace(Text text, int current, params string[] lines)
        {
            OpenXmlElement currentElement = text;
            Break textBreak;

            var run = text.Parent as Run;
            for (int i = 1; i < lines.Length; i++)
            {
                textBreak = new Break();
                currentElement = run.InsertAfter(textBreak, currentElement);
                //currentElement = textBreak;
                text = new Text(lines[i]);
                currentElement = run.InsertAfter(text, currentElement);

                current++;
                //currentElement = text;
            }
            return current;
        }
    }
}
