using System.Collections.ObjectModel;
using System.Windows.Controls;
using AvalonDock;
using AvalonDock.Layout;
using WpfDesktopTmplt.Core.Application.Interfaces;
using WpfDesktopTmplt.Presentation.Commands.Window.Base;
using WpfDesktopTmplt.Presentation.ViewModels;

namespace WpfDesktopTmplt.Presentation.Commands.Window
{
    public class ArrangeGridCommand : ArrangeCommandBase, ICommand
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

            List<LayoutDocumentPane> listLayouts = new List<LayoutDocumentPane>();
            listLayouts.Add(paneHorizontal1);
            listLayouts.Add(paneHorizontal2);
            listLayouts.Add(paneHorizontal3);
            listLayouts.Add(paneHorizontal4);

            int counter = 0;
            foreach (var document in documents) {
                listLayouts[counter].Children.Add(document);
                counter++;
                if(counter > 3)
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
