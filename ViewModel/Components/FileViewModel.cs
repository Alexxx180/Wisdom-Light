﻿using System;
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

        public ICommand NewCommand { get; protected internal set; }
        public ICommand OpenCommand { get; protected internal set; }
        public ICommand SaveCommand { get; protected internal set; }
        public ICommand SaveAsCommand { get; protected internal set; }
        public ICommand CloseCommand { get; protected internal set; }

        public ICommand AddInformation { get; protected internal set; }
        public ICommand DropInformation { get; protected internal set; }
        public ICommand AddDocument { get; protected internal set; }
        public ICommand DropDocument { get; protected internal set; }

        public TemplateViewModel Data { get; set; }
    }
}