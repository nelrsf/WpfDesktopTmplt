using iPlanner;
using iPlanner.Presentation;
using iPlanner.Presentation.Commands;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.Base;
using iPlanner.Presentation.Services.AppMediator.MediatorHandlers;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;


public class AppMediatorService : IMediator
{
    private MainWindow? _window;
    private readonly Dictionary<Type, object> _handlers;

    public AppMediatorService()
    {
        InitializeMainWindow();
        _handlers = new Dictionary<Type, object>();
        InitializeHandlers();
    }

    private void InitializeHandlers()
    {
        var viewHandler = new ViewMessageHandler(new InsertNewViewCommand());
        var tabHandler = new TabMessageHandler(new SelectTabCommand());
        var teamHandler = new TeamMessageHandler();
        var genericHandler = new CommandMessageHandler();
        var reportHandler = new ReportMessageHandler();
        var syncFilterReportHandler = new SyncReportFilterHandler();
        var closeFormMessageHandler = new CloseFormMessageHandler(new CloseFormCommand());

        // Registramos los handlers
        RegisterHandler<ViewMessage>(viewHandler);
        RegisterHandler<TabMessage>(tabHandler);
        RegisterHandler<TeamMessage>(teamHandler);
        RegisterHandler<CommandMessage>(genericHandler);
        RegisterHandler<ReportMessage>(reportHandler);
        RegisterHandler<CloseFormMessage>(closeFormMessageHandler);
        RegisterHandler<SyncReportFilterMessage>(syncFilterReportHandler);
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

    private void InitializeMainWindow()
    {
        _window = MainWindowProvider.GetMainWindow();
    }

}