using System.Windows.Input;

namespace WisdomLight.ViewModel.Components.Core.Commands
{
    public interface ICloser : ICloseable
    {
        public ICommand CloseCommand { get; set; }
    }
}
