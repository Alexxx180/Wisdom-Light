using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WisdomLight.Model;

namespace WisdomLight.ViewModel.Components.Data.Units
{
    public class Querier : Queue<int>, ICloneable<Querier>
    {
        private string _path;
        public string Path
        {
            get => _path;
            set
            {
                _path = value;
                OnPropertyChanged();
            }
        }

        public Querier() : base()
        {
        }

        public Querier(IEnumerable<int> elements) : base(elements)
        {
        }

        public Querier(IEnumerable<int> elements, string path) : this(elements)
        {
            Path = path;
        }

        public Querier Clone()
        {
            return new Querier(this);
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        #endregion
    }
}
