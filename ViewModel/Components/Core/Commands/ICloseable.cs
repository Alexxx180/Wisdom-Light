using System;

namespace WisdomLight.ViewModel.Components.Core.Commands
{
    public interface ICloseable
    {
        public Action Close { get; set; }
        public bool CanClose { get; }
    }
}
