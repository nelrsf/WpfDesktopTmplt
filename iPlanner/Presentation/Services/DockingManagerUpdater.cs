using AvalonDock;
using AvalonDock.Layout;
using System.Windows;
using System.Windows.Controls;

namespace iPlanner.Presentation.ViewModels.Layout
{
    public class DockingManagerEventsHandler
    {
        private DockingManager? _dockingManager;
        private List<Action<UserControl>> _subscribers;
        public event EventHandler? OnDockingManagerActiveContentNotFound;

        public DockingManagerEventsHandler()
        {
            _subscribers = new List<Action<UserControl>>();
        }

        public void RegisterDockingManager()
        {
            MainWindow? mainWindow = MainWindowProvider.GetMainWindow();
            if (mainWindow == null) return;
            _dockingManager = mainWindow.dockingManager;
            _dockingManager.ActiveContentChanged += DockingManager_ActiveContentChanged;
        }

        public void RegisterSubscriber(Action<UserControl> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            _subscribers.Add(action);
        }

        private void DockingManager_ActiveContentChanged(object? sender, EventArgs e)
        {
            if (sender is not DockingManager dockingManager) return;
            if (dockingManager == null) return;
            if (dockingManager.DataContext is not MainWindowViewModel mainWindowViewModel) return;

            var documents = mainWindowViewModel.Documents;
           

            var activeDocument = documents.Where(
                d =>
                {
                    return d.IsActive == true;
                }).FirstOrDefault();

            if (activeDocument == null)
            {
                OnDockingManagerActiveContentNotFound?.Invoke(this, EventArgs.Empty); 
                return;
            }

            if (activeDocument.Content is not UserControl control) return;
            foreach (var subscriber in _subscribers)
            {
                subscriber.Invoke(control);
            }

        }


    }
    public class DockingManagerUpdater
    {
        private readonly MainWindow? _mainWindow;

        public DockingManagerUpdater()
        {
            _mainWindow = MainWindowProvider.GetMainWindow();
        }

        public void UpdateDockingManager()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (_mainWindow.dockingManager == null) return;
                var documentPanes = _mainWindow.dockingManager.Layout.Descendents()
                    .OfType<LayoutDocumentPane>()
                    .ToList();

                if (documentPanes != null && documentPanes.Count > 0)
                {
                    foreach (var pane in documentPanes)
                    {
                        pane.Children.Clear();
                    }

                    int paneCounter = 0;
                    foreach (var doc in _mainWindow.ViewModel.Documents)
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
    }
}
