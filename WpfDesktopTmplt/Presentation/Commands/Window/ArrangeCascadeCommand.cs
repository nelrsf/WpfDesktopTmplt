using System.Collections.ObjectModel;
using System.Windows.Controls;
using AvalonDock;
using AvalonDock.Layout;
using WpfDesktopTmplt.Core.Application.Interfaces;
using WpfDesktopTmplt.Presentation.Commands.Window.Base;
using WpfDesktopTmplt.Presentation.ViewModels;

namespace WpfDesktopTmplt.Presentation.Commands.Window
{
    public class ArrangeCascadeCommand : ArrangeCommandBase, ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            base.Execute(parameter);
        }

        public override LayoutPanel ArrangePanels(ObservableCollection<LayoutDocument> documents)
        {
            LayoutDocumentPane panel = new LayoutDocumentPane();
            foreach (LayoutDocument document in documents) {
                panel.Children.Add(document);
            }
            LayoutPanel layoutPanel = new LayoutPanel();
            layoutPanel.Children.Add(panel);
            return layoutPanel;
        }

    }

}
