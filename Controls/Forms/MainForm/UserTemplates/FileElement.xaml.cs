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

        #region TemplateDeterming Logic
        private FillTemplatesWindow TemplateProgram()
        {
            if (!File.Exists(FullName))
            {
                return new FillTemplatesWindow(MainForm, FileName);
            }

            Document program = LoadFromTemplate();

            if (program is null)
            {
                return new FillTemplatesWindow(MainForm, FileName);
            }
            else
            {
                return new FillTemplatesWindow(MainForm, program, FileName);
            }
        }

        private void CreateFromTemplate
            (object sender, RoutedEventArgs e)
        {
            FillTemplatesWindow Program = TemplateProgram();
            Program.Show();
        }
        #endregion

        private void DropTemplate(object sender, RoutedEventArgs e)
        {
            MainForm.DropTemplate(this);
        }
    }
}