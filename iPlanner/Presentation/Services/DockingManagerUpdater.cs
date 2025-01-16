using AvalonDock.Layout;
using System.Windows;

namespace iPlanner.Presentation.ViewModels.Layout
{
    public class DockingManagerUpdater
    {
        private readonly MainWindow _mainWindow;

        public DockingManagerUpdater(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
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
