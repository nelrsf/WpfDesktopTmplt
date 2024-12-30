using AvalonDock.Layout;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Commands.Window.Base;
using System.Collections.ObjectModel;

namespace iPlanner.Presentation.Commands.Window
{
    public class ArrangeCascadeCommand : ArrangeCommandBase, ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute() => true;


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
