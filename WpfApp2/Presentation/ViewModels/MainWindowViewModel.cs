using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using AvalonDock;
using AvalonDock.Layout;
using WpfDesktopTmplt.Presentation.Controls;
using WpfDesktopTmplt.Presentation.Services;

namespace WpfDesktopTmplt.Presentation.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindow mainWindow;
        private LayoutAnchorablePane SideBar { get; set; }
        private RadioButton _lastCheckedButton = null;
        private LayoutAnchorable _currentPanel { get; set; }
        private SidebarControl SidebarPanel { get; set; }
        private LayoutAnchorable explorerPanel { get; set; }
        private DockingManager? _dockingManager { get; set; }
        public ObservableCollection<LayoutDocument> Documents { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            Documents = new ObservableCollection<LayoutDocument>();
            InitializeViews();
        }

        public void IntializeDockingManager(DockingManager dockingManager)
        {
            _dockingManager = dockingManager;
            UpdateDockingManager();
        }

        public void InitializeSidePanel()
        {
            SideBar = mainWindow.SideBar; 
            explorerPanel = mainWindow.explorerPanel;
            SidebarPanel = mainWindow.SidebarPanel;
            SidebarPanel.SetTitle("Explorador de Soluciones");
        }

        private void InitializeViews()
        {
            LayoutDocument home = new LayoutDocument
            {
                Title = ControlFactory.HOME_CONTROL,
                Content = Activator.CreateInstance(typeof(WelcomeControl)),
                CanClose = true,
            };
            home.Closed += DeleteDocument;
            Documents.Add(home);
        }


        public void UpdateDockingManager()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var documentPanes = _dockingManager.Layout.Descendents()
                    .OfType<LayoutDocumentPane>()
                    .ToList();

                if (documentPanes != null && documentPanes.Count > 0)
                {
                    foreach (var pane in documentPanes)
                    {
                        pane.Children.Clear();
                    }

                    int paneCounter = 0;
                    foreach (var doc in Documents)
                    {
                        documentPanes[paneCounter].Children.Add(doc);
                        paneCounter++;

                        if (paneCounter >= documentPanes.Count)
                        {
                            paneCounter = 0;
                        }
                    }
                }
            });
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void DeleteDocument(object? sender, EventArgs e)
        {
            Documents.Remove((LayoutDocument)sender);
        }

        public void AddView(string viewName)
        {
            ControlFactory controlFactory = new ControlFactory();
            Type controlType = controlFactory.GetControl(viewName);
            LayoutDocument newDocument = new LayoutDocument
            {
                CanClose = true,
                Title = viewName,
                Content = Activator.CreateInstance(controlType)
            };
            newDocument.Closed += DeleteDocument;
            Documents.Add(newDocument);
            UpdateDockingManager();
            AppMediatorService mediator = AppMediatorService.GetInstance();
            mediator.notify(this, CommandDictionary.SelectTab, newDocument);
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
                        _currentPanel = explorerPanel;
                        SidebarPanel.SetTitle("Explorador de soluciones");
                        break;
                    case "Search":
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
