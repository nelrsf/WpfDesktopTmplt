using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfDesktopTmplt.Core.Application.Interfaces;
using WpfDesktopTmplt.Presentation.ViewModels;

namespace WpfDesktopTmplt.Presentation.Commands
{
    public class InsertNewViewCommand : ICommand
    {
        private string _viewName;

        public string ViewName
        {
            get { return _viewName; }
            set {
                if (value is string)
                {
                    _viewName = value;
                }
                else {
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

            MainWindow mainWindow = (MainWindow) parameter;
            MainWindowViewModel mainWindowViewModel = mainWindow.DataContext as MainWindowViewModel;
            if (mainWindowViewModel != null) {
                mainWindowViewModel.AddView(_viewName);
            }

        }
    }
}
