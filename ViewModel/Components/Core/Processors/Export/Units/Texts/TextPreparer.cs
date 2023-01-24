using System.Linq;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;

namespace WisdomLight.ViewModel.Components.Core.Processors.Export.Texts
{
    public class TextPreparer
    {
        /// <summary>
        /// Handle case when value starts/ends with whitespace
        /// </summary>
        /// <param name="text">Text to process.</param>
        /// <param name="space">Space handling.</param>
        /// <param name="start">Text to set.</param>
        private protected void Whitespace(Text text, SpaceProcessingModeValues space, string start)
        {
            text.Space = new EnumValue<SpaceProcessingModeValues>(space);
            text.Text = start;
        }

        /// <summary>
        /// Clear text at neccessary points in enumerable.
        /// </summary>
        /// <param name="texts">Texts to iterate.</param>
        /// <param name="start">Point to start cleaning.</param>
        /// <param name="end">Point to end cleaning.</param>
        private protected void Clear(IEnumerable<Text> texts, int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                texts.ElementAt(i).Text = string.Empty;
            }
        }
    }
}
