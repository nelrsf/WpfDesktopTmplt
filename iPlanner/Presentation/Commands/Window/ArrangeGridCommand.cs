using System.Collections.ObjectModel;
using System.Windows.Controls;
using AvalonDock;
using AvalonDock.Layout;
using iPlanner.Presentation.ViewModels;
using iPlanner.Presentation.Commands.Window.Base;
using iPlanner.Core.Application.Interfaces;

namespace iPlanner.Presentation.Commands.Window
{
    public class ArrangeGridCommand : ArrangeCommandBase, ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public override void Execute(object? parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            if(parameter == null) return;

            base.Execute(parameter);
        }

        public override LayoutPanel ArrangePanels(ObservableCollection<LayoutDocument> documents)
        {
            LayoutPanel paneVertical1 = new LayoutPanel();
            LayoutPanel paneVertical2 = new LayoutPanel();

            LayoutDocumentPane paneHorizontal1 = new LayoutDocumentPane();
            LayoutDocumentPane paneHorizontal2 = new LayoutDocumentPane();
            LayoutDocumentPane paneHorizontal3 = new LayoutDocumentPane();
            LayoutDocumentPane paneHorizontal4 = new LayoutDocumentPane();

            paneVertical1.Children.Add(paneHorizontal1);
            paneVertical1.Children.Add(paneHorizontal2);
            paneVertical2.Children.Add(paneHorizontal3);
            paneVertical2.Children.Add(paneHorizontal4);

            List<LayoutDocumentPane> listLayouts =
            [
                paneHorizontal1,
                paneHorizontal2,
                paneHorizontal3,
                paneHorizontal4,
            ];

            int counter = 0;
            foreach (var document in documents)
            {
                listLayouts[counter].Children.Add(document);
                counter++;
                if (counter > 3)
                {
                    counter = 0;
                }
            }

            LayoutPanel mainPanel = new LayoutPanel();
            paneVertical1.Orientation = Orientation.Vertical;
            paneVertical2.Orientation = Orientation.Vertical;
            mainPanel.Children.Add(paneVertical1);
            mainPanel.Children.Add(paneVertical2);
            return mainPanel;
        }


    }
}
