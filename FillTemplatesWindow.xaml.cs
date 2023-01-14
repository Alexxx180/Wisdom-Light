using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WisdomLight.ViewModel;
using System.Collections.Generic;
using WisdomLight.Model;
using static WisdomLight.ViewModel.Data.Files.Writers.AutoGenerating.Processors;
using static WisdomLight.ViewModel.Data.Files.Writers.ResultRenderer;
using System.Windows.Input;
using System.Windows.Controls;
using WisdomLight.ViewModel.Files.Fields;
using WisdomLight.ViewModel.Data.Files.Fields;

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
        #endregion

        public FillTemplatesWindow(string fileName)
        {
            InitializeComponent();
            ViewModel = new FileViewModel(
                fileName,
                new List<IExpression> {
                    new TextExpression() { Type = "Текст" },
                    new NumberExpression() { Type = "Число" },
                    new DateExpression() { Type = "Дата" }
                }
            );
        }

        public FillTemplatesWindow(Document program, string fileName) : this(fileName)
        {
            //ViewModel.SetFromTemplate(program);
            //ViewModel.IsChanged = false;
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