using System.Windows;
using WisdomLight.ViewModel.Components.Core.Commands;

namespace WisdomLight.View.Binds.Attach
{
    public class WindowCloser
    {
        public static bool GetEnableWindowClosing(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableWindowClosingProperty);
        }

        public static void SetEnableWindowClosing(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableWindowClosingProperty, value);
        }

        public static readonly DependencyProperty
            EnableWindowClosingProperty = DependencyProperty.RegisterAttached
            ("EnableWindowClosing", typeof(bool), typeof(WindowCloser),
                new PropertyMetadata(false, OnEnableWindowClosingChanged));

        private static void OnEnableWindowClosingChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is Window window)
            {
                window.Loaded += (s, e) =>
                {
                    if (window.Content is ICloseable viewmodel)
                    {
                        viewmodel.Close += () => window.Close();
                        window.Closing += (s, e) =>
                        {
                            e.Cancel = !viewmodel.CanClose;
                        };
                    }
                };
            }
        }

    }
}
