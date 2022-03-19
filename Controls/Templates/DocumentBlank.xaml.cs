using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using WisdomLight.ViewModel;
using WisdomLight.Writers;

namespace WisdomLight.Controls.Templates
{
    /// <summary>
    /// Document template component
    /// </summary>
    public partial class DocumentBlank : UserControl, INotifyPropertyChanged, IRawData<string>
    {
        #region IRawData Members
        public string Raw()
        {
            return FullPath;
        }

        public void SetElement(string info)
        {
            FullPath = info;
        }
        #endregion

        #region DocumentBlank Members
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

        private string _fullPath;
        public string FullPath
        {
            get => _fullPath;
            set
            {
                _fullPath = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public DocumentBlank()
        {
            InitializeComponent();
        }

        private void SetFilePath(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = ResultRenderer.TemplateFilter
            };

            if (dialog.ShowDialog().Value)
            {
                FullPath = dialog.FileName;
            }
        }

        private void DropBlank(object sender, RoutedEventArgs e)
        {
            ViewModel.DropBlank(this);
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