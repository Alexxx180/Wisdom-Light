using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using WisdomLight.Customing;
using WisdomLight.ViewModel;

namespace WisdomLight.Controls.Forms
{
    /// <summary>
    /// UIElements switcher
    /// </summary>
    public partial class Switcher : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty
            GroupProperty = DependencyProperty.Register(
                nameof(Group), typeof(SwitchGroup), typeof(Switcher));

        public static readonly DependencyProperty
            ElementProperty = DependencyProperty.Register(nameof(Element),
                typeof(FrameworkElement), typeof(Switcher));

        public static readonly DependencyProperty
            IsNotPressedProperty = DependencyProperty.Register(
                nameof(IsNotPressed), typeof(bool), typeof(Switcher));

        #region Switcher Members
        public SwitchGroup Group
        {
            get => GetValue(GroupProperty) as SwitchGroup;
            set => SetValue(GroupProperty, value);
        }

        public FrameworkElement Element
        {
            get => GetValue(ElementProperty) as FrameworkElement;
            set => SetValue(ElementProperty, value);
        }

        public bool IsNotPressed
        {
            get => GetValue(IsNotPressedProperty).ToBool();
            set => SetValue(IsNotPressedProperty, value);
        }

        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        private Style _viewStyle;
        public Style ViewStyle
        {
            get => _viewStyle;
            set
            {
                _viewStyle = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public Switcher()
        {
            InitializeComponent();
            IsNotPressed = true;
        }

        private void Switch(object sender, RoutedEventArgs e)
        {
            IsNotPressed = false;
            Group.SwitchElement(this, Element);
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