using System.Windows;

namespace iPlanner.Presentation
{
    public static class MainWindowProvider
    {
        private static MainWindow? _mainWindow;

        public static void SetMainWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public static MainWindow? GetMainWindow()
        {
            return _mainWindow;
        }
    }
}
