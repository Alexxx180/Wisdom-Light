using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WisdomLight.Model;
using WisdomLight.ViewModel.Components.Core.Dialogs;

namespace WisdomLight.ViewModel.Components.Data.Units
{
    public class Querier : Stack<int>, INotifyPropertyChanged, ICloneable<Querier>
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public Querier() : base() { }

        public Querier(string placeholder) : base()
        {
            Name = placeholder;
        }

        public Querier(IEnumerable<int> elements) : base(elements) { }

        public Querier(IEnumerable<int> elements, string path) : this(elements)
        {
            Name = path;
        }

        public Querier Clone()
        {
            return new Querier(this, Name);
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
