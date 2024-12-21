using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Commands.Window.Base;

namespace iPlanner.Presentation.Commands.Window
{
    public class ArrangeVerticalCommand : ArrangeCommandBase, ICommand<object>
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            base.Execute(parameter);

        }

    }
}
