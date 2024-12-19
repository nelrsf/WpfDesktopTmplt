using AvalonDock.Layout;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Controls.Teams;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.ViewModels.Layout;
using iPlanner.Presentation.ViewModels.Teams;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace iPlanner.Presentation.Commands.Teams
{
    public class CloseFormCommand : IWindowCommand
    {
        public IMainWindow? MainWindow { get; set; }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (!CanExecute(parameter)) return;

            if (parameter is IFormViewModel)
            {
                IFormViewModel viewModel = (IFormViewModel)parameter;
                UserControl userControl = viewModel.GetUserControl();
                MainWindowViewModel mainWindowViewModel = (MainWindowViewModel)((MainWindow)MainWindow).DataContext;
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
}
