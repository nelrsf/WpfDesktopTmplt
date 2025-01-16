using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Commands;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;

namespace iPlanner.Presentation.Services.AppMediator.MediatorHandlers
{
    public class ViewMessageHandler : IMessageHandler<ViewMessage>
    {
        private readonly InsertNewViewCommand _command;
        private readonly IMainWindow? _window;


        public ViewMessageHandler(InsertNewViewCommand command)
        {
            _command = command;
        }

        public void Handle(ViewMessage message)
        {
            _command.SetMessage(message);
            _command.Execute();
        }
    }
}
