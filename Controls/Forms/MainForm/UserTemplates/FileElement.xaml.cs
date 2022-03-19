using System.Windows;
using System.IO;
using System.ComponentModel;
using WisdomLight.Model;

namespace WisdomLight.Controls.Forms.MainForm.UserTemplates
{
    /// <summary>
    /// Element containing file name info
    /// </summary>
    public partial class FileElement : TemplateControl, INotifyPropertyChanged
    {
        public FileElement()
        {
            InitializeComponent();
        }

        private FillTemplatesWindow
            CreateProgram(string name)
        {
            return new FillTemplatesWindow(name);
        }

        public FillTemplatesWindow CreateProgram
            (Document blanks, string name)
        {
            return new FillTemplatesWindow(blanks, name);
        }

        private FillTemplatesWindow TemplateProgram()
        {
            if (!File.Exists(FullName))
            {
                return CreateProgram(FileName);
            }

            Document program = LoadFromTemplate<Document>();

            if (program is null)
            {
                return CreateProgram(FileName);
            }
            else
            {
                return CreateProgram(program, FileName);
            }
        }

        private void DropTemplate(object sender, RoutedEventArgs e)
        {
            MainForm.DropTemplate(this);
        }

        private void CreateFromTemplate
            (object sender, RoutedEventArgs e)
        {
            FillTemplatesWindow Program = TemplateProgram();
            Program.Show();
        }
    }
}