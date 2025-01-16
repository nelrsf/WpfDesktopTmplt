using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.Base;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;

namespace iPlanner.Presentation.Services.AppMediator.MediatorHandlers
{
    public class CommandMessageHandler : IMessageHandler<CommandMessage>
    {
        public CommandMessageHandler()
        {
        }

        public void Handle(CommandMessage message)
        {
            if (message.CommandType == null)
                throw new ArgumentNullException(nameof(message.CommandType));

            ICommand? command = Activator.CreateInstance(message.CommandType) as ICommand;

            if (command == null)
                throw new InvalidOperationException($"No se pudo crear una instancia de ICommand desde {message.CommandType}");

            if (command is CommandInputMessageBase<CommandMessage>)
            {
                ((CommandInputMessageBase<CommandMessage>)command).SetMessage(message);
            }

            command.Execute();
        }
    }
}
