using System;

namespace WisdomLight.ViewModel.Commands
{
    public interface ICloseable
    {
        public Action Close { get; set; }
        public bool CanClose { get; }
    }
}
