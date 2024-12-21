using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Dialogs;

namespace iPlanner.Presentation.Commands
{
    public class OpenNewDialog : ICommand<object>
    {
        public event EventHandler? CanExecuteChanged;
        private IMediator _mediator;

        public OpenNewDialog()
        {
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
