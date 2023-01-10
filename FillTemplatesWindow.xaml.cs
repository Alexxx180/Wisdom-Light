using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WisdomLight.Controls.Forms.MainForm;
using WisdomLight.ViewModel;
using WisdomLight.Writers.AutoGenerating.Documents;
using WisdomLight.Model;
using static WisdomLight.Writers.AutoGenerating.Processors;
using static WisdomLight.Writers.ResultRenderer;
using System.Windows.Input;
using System.Windows.Controls;


namespace WisdomLight
{
    /// <summary>
    /// Add new discipline program window
    /// </summary>
    public partial class FillTemplatesWindow : Window, INotifyPropertyChanged
    {
        #region DisciplineProgramWindow Members
        private MainPart _mainForm;
        public MainPart MainForm
        {
            get => _mainForm;
            set
            {
                _mainForm = value;
                OnPropertyChanged();
            }
        }

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

        public FillTemplatesWindow(MainPart mainForm, string fileName)
        {
            InitializeComponent();
            MainForm = mainForm;
            _originalFileName = fileName;
            FileName = fileName;
            ViewModel = new FileViewModel();
        }

        public FillTemplatesWindow(MainPart mainForm, Document program, string fileName) : this(mainForm, fileName)
        {
            ViewModel.SetFromTemplate(program);
            ViewModel.IsChanged = false;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Save();

            Pair<string, bool> head = UserAgreement();
            if (head.Value)
            {
                FileDocument.WriteDocuments(ViewModel, $"{head.Name}\\");
            }
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            if (Keyboard.FocusedElement is TextBox textBox)
            {
                TraversalRequest tRequest = new
                    TraversalRequest(FocusNavigationDirection.Next);
                _ = textBox.MoveFocus(tRequest);
            }

            Save();
            MainForm.ReloadTemplates();
        }

        private void Save()
        {
            if (_originalFileName != FileName)
            {
                RenameFile(_originalFileName, FileName);
                _originalFileName = FileName;
            }

            if (ViewModel.WasChanged())
                SavePreferences();
        }

        private void SavePreferences()
        {
            TruncateFile(_originalFileName);
            SaveRuntime(FileName, ViewModel);
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