using iPlanner.Presentation.Dialogs;
using iPlanner.Presentation.Interfaces;

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
