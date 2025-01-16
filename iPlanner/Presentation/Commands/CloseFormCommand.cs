using AvalonDock.Layout;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.Base;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;
using iPlanner.Presentation.ViewModels.Layout;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace iPlanner.Presentation.Commands
{
    public class CloseFormCommand : CommandInputMessageBase<CloseFormMessage>, ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            if (!CanExecute()) return;


            if (message == null) return;

            UserControl? userControl = message?.sender?.GetUserControl();
            if (userControl == null) return;
            MainWindowViewModel? mainWindowViewModel = ((MainWindow)message.window).ViewModel;
            if (mainWindowViewModel == null) return;
            ObservableCollection<LayoutDocument> documents = mainWindowViewModel.Documents;
            LayoutDocument layoutToRemove = null;
            foreach (LayoutDocument document in documents)
            {
                if (document.Content == userControl)
                {
                    layoutToRemove = document;
                }
            }
            if (layoutToRemove != null)
            {
                documents.Remove(layoutToRemove);
            }
            mainWindowViewModel.UpdateDockingManager();
        }
    }
}

