namespace iPlanner.Core.Application.Interfaces
{
    public interface ICommand
    {
        event EventHandler? CanExecuteChanged;

        bool CanExecute();


        void Execute();

    }
}
