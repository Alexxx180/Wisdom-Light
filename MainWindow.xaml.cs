using System.Windows;
using System.Collections.ObjectModel;
using WisdomLight.ViewModel;
using WisdomLight.ViewModel.Commands;
using WisdomLight.Model;
using WisdomLight.ViewModel.Data.Files;

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
            new FillTemplatesWindow(ViewModel.Serializer, ViewModel.IsDefended).Show();
        }

        private void OpenCommand(object argument)
        {
            KeyConfirmer dialog = DialogManager.Open();
            new FillTemplatesWindow(ViewModel.Serializer.Load(dialog.Status.Path)).Show();
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
                new RelayCommand(argument => Close()),
                false, true
            );
            InitializeComponent();
        }

        public MainViewModel ViewModel { get; }
    }
}