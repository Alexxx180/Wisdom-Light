using System.Text.RegularExpressions;

namespace WisdomLight.Writers
{
    /// <summary>
    /// Additional tools-pack for open xml
    /// </summary>
    public static class OpenXmlTools
    {
        // Filters control characters but allows only properly-formed surrogate sequences
        private static Regex _invalidXMLChars = new Regex(
            @"(?<![\uD800-\uDBFF])[\uDC00-\uDFFF]|[\uD800-\uDBFF](?![\uDC00-\uDFFF])|[\x00-\x08\x0B\x0C\x0E-\x1F\x7F-\x9F\uFEFF\uFFFE\uFFFF]",
            RegexOptions.Compiled);
        /// <summary>
        /// Removes any unusual unicode characters that can't be encoded into XML which give exception on save
        /// </summary>
        public static string RemoveInvalidXMLChars(string text)
        {
            return string.IsNullOrEmpty(text) ? "" :
                _invalidXMLChars.Replace(text, "");
        }
    }
}