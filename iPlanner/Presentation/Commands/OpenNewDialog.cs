using iPlanner.Presentation.Dialogs;
using iPlanner.Core.Application.Interfaces;

namespace iPlanner.Presentation.Commands
{
    public class OpenNewDialog : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private IMediator _mediator;

        public OpenNewDialog() { 
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            NewViewDialog dialog = new NewViewDialog();
        }
    }
}
