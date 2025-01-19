using iPlanner.Presentation.Services.AppMediator.Base;
using System.Windows;

namespace iPlanner.Presentation.Interfaces
{

    public interface IMessageHandler<TMessage>
    {
        void Handle(TMessage message);
    }
    public interface IMediator
    {
        void Notify<TMessage>(TMessage message) where TMessage : MessageBase;
        //void RegisterMainWindow(Window mainWindow);
        void RegisterHandler<TMessage>(IMessageHandler<TMessage> handler);
    }
}

