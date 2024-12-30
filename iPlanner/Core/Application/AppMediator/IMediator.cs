using iPlanner.Core.Application.AppMediator.Base;
using iPlanner.Core.Application.Interfaces;

namespace iPlanner.Core.Application.AppMediator
{

    public interface IMessageHandler<TMessage>
    {
        void Handle(TMessage message);
    }
    public interface IMediator
    {
        void Notify<TMessage>(TMessage message) where TMessage : MessageBase;
        void RegisterMainWindow(IMainWindow mainWindow);
        void RegisterHandler<TMessage>(IMessageHandler<TMessage> handler);
    }
}

