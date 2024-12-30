using iPlanner.Core.Application.AppMediator;
using iPlanner.Core.Application.AppMediator.Base;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Commands;
using iPlanner.Presentation.Services.MediatorMessages;
using static iPlanner.Presentation.Services.MediatorMessages.CloseFormMessageHandler;

public class AppMediatorService : IMediator
{
    private IMainWindow? _window;
    private readonly Dictionary<Type, object> _handlers;

    public AppMediatorService()
    {
        _handlers = new Dictionary<Type, object>();
    }

    private void InitializeHandlers()
    {
        var viewHandler = new ViewMessageHandler(new InsertNewViewCommand());
        var tabHandler = new TabMessageHandler(new SelectTabCommand());
        var teamHandler = new TeamMessageHandler();
        var genericHandler = new CommandMessageHandler();
        var reportHandler = new ReportMessageHandler();
        var closeFormMessageHandler = new CloseFormMessageHandler(new CloseFormCommand());

        // Registramos los handlers
        RegisterHandler<ViewMessage>(viewHandler);
        RegisterHandler<TabMessage>(tabHandler);
        RegisterHandler<TeamMessage>(teamHandler);
        RegisterHandler<CommandMessage>(genericHandler);
        RegisterHandler<ReportMessage>(reportHandler);
        RegisterHandler<CloseFormMessage>(closeFormMessageHandler);
    }

    public void RegisterHandler<TMessage>(IMessageHandler<TMessage> handler)
    {
        _handlers[typeof(TMessage)] = handler;
    }

    public void Notify<TMessage>(TMessage message) where TMessage : MessageBase
    {
        if (_window == null)
            throw new InvalidOperationException("Window is not initialized");

        SetMainWindow(message);

        if (_handlers.TryGetValue(typeof(TMessage), out var handler))
        {
            ((IMessageHandler<TMessage>)handler).Handle(message);
        }
        else
        {
            throw new InvalidOperationException($"No handler registered for message type {typeof(TMessage)}");
        }
    }

    private void SetMainWindow(MessageBase message)
    {
        message.window = _window;
        if (message.innerMessages != null && message.innerMessages.Count > 0)
        {
            foreach (MessageBase innerMsg in message.innerMessages)
            {
                SetMainWindow(innerMsg);
            }
        }
    }

    public void RegisterMainWindow(IMainWindow mainWindow)
    {
        _window = _window ?? mainWindow;
        InitializeHandlers();
    }

}