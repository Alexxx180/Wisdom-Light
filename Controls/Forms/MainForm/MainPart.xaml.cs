using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Serilog;
using WisdomLight.ViewModel;
using WisdomLight.Controls.Forms.MainForm.UserTemplates;
using static WisdomLight.Writers.AutoGenerating.Processors;
using WisdomLight.Model;
using System.Collections.Generic;
using static WisdomLight.Writers.ResultRenderer;

namespace WisdomLight.Controls.Forms.MainForm
{
    /// <summary>
    /// Part responsible for add/load program calls
    /// </summary>
    public partial class MainPart : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty
            ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
                typeof(FileViewModel), typeof(MainPart));

        #region MainPart Members
        public FileViewModel ViewModel
        {
            get => GetValue(ViewModelProperty) as FileViewModel;
            set => SetValue(ViewModelProperty, value);
        }

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
        }

        private void LoadTemplates()
        {
            Log.Information("Loading templates from folder: " +
                RuntimeDirectory);
            try
            {
                foreach (string file in
                    Directory.GetFiles(RuntimeDirectory))
                {
                    if (Path.GetExtension(file).ToLower() != ".json")
                    {
                        continue;
                    }

                    _ = Files.Add(file);

                    FileElement template = new FileElement
                    {
                        FullName = file,
                        MainForm = this
                    };
                    Templates.Add(template);
                }
            }
            catch (IOException exception)
            {
                Log.Error("Exception on templates loading: " +
                    exception.Message);
                LoadMessage(exception.Message);
            }
        }

        public void AddTemplate(string fullName, string fileName)
        {
            if (Files.Contains(fullName))
                return;

            _ = Files.Add(fullName);
            FileElement template = new FileElement
            {
                FullName = $"{RuntimeDirectory}{fileName}.json"
            };
            Templates.Add(template);
            OnPropertyChanged(nameof(Templates));
        }

        public void DropTemplate(FileElement template)
        {
            string name = template.FullName;
            if (Files.Contains(name))
            {
                _ = Files.Remove(name);
                File.Delete(RuntimeDirectory + name);
            }

            _ = Templates.Remove(template);
            OnPropertyChanged(nameof(Templates));
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