using iPlanner.Presentation.Commands;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;

namespace iPlanner.Presentation.Services.AppMediator.MediatorHandlers
{
    public class TabMessageHandler : IMessageHandler<TabMessage>
    {
        private readonly SelectTabCommand _command;

        public TabMessageHandler(SelectTabCommand command)
        {
            _command = command;
        }

        public void Handle(TabMessage message)
        {
            _command.Document = message.Document;
            _command.Execute();
        }
    }


}
