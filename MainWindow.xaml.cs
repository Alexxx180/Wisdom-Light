using System.Windows;
using System.Windows.Input;
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
        public MainWindow()
        {
            ViewModel = new MainViewModel(
                new ObservableCollection<string>()
                {
                    "Тест"
                },
                new RelayCommand(
                    argument =>
                    {

                    }
                ),
                new RelayCommand(
                    argument =>
                    {

                    }
                ),
                new RelayCommand(
                    argument =>
                    {
                        new FillTemplatesWindow((string)argument).Show();
                    }
                )
            );
            InitializeComponent();
        }

        public MainViewModel ViewModel { get; }
    }
}