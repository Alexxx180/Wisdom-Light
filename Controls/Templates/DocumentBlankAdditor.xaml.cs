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
    /// Special component to add new document template
    /// </summary>
    public partial class DocumentBlankAdditor : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty
            ViewModelProperty = DependencyProperty.Register(
                nameof(ViewModel), typeof(FileViewModel),
                typeof(DocumentBlankAdditor));

        #region DocumentBlank Members
        public FileViewModel ViewModel
        {
            get => GetValue(ViewModelProperty) as FileViewModel;
            set => SetValue(ViewModelProperty, value);
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

        public DocumentBlankAdditor()
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

        private void AddBlank(object sender, RoutedEventArgs e)
        {
            ViewModel.AddBlank(FullPath);
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
