using AvalonDock.Layout;
using iPlanner.Presentation.Controls;
using iPlanner.Presentation.Controls.Sidebar;
using System.Windows;
using System.Windows.Controls;

namespace iPlanner.Presentation.ViewModels.Layout
{
    public class SidePanelManager
    {
        private readonly MainWindow _mainWindow;
        private RadioButton? _lastCheckedButton = null;
        private LayoutAnchorable? _currentPanel;
        private SidebarControl? SidebarPanel;
        private LocationsViewControl? LocationsView;
        private TeamsControl? TeamsControl;
        private LayoutAnchorable? explorerPanel;

        public SidePanelManager(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public void InitializeSidePanel()
        {
            if (_mainWindow == null) return;
            LocationsView = new LocationsViewControl();
            TeamsControl = new TeamsControl();
            explorerPanel = _mainWindow.explorerPanel;
            SidebarPanel = new SidebarControl();
            SidebarPanel.SetTitle("Explorador de Facilidades");
            explorerPanel.Content = LocationsView;
        }

        public void TabButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton currentButton)
            {
                if (_lastCheckedButton == currentButton)
                {
                    currentButton.IsChecked = false;
                    _lastCheckedButton = null;
                    if (_currentPanel != null)
                    {
                        _currentPanel.Hide();
                    }
                    return;
                }

                _lastCheckedButton = currentButton;

                // Mostrar el panel correspondiente
                switch (currentButton.Tag?.ToString())
                {
                    case "Explorer":
                        explorerPanel.Content = LocationsView;
                        _currentPanel = explorerPanel;
                        SidebarPanel.SetTitle("Explorador de Facilidades");
                        break;
                    case "Search":
                        explorerPanel.Content = TeamsControl;
                        _currentPanel = explorerPanel;
                        SidebarPanel.SetTitle("Buscar");
                        break;
                }

                if (_currentPanel != null && _currentPanel.IsHidden)
                {
                    _currentPanel.Show();
                }
            }
        }
    }
}
