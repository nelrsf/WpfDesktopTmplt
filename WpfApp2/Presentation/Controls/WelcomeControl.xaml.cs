using System.Windows;
using System.Windows.Controls;

namespace WpfDesktopTmplt.Presentation.Controls
{
    public partial class WelcomeControl : UserControl
    {
        public WelcomeControl()
        {
            InitializeComponent();
        }

        private void NewProject_Click(object sender, RoutedEventArgs e)
        {
            // Implementar lógica para nuevo proyecto
            MessageBox.Show("Crear nuevo proyecto");
        }

        private void OpenProject_Click(object sender, RoutedEventArgs e)
        {
            // Implementar lógica para abrir proyecto
            MessageBox.Show("Abrir proyecto existente");
        }
    }
}