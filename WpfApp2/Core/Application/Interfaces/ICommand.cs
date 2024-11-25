namespace WpfDesktopTmplt.Core.Application.Interfaces
{
    public interface ICommand
    {
        event EventHandler? CanExecuteChanged;

        bool CanExecute(object? parameter);


        void Execute(object? parameter);

    }
}
