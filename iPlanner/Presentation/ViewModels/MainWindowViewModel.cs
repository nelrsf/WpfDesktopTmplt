using iPlanner.Presentation.Controls;
using iPlanner.Presentation.Controls.Sidebar;
using AvalonDock;
using AvalonDock.Layout;
using iPlanner.Presentation.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using iPlanner.Core.Application.Interfaces;

namespace iPlanner.Presentation.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindow? mainWindow;

        private IMediator? _mediator;
        private LayoutAnchorablePane? SideBar { get; set; }
        private RadioButton? _lastCheckedButton = null;
        private LayoutAnchorable? _currentPanel { get; set; }
        private SidebarControl? SidebarPanel { get; set; }
        private LocationsViewControl? LocationsView { get; set; }

        private TeamsControl? TeamsControl { get; set; }
        private LayoutAnchorable? explorerPanel { get; set; }
        private LayoutAnchorable? locationsPanel { get; set; }
        private DockingManager? _dockingManager { get; set; }
        public ObservableCollection<LayoutDocument> Documents { get; private set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindowViewModel()
        {
            _mediator = AppServices.GetService<IMediator>();
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
            if(mainWindow==null) return;
            SideBar = mainWindow.SideBar;
            LocationsView = new LocationsViewControl();
            TeamsControl = new TeamsControl();
            explorerPanel = mainWindow.explorerPanel;
            SidebarPanel = new SidebarControl();
            SidebarPanel.SetTitle("Explorador de Facilidades");
            explorerPanel.Content = LocationsView;
        }

        private void InitializeViews()
        {
            LayoutDocument? home = new LayoutDocument
            {
                Title = ControlFactory.HOME_CONTROL,
                Content = Activator.CreateInstance(typeof(WelcomeControl)),
                CanClose = true,
            };
            home.Closed += DeleteDocument;
            Documents.Add(home);

            LayoutDocument? reportList = new LayoutDocument
            {
                CanClose = true,
                Title = ControlFactory.REPORT_LIST_CONTROL,
                Content = Activator.CreateInstance(typeof(ReportListControl))
            };
            reportList.Closed += DeleteDocument;
            Documents.Add(reportList);

        }


        public void UpdateDockingManager()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (_dockingManager == null) return; 
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

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void DeleteDocument(object? sender, EventArgs e)
        {
            if(sender == null) return;
            Documents.Remove((LayoutDocument)sender);
        }

        public void AddView(string viewName)
        {
            ControlFactory? controlFactory = new ControlFactory();
            Type? controlType = controlFactory.GetControl(viewName);
            LayoutDocument? newDocument = new LayoutDocument
            {
                CanClose = true,
                Title = viewName,
                Content = Activator.CreateInstance(controlType)
            };
            newDocument.Closed += DeleteDocument;
            Documents.Add(newDocument);
            UpdateDockingManager();
            _mediator.Notify(this, CommandType.SelectTab, newDocument);
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
