using System;
using System.Windows.Input;
using WisdomLight.ViewModel.Commands;
using WisdomLight.ViewModel.Components;

namespace WisdomLight.ViewModel
{
    public class FileViewModel : NotifyPropertyChanged, ICloseable
    {
        public FileViewModel() { }

        public Action Close { get; set; }

        private bool _canClose;
        public bool CanClose
        {
            get => _canClose;
            set
            {
                _canClose = value;
                OnPropertyChanged();
            }
        }

        public ICommand NewCommand { get; set; }
        public ICommand OpenCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand SaveAsCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public TemplateViewModel Data { get; set; }
    }
}