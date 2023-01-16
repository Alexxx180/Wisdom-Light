using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using Serilog;
using WisdomLight.ViewModel.Data.Files.Writers.AutoGenerating;
using static WisdomLight.ViewModel.Data.Files.Writers.AutoGenerating.JsonProcessor;
using static WisdomLight.ViewModel.Data.Files.Writers.ResultRenderer;

namespace WisdomLight.ViewModel
{
    public class MainViewModel : NotifyPropertyChanged
    {
        public MainViewModel() { }

        public MainViewModel(
            ObservableCollection<string> templates,
            ICommand add, ICommand drop, ICommand newCommand,
            ICommand open, ICommand close, bool isDefended, bool isDefaultPath)
        {
            Templates = templates;
            AddCommand = add;
            DropCommand = drop;
            OpenCommand = open;
            CloseCommand = close;
            NewCommand = newCommand;
            IsDefended = isDefended;
            IsDefaultPath = isDefaultPath;
        }

        #region Templates Logic
        public void ReloadTemplates()
        {
            Templates.Clear();
            //LoadTemplates();

            //Templates = new ObservableCollection<FileElement>();

            //LoadTemplates();
        }

        //private void LoadTemplates()
        //{
        //    Log.Information("Loading templates from runtime folder");

        //    string[] files = LoadTemplateNames();
        //    for (byte i = 0; i < files.Length; i++)
        //    {
        //        string file = files[i];
        //        if (!file.IsJson())
        //        {
        //            continue;
        //        }

        //        _ = Files.Add(file);
        //        Templates.Add(file);
        //    }
        //}

        //internal void AddTemplate(string fullName, string fileName)
        //{
        //    if (Files.Contains(fullName))
        //        return;

        //    _ = Files.Add(fullName);
        //    Templates.Add(fileName.ToRuntime());
        //}

        //internal void DropTemplate(string name)
        //{
        //    if (Files.Contains(name))
        //    {
        //        _ = Files.Remove(name);
        //        TruncateFile(name);
        //    }

        //    _ = Templates.Remove(name);
        //}
        #endregion

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
                OnPropertyChanged();
            }
        }
        #endregion

        public ICommand AddCommand { get; }
        public ICommand DropCommand { get; }

        public ICommand NewCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand CloseCommand { get; }

        //#region TemplateDeterming Logic
        //private FillTemplatesWindow TemplateProgram()
        //{
        //    return new FillTemplatesWindow(MainForm, FileName);
        //    //if (!File.Exists(FullName))
        //    //{
        //    //    return new FillTemplatesWindow(MainForm, FileName);
        //    //}

        //    Document program = LoadFromTemplate();

        //    if (program is null)
        //    {
        //        return new FillTemplatesWindow(MainForm, FileName);
        //    }
        //    else
        //    {
        //        return new FillTemplatesWindow(MainForm, program, FileName);
        //    }
        //}

        //private void CreateFromTemplate
        //    (object sender, RoutedEventArgs e)
        //{
        //    FillTemplatesWindow Program = TemplateProgram();
        //    Program.Show();
        //}
        //#endregion

        //private void DropTemplate(object sender, RoutedEventArgs e)
        //{
        //    MainForm.DropTemplate(this);
        //}

        //private string _fileName;
        //public override string FullName
        //{
        //    get => _fileName;
        //    set
        //    {
        //        _fileName = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public FileElementAdditor()
        //{
        //    InitializeComponent();
        //}

        //private void AddTemplate(object sender, RoutedEventArgs e)
        //{
        //    MainForm.AddTemplate($"{FullName}.json", FullName);
        //}
    }
}
