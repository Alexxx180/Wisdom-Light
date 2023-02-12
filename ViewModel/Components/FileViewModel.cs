using System;
using System.Collections.Generic;
using System.Windows.Input;
using WisdomLight.ViewModel.Components.Core.Commands;
using WisdomLight.ViewModel.Components.Core.Processors.Export.Documents;
using WisdomLight.ViewModel.Components.Data;

namespace WisdomLight.ViewModel.Components
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
        
        public ICommand NewCommand { get; protected internal set; }
        public ICommand OpenCommand { get; protected internal set; }
        public ICommand SaveCommand { get; protected internal set; }
        public ICommand SaveAsCommand { get; protected internal set; }
        public ICommand ExportCommand { get; protected internal set; }
        public ICommand CloseCommand { get; protected internal set; }

        public ICommand AddInformation { get; protected internal set; }
        public ICommand DropInformation { get; protected internal set; }

        public ICommand AddLink { get; protected internal set; }
        public ICommand DropLink { get; protected internal set; }
        public ICommand OpenLink { get; protected internal set; }

        public ICommand AddQuery { get; protected internal set; }
        public ICommand DropQuery { get; protected internal set; }
        public ICommand OpenQuery { get; protected internal set; }

        public ICommand RelativePathCommand { get; protected internal set; }

        public TemplateViewModel Data { get; set; }
        public List<FileDocument> Exporters { get; set; }
    }
}