using iPlanner.Presentation.Commands;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.Base;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;

namespace iPlanner.Presentation.Services.AppMediator.MediatorHandlers
{
    public class CloseFormMessageHandler : IMessageHandler<CloseFormMessage>
    {
        private readonly CloseFormCommand _command;
        public CloseFormMessageHandler(CloseFormCommand command)
        {
            _command = command;
        }
        public void Handle(CloseFormMessage message)
        {
            _command.SetMessage(message);
            _command.Execute();
        }

        public void Handle(MessageBase message)
        {
            CloseFormMessage? closeFormMessage = message.innerMessages?.OfType<CloseFormMessage>().FirstOrDefault();
            if (closeFormMessage != null)
            {
                Handle(closeFormMessage);
            }
        }
    }

}
