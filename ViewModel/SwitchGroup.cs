using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WisdomLight.Controls.Forms;
using static WisdomLight.Customing.Decorators;

namespace WisdomLight.ViewModel
{
    public class SwitchGroup : INotifyPropertyChanged
    {
        private Switcher _activeSwitcher;
        public Switcher ActiveSwitcher
        {
            get => _activeSwitcher;
            set
            {
                if (_activeSwitcher != null)
                    _activeSwitcher.IsNotPressed = true;
                _activeSwitcher = value;
                OnPropertyChanged();
            }
        }

        private FrameworkElement _activeElement;
        public FrameworkElement ActiveElement
        {
            get => _activeElement;
            set
            {
                if (_activeElement != null)
                    _activeElement.SetActive(false);
                _activeElement = value;
                _activeElement.SetActive(true);
                OnPropertyChanged();
            }
        }

        public void
            SwitchElement(Switcher switcher,
            FrameworkElement element)
        {
            ActiveSwitcher = switcher;
            ActiveElement = element;
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