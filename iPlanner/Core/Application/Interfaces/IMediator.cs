using iPlanner.Presentation.Services;

namespace iPlanner.Core.Application.Interfaces
{
    public interface IMediator
    {
        public void Notify(object sender, CommandType commandType);

        public void RegisterMainWindow(IMainWindow window);

        public void Notify(object sender, CommandType commandType, object commandParameter);
    }
}
