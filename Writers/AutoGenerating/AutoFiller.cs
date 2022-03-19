using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

namespace WisdomLight.Writers.AutoGenerating
{
    public static class AutoFiller
    {
        public static void Save(string path, MemoryStream stream)
        {
            Processors.Save(path, stream.ToArray());
        }

        public static void ReplaceInParagraphs(
            IEnumerable<Paragraph> paragraphs, 
            string find, string replaceWith
            )
        {
            foreach (var textParagraph in paragraphs)
                ReplaceText(textParagraph, find, replaceWith);
        }

        public static void ReplaceInCells(IEnumerable<TableCell> cells, string find, string replaceWith)
        {
            foreach (TableCell cell in cells)
            {
                foreach (var cellData in cell)
                {
                    Paragraph textParagraph = cellData as Paragraph;
                    if (textParagraph != null)
                        ReplaceText(textParagraph, find, replaceWith);
                }
            }
        }

        /// <summary>
        /// Handle any lone n/r returns, plus newline. 
        /// </summary>
        /// <param name="text">Text to split</param>
        /// <returns>Text lines array</returns>
        private static string[] SplitLines(this string text)
        {
            return text.Replace(Environment.NewLine, "\r").Split('\n', '\r');
        }

        /// <summary>
        /// Clear text at neccessary points in enumerable.
        /// </summary>
        /// <param name="texts">Texts to iterate.</param>
        /// <param name="startPoint">Point to start cleaning.</param>
        /// <param name="endPoint">Point to end cleaning.</param>
        private static void ClearText(
            this IEnumerable<Text> texts,
            int startPoint, int endPoint
            )
        {
            for (int i = startPoint; i <= endPoint; i++)
            {
                texts.ElementAt(i).Text = string.Empty;
            }
        }

        /// <summary>
        /// Make a wrap replacement:
        /// Finish single-line replacement
        /// to end of text to replace with
        /// </summary>
        /// <param name="text">Current replacement text element.</param>
        /// <param name="lines">Array of replacement text lines.</param>
        /// <param name="currentTextIndex">Current element index.</param>
        /// <returns>Text index of last inserted text chunk.</returns>
        private static int WrapReplace(this Text text,
            string[] lines, int currentTextIndex)
        {
            OpenXmlElement currentElement = text;
            Break textBreak;

            var run = text.Parent as Run;
            for (int i = 1; i < lines.Count(); i++)
            {
                textBreak = new Break();
                run.InsertAfter(textBreak, currentElement);
                currentElement = textBreak;
                text = new Text(lines[i]);
                run.InsertAfter(text, currentElement);

                currentTextIndex++;
                currentElement = text;
            }
            return currentTextIndex;
        }

        /// <summary>
        /// Check text if match 'text to find' condition
        /// </summary>
        /// <param name="texts">Texts to iterate.</param>
        /// <param name="textElementIndex">
        /// Start point of current text element.
        /// </param>
        /// <param name="charElementIndex">
        /// Start point of current char in element.
        /// </param>
        /// <param name="find">Text to find.</param>
        /// <returns>Match expression.</returns>
        private static Match IsMatch(
            IEnumerable<Text> texts,
            int textElementIndex,
            int charElementIndex,
            string find
            )
        {
            int findIndex = 0;
            for (int i = textElementIndex; i < texts.Count(); i++)
            {
                string text = texts.ElementAt(i).Text;
                for (int j = charElementIndex; j < text.Length; j++)
                {
                    // Element mismatch
                    if (find[findIndex] != text[j])
                    {
                        return null; 
                    }

                    // Go to next character
                    findIndex++;

                    if (findIndex != find.Length)
                        continue;

                    return new Match()
                    {
                        EndElementIndex = i,
                        EndCharIndex = j
                    };
                }

                // Reset char index for next element
                charElementIndex = 0;
            }

            // Ran out of text, not a string match
            return null;
        }

        /// <summary>
        /// Figure out which Text element within the paragraph
        /// contains the starting point of the search string 
        /// and replace the existing text on that point.
        /// </summary>
        /// <param name="paragraph">Paragraph to search in.</param>
        /// <param name="find">Text expression to find.</param>
        /// <param name="replaceWith">Text expression to replace with.</param>
        public static void ReplaceText(Paragraph paragraph, string find, string replaceWith)
        {
            var texts = paragraph.Descendants<Text>();
            for (int t = 0; t < texts.Count(); t++)
            {
                Text txt = texts.ElementAt(t);
                for (int c = 0; c < txt.Text.Length; c++)
                {
                    var match = IsMatch(texts, t, c, find);
                    if (match != null)
                    {
                        string[] lines = replaceWith.SplitLines();
                        int len = lines.Length - 1;

                        // End of the replacement text.
                        int skip = lines[len].Length - 1; 

                        int endChar = match.EndCharIndex + 1;
                        int endElement = match.EndElementIndex;

                        // Check if it has a prefix
                        if (c > 0)
                        {
                            lines[0] = txt.Text.Substring(0, c) + lines[0];
                        }

                        // Check if it has a postfix
                        string text = texts.ElementAt(endElement).Text;
                        if (endChar < text.Length)
                        {
                            lines[len] += text[endChar..];
                        }

                        // In case value starts/ends with whitespace
                        txt.Space = new EnumValue<SpaceProcessingModeValues>
                            (SpaceProcessingModeValues.Preserve);
                        txt.Text = lines[0];

                        texts.ClearText(t + 1, endElement);

                        // If 'with' contained line breaks - add breaks back
                        if (lines.Count() > 1)
                        {
                            t = txt.WrapReplace(lines, t);

                            // New line
                            c = skip;
                        }
                        else
                        {
                            // Same line
                            c += skip;
                        }
                    }
                }
            }
        }
    }
}