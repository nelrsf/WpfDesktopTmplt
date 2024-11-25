using WpfDesktopTmplt.Core.Application.Interfaces;
using WpfDesktopTmplt.Presentation.Dialogs;

namespace WpfDesktopTmplt.Presentation.Commands
{
    public class OpenNewDialog : ICommand
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

            NewViewDialog dialog = new NewViewDialog();
        }
    }
}
