using WisdomLight.ViewModel;

namespace WisdomLight.Model
{
    public class Bridge<T> : NotifyPropertyChanged, ICloneable<Bridge<T>> where T : ICloneable<T>
    {
        public Bridge() { }

        public Bridge(T item)
        {
            Item = item;
        }

        private T _item;
        public T Item
        {
            get => _item;
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }

        public Bridge<T> Clone()
        {
            return new Bridge<T>
            {
                Item = Item.Clone()
            };
        }
    }
}
