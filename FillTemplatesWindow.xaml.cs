using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WisdomLight.ViewModel;
using WisdomLight.Writers.AutoGenerating.Documents;
using static WisdomLight.Writers.AutoGenerating.Processors;
using static WisdomLight.Writers.ResultRenderer;
using WisdomLight.Model;

namespace WisdomLight
{
    /// <summary>
    /// Add new discipline program window
    /// </summary>
    public partial class FillTemplatesWindow : Window, INotifyPropertyChanged
    {
        #region DisciplineProgramWindow Members
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

        private string _originalFileName;

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public FillTemplatesWindow(string fileName)
        {
            InitializeComponent();
            _originalFileName = fileName;
            FileName = fileName;
            ViewModel = new FileViewModel();
        }

        public FillTemplatesWindow(Document program, string fileName) : this(fileName)
        {
            ViewModel.SetFromTemplate(program);
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.WasChanged())
                SavePreferences();

            Pair<string, bool> head = UserAgreement();
            if (head.Value)
            {
                FileDocument.WriteDocuments
                    (ViewModel.MakeDocument(), head.Name);
            }
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            if (ViewModel.WasChanged())
                SavePreferences();
        }

        private void SavePreferences()
        {
            ProcessJson(RuntimeDirectory + FileName,
                ViewModel.MakeDocument());
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