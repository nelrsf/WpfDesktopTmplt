namespace iPlanner.Core.Application.Interfaces
{
    interface IWindowCommand : ICommand
    {
        public IMainWindow? MainWindow { get; internal set; }
    }
}
