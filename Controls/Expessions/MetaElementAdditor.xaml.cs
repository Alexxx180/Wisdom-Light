using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WisdomLight.ViewModel;
using System.Windows;

namespace WisdomLight.Controls.Expressions
{
    /// <summary>
    /// Special component to add new metadata
    /// </summary>
    public partial class MetaElementAdditor : UserControl, INotifyPropertyChanged, IRawData<Model.Expression>
    {
        public static readonly DependencyProperty
            ViewModelProperty = DependencyProperty.Register(
                nameof(ViewModel), typeof(FileViewModel),
                typeof(MetaElementAdditor));

        #region IRawData Members
        public Model.Expression Raw()
        {
            return new Model.Expression(MetaName, MetaValue);
        }

        public void SetElement(Model.Expression info)
        {
            MetaName = info.Name;
            MetaValue = info.Data;
        }
        #endregion

        #region MetaData Members
        public FileViewModel ViewModel
        {
            get => GetValue(ViewModelProperty) as FileViewModel;
            set => SetValue(ViewModelProperty, value);
        }

        private string _name;
        public string MetaName
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _value;
        public string MetaValue
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public MetaElementAdditor()
        {
            InitializeComponent();
        }

        private void AddMetaData(object sender, RoutedEventArgs e)
        {
            ViewModel.AddMetaData(Raw());
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