using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using System.Windows.Input;
using WisdomLight.ViewModel.Data.Files.Processors.Serialization.Objects;

namespace WisdomLight.ViewModel
{
    public class MainViewModel : NotifyPropertyChanged
    {
        [JsonInclude]
        public FileFiller Serializer { get; private set; }

        public MainViewModel(FileFiller serializer)
        {
            Serializer = serializer;
        }

        public MainViewModel(
            ObservableCollection<string> templates,
            ICommand add, ICommand drop, ICommand newCommand,
            ICommand open, ICommand close, bool isDefended, bool isDefaultPath)
        {
            Serializer = new FileFiller();
            Templates = templates;
            AddCommand = add;
            DropCommand = drop;
            OpenCommand = open;
            CloseCommand = close;
            NewCommand = newCommand;
            IsDefended = isDefended;
            IsDefaultPath = isDefaultPath;
        }

        public string SelectedLocation { get; set; }

        private string _userLocation;
        public string UserLocation
        {
            get => _userLocation;
            set
            {
                _userLocation = value;
                OnPropertyChanged();
            }
        }

        #region MainPart Members
        private ObservableCollection<string> _templates;
        public ObservableCollection<string> Templates
        {
            get => _templates;
            set
            {
                _templates = value;
                OnPropertyChanged();
            }
        }

        private bool _isDefended;
        public bool IsDefended
        {
            get => _isDefended;
            set
            {
                _isDefended = value;
                OnPropertyChanged();
            }
        }

        private bool _isDefaultPath;
        public bool IsDefaultPath
        {
            get => _isDefaultPath;
            set
            {
                _isDefaultPath = value;
                SelectedLocation = value ?
                    App.DefaultLocation : UserLocation;
                OnPropertyChanged();
            }
        }
        #endregion

        public ICommand AddCommand { get; }
        public ICommand DropCommand { get; }

        public ICommand NewCommand { get; }
        public ICommand OpenCommand { get; }

        public ICommand ImportCommand { get; }
        public ICommand SearchCommand { get; }

        public ICommand CloseCommand { get; }
    }
}
