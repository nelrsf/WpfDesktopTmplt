﻿using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Services.AppMediator.Base;

namespace iPlanner.Presentation.Interfaces
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
