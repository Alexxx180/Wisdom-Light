using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WisdomLight.View.Controls
{
    /// <summary>
    /// Command binding button alternative
    /// </summary>
    public partial class CommandControl : UserControl
    {
        public CommandControl()
        {
            MouseLeftButtonDown += OnMouseLeftButtonDown;
            InitializeComponent();
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (Command != null)
            {
                if (Command.CanExecute(CommandParameter))
                {
                    Command.Execute(CommandParameter);
                }
            }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(CommandControl),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(CommandControl),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
    }
}
