using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WisdomLight.ViewModel;

namespace WisdomLight.Controls.Expressions
{
    /// <summary>
    /// Record component containing discipline meta data
    /// </summary>
    public partial class MetaElement : UserControl, INotifyPropertyChanged, IRawData<Model.Expression>, IWrapFields
    {
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
        private FileViewModel _viewModel;
        public FileViewModel ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                OnPropertyChanged();
            }
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

        #region IWrapFields Members
        private bool _isWrap;
        public bool IsWrap
        {
            get => _isWrap;
            set
            {
                _isWrap = value;
                OnPropertyChanged();
            }
        }

        public void WrapFields()
        {
            IsWrap = !IsWrap;
        }
        #endregion

        public MetaElement()
        {
            InitializeComponent();
        }

        private void DropMetaData(object sender, RoutedEventArgs e)
        {
            ViewModel.DropMetaData(this);
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