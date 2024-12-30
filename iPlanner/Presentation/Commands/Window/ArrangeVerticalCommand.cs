using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Commands.Window.Base;

namespace iPlanner.Presentation.Commands.Window
{
    public class ArrangeVerticalCommand : ArrangeCommandBase, ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute()
        {
            return true;
        }

    }
}
