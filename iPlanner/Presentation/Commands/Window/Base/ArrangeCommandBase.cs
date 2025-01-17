﻿using AvalonDock;
using AvalonDock.Layout;
using iPlanner.Presentation.Services.AppMediator.Base;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;
using iPlanner.Presentation.ViewModels.Layout;
using System.Collections.ObjectModel;

namespace iPlanner.Presentation.Commands.Window.Base
{
    public class ArrangeCommandBase : CommandInputMessageBase<CommandMessage>
    {
        public LayoutAnchorablePane GetSideBar(MainWindow window)
        {
            return window.SideBar;
        }

        public ObservableCollection<LayoutDocument> GetAllDocuments(MainWindowViewModel mainWindowViewModel)
        {
            return mainWindowViewModel.Documents;
        }

        public virtual LayoutPanel ArrangePanels(ObservableCollection<LayoutDocument> documents)
        {
            List<LayoutDocumentPane> layouts = new List<LayoutDocumentPane>();

            LayoutDocumentPane pane1 = new LayoutDocumentPane();
            LayoutDocumentPane pane2 = new LayoutDocumentPane();
            LayoutDocumentPane pane3 = new LayoutDocumentPane();

            layouts.Add(pane1);
            layouts.Add(pane2);
            layouts.Add(pane3);

            int panelIndx = 0;
            foreach (var document in documents)
            {

                layouts[panelIndx].Children.Add(document);
                panelIndx++;
                if (panelIndx > 2)
                {
                    panelIndx = 0;
                }
            }
            LayoutPanel mainLayoutPanel = new LayoutPanel();
            mainLayoutPanel.Children.Add(layouts[0]);
            mainLayoutPanel.Children.Add(layouts[1]);
            mainLayoutPanel.Children.Add(layouts[2]);
            return mainLayoutPanel;
        }


        public virtual void Execute()
        {
            if(message == null) return;

            MainWindow? mainWindow = message.window as MainWindow;

            if (mainWindow == null)
            {
                return;
            }

            DockingManager dockingManager = mainWindow.dockingManager;

            if (dockingManager == null)
            {
                return;
            }

            var documents = GetAllDocuments((MainWindowViewModel)mainWindow.DataContext);
            if (documents.Count < 2) return;

            LayoutAnchorablePane sideBar = GetSideBar(mainWindow);
            if (sideBar.Children.Count == 0)
            {
                sideBar.Children.Add(RecoverSideBarContent(mainWindow));
            }

            LayoutPanel newLayoutPanel = new LayoutPanel();
            newLayoutPanel.Children.Add(sideBar);


            LayoutPanel mainLayoutDocumentPane = ArrangePanels(documents);

            newLayoutPanel.Children.Add(mainLayoutDocumentPane);

            dockingManager.Layout.RootPanel.Children.Clear();
            dockingManager.Layout.RootPanel.Children.Add(newLayoutPanel);
        }

        private LayoutAnchorable RecoverSideBarContent(MainWindow mainWindow)
        {
            DockingManager dockingManager = mainWindow.dockingManager;
            LayoutAnchorable layoutAnchorable = dockingManager.Layout.Descendents().OfType<LayoutAnchorable>().FirstOrDefault();
            return layoutAnchorable;
        }
    }

}
