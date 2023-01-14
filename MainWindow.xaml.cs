using System.Windows;
using System.Collections.ObjectModel;
using WisdomLight.ViewModel;
using WisdomLight.ViewModel.Commands;

namespace WisdomLight
{
    /// <summary>
    /// Containing data templates
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Commands
        private void AddCommand(object argument)
        {

        }

        private void DropCommand(object argument)
        {

        }

        private void NewCommand(object argument)
        {
            new FillTemplatesWindow((string)argument).Show();
        }

        private void OpenCommand(object argument)
        {
            new FillTemplatesWindow((string)argument).Show();
        }

        private void CloseCommand()
        {
            Close();
        }
        #endregion

        public MainWindow()
        {
            ViewModel = new MainViewModel(
                new ObservableCollection<string>()
                {
                    "Тест"
                },
                new RelayCommand(argument => AddCommand(argument)),
                new RelayCommand(argument => DropCommand(argument)),
                new RelayCommand(argument => NewCommand(argument)),
                new RelayCommand(argument => OpenCommand(argument)),
                new RelayCommand(argument => CloseCommand()),
                false, true
            );
            InitializeComponent();
        }

        public MainViewModel ViewModel { get; }
    }
}