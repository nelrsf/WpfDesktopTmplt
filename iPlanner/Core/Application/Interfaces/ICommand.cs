namespace iPlanner.Core.Application.Interfaces
{
    public interface ICommand<T>
    {
        event EventHandler? CanExecuteChanged;

        bool CanExecute(T? parameter);


        void Execute(T? parameter);

    }
}
