﻿using AvalonDock.Layout;
using iPlanner.Presentation.Commands.Window.Base;
using iPlanner.Presentation.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace iPlanner.Presentation.Commands.Window
{
    public class ArrangeHorizontalCommand : ArrangeCommandBase, ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute() => true;

        public override LayoutPanel ArrangePanels(ObservableCollection<LayoutDocument> documents)
        {
            LayoutDocumentPane pane1 = new LayoutDocumentPane();
            LayoutDocumentPane pane2 = new LayoutDocumentPane();


            List<LayoutDocumentPane> layoutList = new List<LayoutDocumentPane>();
            layoutList.Add(pane1);
            layoutList.Add(pane2);

            int panelIndx = 0;
            foreach (var document in documents)
            {

                layoutList[panelIndx].Children.Add(document);
                panelIndx++;
                if (panelIndx > 1)
                {
                    panelIndx = 0;
                }
            }
            LayoutPanel mainPanel = new LayoutPanel();
            mainPanel.Children.Add(pane1);
            mainPanel.Children.Add(pane2);
            mainPanel.Orientation = Orientation.Vertical;
            return mainPanel;

        }

    }

}