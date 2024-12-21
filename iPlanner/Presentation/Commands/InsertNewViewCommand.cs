using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.ViewModels.Layout;

namespace iPlanner.Presentation.Commands
{
    public class InsertNewViewCommand : ICommand<IMainWindow>
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

        public bool CanExecute(IMainWindow? parameter)
        {
            return true;
        }

        public void Execute(IMainWindow? mainWindow)
        {
            if (!CanExecute(mainWindow)) return;

            if (_viewName == null) return;


            MainWindowViewModel mainWindowViewModel = ((MainWindow)mainWindow).DataContext as MainWindowViewModel;
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
