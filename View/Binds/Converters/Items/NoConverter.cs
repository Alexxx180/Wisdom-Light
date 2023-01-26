namespace WisdomLight.View.Binds.Converters.Items
{
    /// <summary>
    /// Does not convert.
    /// </summary>
    internal class NoConverter : IItemConverter
    {
        /// <returns>Same object.</returns>
        public object Convert(object master)
        {
            return master;
        }

        /// <returns>Same object.</returns>
        public object ConvertBack(object target)
        {
            return target;
        }
    }
}
