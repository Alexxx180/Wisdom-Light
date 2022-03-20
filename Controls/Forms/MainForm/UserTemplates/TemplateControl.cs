using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using WisdomLight.Model;
using static WisdomLight.Writers.AutoGenerating.Processors;

namespace WisdomLight.Controls.Forms.MainForm.UserTemplates
{
    /// <summary>
    /// User input template file unit
    /// </summary>
    public class TemplateControl : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty
            MainFormProperty = DependencyProperty.Register(nameof(MainForm),
                typeof(MainPart), typeof(TemplateControl));

        public MainPart MainForm
        {
            get => GetValue(MainFormProperty) as MainPart;
            set => SetValue(MainFormProperty, value);
        }

        #region TemplateControl Members
        private string _fullName;
        public virtual string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FileName));
            }
        }

        public string FileName => Path.GetFileNameWithoutExtension(FullName);
        #endregion

        public Document LoadFromTemplate()
        {
            return LoadDocument(FullName);
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