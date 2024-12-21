using AvalonDock.Layout;
using iPlanner.Core.Application.Interfaces;

namespace iPlanner.Presentation.Commands
{
    internal class ToggleSideBarCommand : ICommand<IMainWindow>
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(IMainWindow? parameter)
        {
            return true;
        }

        public void Execute(IMainWindow? mainWindow)
        {
            if (!CanExecute(mainWindow))
            {
                return;
            }


            LayoutAnchorable explorerPanel = ((MainWindow)mainWindow).explorerPanel;
            if (explorerPanel != null)
            {
                if (explorerPanel.IsHidden)
                {
                    explorerPanel.Show();
                }
                else
                {
                    explorerPanel.Hide();
                }
            }
        }
    }
}
