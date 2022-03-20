using System.Windows;
using System.ComponentModel;

namespace WisdomLight.Controls.Forms.MainForm.UserTemplates
{
    /// <summary>
    /// Special component to add new file name to list
    /// </summary>
    public partial class FileElementAdditor : TemplateControl, INotifyPropertyChanged
    {
        private string _fileName;
        public override string FullName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                OnPropertyChanged();
            }
        }

        public FileElementAdditor()
        {
            InitializeComponent();
        }

        private void AddTemplate(object sender, RoutedEventArgs e)
        {
            MainForm.AddTemplate(FullName + ".json", FullName);
        }
    }
}