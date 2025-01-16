
using System.Windows;
using System.Windows.Controls;

namespace iPlanner.Presentation.Dialogs
{
    public class ProgressDialog : Window
    {
        public ProgressDialog(string message)
        {
            // Configuración básica de ventana
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            SizeToContent = SizeToContent.WidthAndHeight;
            Topmost = true;

            // Contenido simple
            var panel = new StackPanel { Margin = new Thickness(20) };
            panel.Children.Add(new TextBlock { Text = message, Margin = new Thickness(0, 0, 0, 10) });
            panel.Children.Add(new ProgressBar { IsIndeterminate = true, Width = 200 });
            Content = panel;
        }

        public void ShowAndActivate()
        {
            Show();
            Activate();
        }
    }
}
