using AvalonDock;
using AvalonDock.Layout;
using WpfDesktopTmplt.Core.Application.Interfaces;

namespace WpfDesktopTmplt.Presentation.Commands
{
    internal class SelectTabCommand : ICommand
    {
        private LayoutDocument _document;
        public LayoutDocument Document { 
            get { return _document; } 
            set {
                if (value is LayoutDocument)
                {
                    _document = value;
                }
                else {
                    throw new ArgumentException("Párametro Invalido");
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
            if (!CanExecute(parameter))
            {
                return;
            }

            if (_document == null)
            {
                return ;
            }

            _document.IsActive = true;

        }
    }
}
