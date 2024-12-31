using iPlanner.Presentation.Commands.Window.Base;
using iPlanner.Presentation.Interfaces;

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
