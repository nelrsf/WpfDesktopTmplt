using System.Windows.Controls;
using AvalonDock;
using AvalonDock.Layout;
using WpfDesktopTmplt.Core.Application.Interfaces;
using WpfDesktopTmplt.Presentation.Commands.Window.Base;
using WpfDesktopTmplt.Presentation.ViewModels;

namespace WpfDesktopTmplt.Presentation.Commands.Window
{
    public class ArrangeVerticalCommand : ArrangeCommandBase, ICommand
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
