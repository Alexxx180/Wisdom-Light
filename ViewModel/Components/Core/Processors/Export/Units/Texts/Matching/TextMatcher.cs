using System.Linq;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;
using WisdomLight.Model;

namespace WisdomLight.ViewModel.Components.Core.Processors.Export.Replace.Texts.Matching
{
    public class TextMatcher : IMatcher
    {
        /// <summary>
        /// Check text if match 'text to find' condition:
        /// - figure out the starting point of XML
        /// Paragraph Text element search string.
        /// </summary>
        /// <param name="texts">Texts to iterate.</param>
        /// <param name="element">
        /// Start point of current text element.
        /// </param>
        /// <param name="character">
        /// Start point of current char in element.
        /// </param>
        /// <param name="find">Text to find.</param>
        /// <returns><see cref="Match"/></returns>
        public Match IsMatch(IEnumerable<Text> texts, int element, int character, string find)
        {
            int findIndex = 0;
            for (int i = element; i < texts.Count(); i++)
            {
                string text = texts.ElementAt(i).Text;
                for (int ii = character; ii < text.Length; ii++)
                {
                    // Element mismatch
                    if (find[findIndex] != text[ii])
                        return null;

                    // Check next character
                    if (++findIndex != find.Length)
                        continue;

                    return new Match()
                    {
                        Element = i,
                        Character = ii
                    };
                }

                // Reset char index for next element
                character = 0;
            }

            // Ran out of text, no string match
            return null;
        }
    }
}
