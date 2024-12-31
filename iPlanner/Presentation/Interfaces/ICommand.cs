namespace iPlanner.Presentation.Interfaces
{
    public interface ICommand
    {
        event EventHandler? CanExecuteChanged;

        bool CanExecute();


        void Execute();

    }
}
