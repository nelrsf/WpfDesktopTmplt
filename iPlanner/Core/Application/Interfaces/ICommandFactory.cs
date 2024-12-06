using iPlanner.Presentation.Services;

namespace iPlanner.Core.Application.Interfaces
{
    public interface ICommandFactory
    {
        ICommand? GetCommand(CommandType commandType);
    }
}
