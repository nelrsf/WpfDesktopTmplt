using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvalonDock.Layout;
using WpfDesktopTmplt.Core.Application.Interfaces;

namespace WpfDesktopTmplt.Presentation.Commands
{
    internal class ToggleSideBarCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (!CanExecute(parameter)) { 
                return;
            }

            if(!(parameter is MainWindow))
            {
                return;
            }

            MainWindow mainWindow = (MainWindow)parameter;

            LayoutAnchorable explorerPanel = mainWindow.explorerPanel;
            if (explorerPanel != null) {
                if (explorerPanel.IsHidden) { 
                    explorerPanel.Show();
                } else
                {
                    explorerPanel.Hide();
                }
            }
        }
    }
}
