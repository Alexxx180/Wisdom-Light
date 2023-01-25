using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using WisdomLight.Model;
using WisdomLight.ViewModel.Components.Core.Processors.Export.Texts;
using WisdomLight.ViewModel.Components.Core.Processors.Export.Units.Texts.Matching.Change.Replace;
using WisdomLight.ViewModel.Components.Core.Processors.Export.Units.Texts.Matching.Replace;

namespace WisdomLight.ViewModel.Components.Core.Processors.Export.Units.Texts.Matching.Change
{
    public class TextChanger : TextPreparer
    {
        public SpaceProcessingModeValues Spaces { get; set; }

        private int _text;
        private int _char;

        public TextChanger()
        {
            Spaces = SpaceProcessingModeValues.Preserve;
            Matcher = new TextMatcher();
            Replacer = new WrapReplacer();
        }

        /// <summary>
        /// Replace the existing search string text on the point.
        /// If breaks - New line : Else - Same line
        /// </summary>
        /// <param name="builder">Builder containing the result text</param>
        /// <param name="current">Current text element to replace in</param>
        private void ReplaceText(ChunkBuilder builder, Text current)
        {
            if (builder.Lines.Length > 1)
            {
                _char = builder.Skip;
                _text = Replacer.Replace(current, _text, builder.Lines);
            }
            else
            {
                _char += builder.Skip;
            }
        }

        /// <summary>
        /// Prepare text to being replaced
        /// </summary>
        /// <param name="chunks">Text chunks collection.</param>
        /// <param name="current">Selected element to process.</param>
        /// <param name="match">Search matching results.</param>
        /// <param name="replace">Text expression to replace with.</param>
        private void ChangeChunk(IEnumerable<Text> chunks, Text current, Match match, string replace)
        {
            ChunkBuilder builder = new ChunkBuilder()
                .Split(replace).Prefix(current, _char).Postfix(chunks, match);

            Whitespace(current, Spaces, builder.Lines[0]);
            Clear(chunks, _text + 1, match.Element);
            ReplaceText(builder, current);
        }

        /// <summary>
        /// Change the paragraph text
        /// </summary>
        /// <param name="paragraph">Paragraph to search in.</param>
        /// <param name="find">Text expression to find.</param>
        /// <param name="replace">Text expression to replace with.</param>
        public void Change(Paragraph paragraph, string find, string replace)
        {
            IEnumerable<Text> chunks = paragraph.Descendants<Text>();
            for (_text = 0; _text < chunks.Count(); _text++)
            {
                Text current = chunks.ElementAt(_text);
                for (_char = 0; _char < current.Text.Length; _char++)
                {
                    Match match = Matcher.IsMatch(chunks, _text, _char, find);
                    if (match == null)
                        continue;

                    match.Character++;
                    ChangeChunk(chunks, current, match, replace);
                }
            }
        }

        public IMatcher Matcher { get; set; }
        public ITextReplacer Replacer { get; set; }
    }
}
