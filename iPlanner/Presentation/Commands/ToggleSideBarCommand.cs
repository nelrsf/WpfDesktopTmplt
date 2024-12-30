using AvalonDock.Layout;
using iPlanner.Core.Application.AppMediator.Base;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Services.MediatorMessages;

namespace iPlanner.Presentation.Commands
{
    internal class ToggleSideBarCommand : CommandInputMessageBase<ViewMessage>, ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            if (!CanExecute())
            {
                return;
            }

            MainWindow? mainWindow = (MainWindow?)(message?.window);
            if (mainWindow == null)return;

            LayoutAnchorable explorerPanel = mainWindow.explorerPanel;
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
