using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Dialogs;

namespace iPlanner.Presentation.Commands
{
    public class OpenNewDialog : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public OpenNewDialog()
        {
        }
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

            NewViewDialog dialog = new NewViewDialog();
        }
    }
}
