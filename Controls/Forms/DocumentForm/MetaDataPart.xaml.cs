using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WisdomLight.ViewModel;
using System.Windows;

namespace WisdomLight.Controls.Forms.DocumentForm
{
    /// <summary>
    /// Part responsible for theme plan editing
    /// </summary>
    public partial class MetaDataPart : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty
            ViewModelProperty = DependencyProperty.Register("ViewModel",
                typeof(FileViewModel), typeof(MetaDataPart));

        internal FileViewModel ViewModel
        {
            get => GetValue(ViewModelProperty) as FileViewModel;
            set
            {
                SetValue(ViewModelProperty, value);
                OnPropertyChanged();
            }
        }

        public MetaDataPart()
        {
            InitializeComponent();
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