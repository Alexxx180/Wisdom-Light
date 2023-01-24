using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WisdomLight.Model;

namespace WisdomLight.ViewModel.Components.Core.Processors.Export.Units.Texts.Matching.Replace
{
    public class ChunkBuilder
    {
        public string[] Lines { get; private set; }
        public int Skip => Lines[^1].Length - 1;

        public SpaceProcessingModeValues SpaceProcessing { get; set; }

        /// <summary>
        /// Handle any lone n/r returns, plus newline. 
        /// </summary>
        /// <param name="text">String to split</param>
        /// <returns><see cref="ChunkBuilder"/></returns>
        public ChunkBuilder Split(string text)
        {
            Lines = text.Replace(Environment.NewLine, "\r").Split('\n', '\r');
            return this;
        }

        /// <summary>
        /// Check if text expression has a prefix
        /// </summary>
        /// <param name="current">Current XML text element</param>
        /// <returns><see cref="ChunkBuilder"/></returns>
        public ChunkBuilder Prefix(Text current, int character)
        {
            if (character > 0)
            {
                Lines[0] = current.Text.Substring(0, character) + Lines[0];
            }
            return this;
        }

        /// <summary>
        /// Check if text expression has a postfix
        /// </summary>
        /// <param name="last">Last match occured</param>
        /// <returns><see cref="ChunkBuilder"/></returns>
        public ChunkBuilder Postfix(IEnumerable<Text> chunks, Match last)
        {
            string text = chunks.ElementAt(last.Element).Text;
            if (last.Character < text.Length)
            {
                Lines[^1] += text[last.Character..];
            }
            return this;
        }
    }
}
