using System.Windows.Controls;
using AvalonDock;
using AvalonDock.Layout;
using iPlanner.Presentation.ViewModels;
using iPlanner.Presentation.Commands.Window.Base;
using iPlanner.Core.Application.Interfaces;

namespace iPlanner.Presentation.Commands.Window
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
