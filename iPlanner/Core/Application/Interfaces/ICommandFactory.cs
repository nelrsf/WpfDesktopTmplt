namespace iPlanner.Core.Application.Interfaces
{
    public interface ICommandFactory<Model>
    {
        ICommand<Model>? GetCommand(Type type);
    }
}
