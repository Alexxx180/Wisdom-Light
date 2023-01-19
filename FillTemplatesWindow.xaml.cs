using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WisdomLight.ViewModel;
using WisdomLight.ViewModel.Commands;
using WisdomLight.ViewModel.Data.Files;
using WisdomLight.Model;
using WisdomLight.ViewModel.Data.Files.Processors.Serialization.Objects;
using Result = System.Windows.Forms.DialogResult;
using WisdomLight.ViewModel.Data.Files.Processors.Serialization;
using System.IO;

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
            new FillTemplatesWindow(ViewModel.CurrentLocation,
                ViewModel.Serializer, ViewModel.IsDefended, ViewModel.IsRelative).Show();
        }

        private void OpenCommand(object argument)
        {
            KeyConfirmer dialog = DialogManager.Open(ViewModel.Serializer.Current);
            if (dialog.Status.Result != Result.OK)
                return;
            string path = dialog.Status.Path;
            ViewModel.Serializer.Change(dialog.Key);
            new FillTemplatesWindow(path, ViewModel.Serializer,
                ViewModel.Serializer.Load(dialog.Status.Path)).Show();
        }

        private void SaveCommand(string path)
        {
            ViewModel.Serializer.FixedSave(path, ViewModel);
        }

        private void SaveAsCommand(object argument)
        {
            KeyConfirmer dialog = DialogManager.Save(ViewModel.Serializer.Current);
            if (dialog.Status.Result != Result.OK)
                return;
            ViewModel.Serializer.Change(dialog.Key);
            ViewModel.Serializer.Save(dialog.Status.Path, ViewModel);
        }
        #endregion

        public FillTemplatesWindow(string path, FileFiller serializer, bool isDefended, bool isRelative)
        {
            InitializeComponent();
            ViewModel = new FileViewModel(serializer, isDefended, isRelative);
            ViewModel.CurrentLocation = path;
            ViewModel.SetCommands(
               new RelayCommand(argument => NewCommand(argument)),
               new RelayCommand(argument => OpenCommand(argument)),
               new RelayCommand(argument => SaveCommand($"{path}\\{ViewModel.Name}")),
               new RelayCommand(argument => SaveAsCommand(argument)),
               new RelayCommand(argument => Close())
            );
        }

        public FillTemplatesWindow(string path, FileFiller serializer, FileViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            ViewModel.CurrentLocation = Path.GetDirectoryName(path);
            ViewModel.SetSerializer(serializer);
            ViewModel.SetCommands(
               new RelayCommand(argument => NewCommand(argument)),
               new RelayCommand(argument => OpenCommand(argument)),
               new RelayCommand(argument => SaveCommand(path)),
               new RelayCommand(argument => SaveAsCommand(argument)),
               new RelayCommand(argument => Close())
            );
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