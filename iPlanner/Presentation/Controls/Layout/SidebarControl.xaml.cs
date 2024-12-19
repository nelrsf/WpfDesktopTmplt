using System.Windows;
using System.Windows.Controls;

namespace iPlanner.Presentation.Controls
{
    public partial class SidebarControl : UserControl
    {
        public SidebarControl()
        {
            InitializeComponent();
        }

        public void SetVisibility(Visibility visibility)
        {
            this.Visibility = visibility;
        }

        public void SetTitle(string title)
        {
            // Asegúrate de que PanelTitle es el nombre de tu TextBlock en el XAML
            if (PanelTitle != null)
            {
                PanelTitle.Text = title;
            }
        }
    }
}
