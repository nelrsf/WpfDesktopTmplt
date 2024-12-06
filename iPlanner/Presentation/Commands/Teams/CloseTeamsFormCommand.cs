using AvalonDock.Layout;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Controls.Teams;
using iPlanner.Presentation.ViewModels;
using System.Collections.ObjectModel;

namespace iPlanner.Presentation.Commands.Teams
{
    public class CloseTeamsFormCommand : ICommand
    {
        public IMainWindow? MainWindow { get; internal set; }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (!CanExecute(parameter)) return;

            if (parameter is TeamFormViewModel)
            {
                TeamFormViewModel viewModel = (TeamFormViewModel)parameter;
                TeamFormControl teamsControl = viewModel.TeamsFormControl;
                MainWindowViewModel mainWindowViewModel = (MainWindowViewModel)((MainWindow)MainWindow).DataContext;
                ObservableCollection<LayoutDocument> documents = mainWindowViewModel.Documents;
                LayoutDocument layoutToRemove = null;
                foreach (LayoutDocument document in documents)
                {
                    if (document.Content == teamsControl)
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
