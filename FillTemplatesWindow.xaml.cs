using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WisdomLight.ViewModel;
using WisdomLight.ViewModel.Commands;
using WisdomLight.ViewModel.Data.Files;
using WisdomLight.Model;
using WisdomLight.ViewModel.Data.Files.Processors.Serialization.Objects;

namespace WisdomLight
{
    /// <summary>
    /// File-template program window
    /// </summary>
    public partial class FillTemplatesWindow : Window, INotifyPropertyChanged
    {
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

        #region Commands
        private void NewCommand(object argument)
        {
            new FillTemplatesWindow(ViewModel.Serializer, ViewModel.IsDefended).Show();
        }

        private void OpenCommand(object argument)
        {
            KeyConfirmer dialog = DialogManager.Open();
            new FillTemplatesWindow(ViewModel.Serializer.Load(dialog.Status.Path)).Show();
        }

        private void SaveAsCommand(object argument)
        {
            KeyConfirmer dialog = DialogManager.Save();
            ViewModel.Serializer.Save(dialog.Status.Path, ViewModel);
        }
        #endregion

        public FillTemplatesWindow(FileFiller serializer, bool isDefended)
        {
            InitializeComponent();
            ViewModel = new FileViewModel(
               serializer,
               new RelayCommand(argument => NewCommand(argument)),
               new RelayCommand(argument => OpenCommand(argument)),
               new RelayCommand(argument => SaveAsCommand(argument)),
               new RelayCommand(argument => Close()),
               isDefended
            );
        }

        public FillTemplatesWindow(FileViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
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