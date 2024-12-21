using AvalonDock.Layout;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.ViewModels.Layout;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace iPlanner.Presentation.Commands
{
    public class CloseFormCommand : IWindowCommand<IFormViewModel>
    {
        public IMainWindow? MainWindow { get; set; }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(IFormViewModel? parameter)
        {
            return true;
        }

        public void Execute(IFormViewModel? viewModel)
        {
            if (!CanExecute(viewModel)) return;

            if (viewModel == null) return;

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

