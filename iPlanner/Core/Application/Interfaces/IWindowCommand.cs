namespace iPlanner.Core.Application.Interfaces
{
    interface IWindowCommand<T> : ICommand<T>
    {
        public IMainWindow? MainWindow { get; internal set; }
    }
}
