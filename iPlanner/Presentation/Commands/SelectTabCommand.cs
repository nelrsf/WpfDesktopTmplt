using AvalonDock.Layout;
using iPlanner.Core.Application.Interfaces;

namespace iPlanner.Presentation.Commands
{
    public class SelectTabCommand : ICommand
    {
        private LayoutDocument? _document;
        public LayoutDocument? Document
        {
            get { return _document; }
            set
            {
                if (value is LayoutDocument)
                {
                    _document = value;
                }
                else
                {
                    throw new ArgumentException("Párametro Invalido");
                }
            }
        }
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            if (!CanExecute())
            {
                return;
            }

            if (_document == null)
            {
                return;
            }

            _document.IsActive = true;

        }
    }
}
