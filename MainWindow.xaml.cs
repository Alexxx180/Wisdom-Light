using System.Windows;
using System.Collections.ObjectModel;
using WisdomLight.ViewModel;
using WisdomLight.ViewModel.Commands;
using WisdomLight.Model;
using WisdomLight.ViewModel.Data.Files;
using WisdomLight.ViewModel.Data.Files.Processors.Serialization.Objects;

namespace WisdomLight
{
    /// <summary>
    /// Containing data templates
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Commands
        //    if (Keyboard.FocusedElement is TextBox textBox)
        //    {
        //        TraversalRequest tRequest = new
        //            TraversalRequest(FocusNavigationDirection.Next);
        //        _ = textBox.MoveFocus(tRequest);
        //    }

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
            KeyConfirmer dialog = DialogManager.Open(ViewModel.Serializer.Current);
            ViewModel.Serializer.Change(dialog.Key);
            new FillTemplatesWindow(ViewModel.Serializer.Load(dialog.Status.Path)).Show();
        }
        #endregion

        public MainWindow()
        {
            ViewModel = new MainViewModel(
                new ObservableCollection<string>() { "Тест" },
                new RelayCommand(argument => AddCommand(argument)),
                new RelayCommand(argument => DropCommand(argument)),
                new RelayCommand(argument => NewCommand(argument)),
                new RelayCommand(argument => OpenCommand(argument)),
                new RelayCommand(argument => Close()),
                true, true
            );
            InitializeComponent();
        }

        public MainViewModel ViewModel { get; }
    }
}