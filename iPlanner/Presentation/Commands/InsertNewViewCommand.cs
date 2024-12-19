using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.ViewModels.Layout;

namespace iPlanner.Presentation.Commands
{
    public class InsertNewViewCommand : ICommand
    {
        private string? _viewName;
        public object? Content;

        public string? ViewName
        {
            get { return _viewName; }
            set
            {
                if (value is string)
                {
                    _viewName = value;
                }
                else
                {
                    throw new ArgumentException("Párametro invalido en comando");
                }
            }
        }


        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (!CanExecute(parameter)) return;
            if (!(parameter is MainWindow)) return;
            if (_viewName == null) return;

            MainWindow mainWindow = (MainWindow)parameter;
            MainWindowViewModel mainWindowViewModel = mainWindow.DataContext as MainWindowViewModel;
            if (mainWindowViewModel != null)
            {
                if (Content == null)
                {
                    throw new InvalidOperationException("No se pudo cargar la vista");
                }
                mainWindowViewModel.AddView(Content, _viewName);
            }

        }
    }
}
