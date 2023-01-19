using System.Windows.Input;

namespace WisdomLight.ViewModel.Data.Files.Fields.Tools
{
    public class EditCommands
    {
        public EditCommands(ICommand add, ICommand drop)
        {
            AddCommand = add;
            DropCommand = drop;
        }

        public ICommand AddCommand { get; }
        public ICommand DropCommand { get; }
    }
}
