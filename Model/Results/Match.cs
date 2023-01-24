namespace WisdomLight.Model
{
    /// <summary>
    /// Match expression container:
    /// Generally used to store last XML
    /// Text element and character indexes
    /// </summary>
    public class Match
    {
        /// <summary>
        /// Last matching element index
        /// containing part of the search text
        /// </summary>
        public int Element { get; set; }

        /// <summary>
        /// Last matching char index of the
        /// search text in last matching element
        /// </summary>
        public int Character { get; set; }
    }
}
