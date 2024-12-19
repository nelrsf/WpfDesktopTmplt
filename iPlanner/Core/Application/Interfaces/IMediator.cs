namespace iPlanner.Core.Application.Interfaces
{

    public interface IMessageHandler<TMessage> 
    {
        void Handle(TMessage message);
    }
    public interface IMediator
    {
        void Notify<TMessage>(TMessage message);
        void RegisterMainWindow(IMainWindow mainWindow);
        void RegisterHandler<TMessage>(IMessageHandler<TMessage> handler);
    }
}

