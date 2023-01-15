namespace WisdomLight.View.Binds.Converters.Items
{
    /// <summary>
    /// Converts Master to the Target, and back again.
    /// </summary>
    public interface IItemConverter
    {
        /// <summary>
        /// Converts the specified master item.
        /// </summary>
        /// <param name="master">The master item.</param>
        /// <returns>Conversion result.</returns>
        public object Convert(object master);

        /// <summary>
        /// Converts the specified target item.
        /// </summary>
        /// <param name="target">The target item.</param>
        /// <returns>Conversion result.</returns>
        public object ConvertBack(object target);
    }
}
