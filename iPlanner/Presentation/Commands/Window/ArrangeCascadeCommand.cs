using AvalonDock.Layout;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Commands.Window.Base;
using System.Collections.ObjectModel;

namespace iPlanner.Presentation.Commands.Window
{
    public class ArrangeCascadeCommand : ArrangeCommandBase, ICommand<object>
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public override void Execute(object? parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            if (parameter == null) return;

            base.Execute(parameter);
        }

        public override LayoutPanel ArrangePanels(ObservableCollection<LayoutDocument> documents)
        {
            LayoutDocumentPane panel = new LayoutDocumentPane();
            foreach (LayoutDocument document in documents)
            {
                panel.Children.Add(document);
            }
            LayoutPanel layoutPanel = new LayoutPanel();
            layoutPanel.Children.Add(panel);
            return layoutPanel;
        }

    }

}
