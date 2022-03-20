using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Serilog;
using WisdomLight.Controls.Forms.MainForm.UserTemplates;
using static WisdomLight.Writers.AutoGenerating.Processors;
using static WisdomLight.Writers.ResultRenderer;

namespace WisdomLight.Controls.Forms.MainForm
{
    /// <summary>
    /// Part responsible for add/load program calls
    /// </summary>
    public partial class MainPart : UserControl, INotifyPropertyChanged
    {
        #region MainPart Members
        private ObservableCollection<FileElement> _templates;
        public ObservableCollection<FileElement> Templates
        {
            get => _templates;
            set
            {
                _templates = value;
                OnPropertyChanged();
            }
        }

        public bool CanAdd => (Templates != null ?
            Templates.Count : 0) < byte.MaxValue;
        #endregion

        public MainPart()
        {
            InitializeComponent();
            Templates = new ObservableCollection<FileElement>();

            LoadTemplates();
        }

        #region Templates Logic
        public void ReloadTemplates()
        {
            Templates.Clear();
            LoadTemplates();
            OnPropertyChanged(nameof(CanAdd));
        }

        private void LoadTemplates()
        {
            Log.Information("Loading templates from runtime folder");
            try
            {
                string[] files = Directory.GetFiles(RuntimeDirectory);

                for (byte i = 0; i < files.Length; i++)
                {
                    string file = files[i];

                    if (Path.GetExtension(file).ToLower() != ".json")
                    {
                        continue;
                    }

                    _ = Files.Add(file);

                    FileElement template = new FileElement
                    {
                        MainForm = this,
                        FullName = file
                    };
                    Templates.Add(template);
                }
            }
            catch (IOException exception)
            {
                Log.Error($"Loading exception: {exception.Message}");
                LoadMessage(exception.Message);
            }
        }

        internal void AddTemplate(string fullName, string fileName)
        {
            if (Files.Contains(fullName))
                return;

            _ = Files.Add(fullName);
            FileElement template = new FileElement
            {
                MainForm = this,
                FullName = $"{RuntimeDirectory}{fileName}.json"
            };

            Templates.Add(template);
            Refresh();
        }

        internal void DropTemplate(FileElement template)
        {
            string name = template.FullName;

            if (Files.Contains(name))
            {
                _ = Files.Remove(name);
                TruncateFile(name);
            }

            _ = Templates.Remove(template);
            Refresh();
        }

        private void Refresh()
        {
            OnPropertyChanged(nameof(Templates));
            OnPropertyChanged(nameof(CanAdd));
        }
        #endregion

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