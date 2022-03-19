namespace WisdomLight.Controls
{
    /// <summary>
    /// Components model raw data wrapper
    /// </summary>
    public interface IRawData<T>
    {
        public T Raw();
        public void SetElement(T info);
    }
}