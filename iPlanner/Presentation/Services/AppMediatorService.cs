using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Commands;
using iPlanner.Presentation.Services;
using iPlanner.Presentation.Services.MediatorMessages;

public class AppMediatorService : IMediator
{
    private readonly ICommandFactory _commandFactory;
    private IMainWindow? _window;
    private readonly Dictionary<Type, object> _handlers;

    public AppMediatorService(ICommandFactory commandFactory)
    {
        _commandFactory = commandFactory;
        _handlers = new Dictionary<Type, object>();
    }

    private void InitializeHandlers()
    {
        // Creamos las instancias de los handlers
        var viewHandler = new ViewMessageHandler(_commandFactory.GetCommand(CommandType.InsertNewView) as InsertNewViewCommand, _window);
        var tabHandler = new TabMessageHandler(_commandFactory.GetCommand(CommandType.SelectTab) as SelectTabCommand, _window);
        var teamHandler = new TeamMessageHandler(_commandFactory, _window);
        var genericHandler = new CommandMessageHandler(_commandFactory, _window);
        var reportHandler = new ReportMessageHandler(_commandFactory);

        // Registramos los handlers
        RegisterHandler<ViewMessage>(viewHandler);
        RegisterHandler<TabMessage>(tabHandler);
        RegisterHandler<TeamMessage>(teamHandler);
        RegisterHandler<CommandMessage>(genericHandler);
        RegisterHandler<ReportMessage>(reportHandler);
    }

    public void RegisterHandler<TMessage>(IMessageHandler<TMessage> handler)
    {
        _handlers[typeof(TMessage)] = handler;
    }

    public void Notify<TMessage>(TMessage message)
    {
        if (_window == null)
            throw new InvalidOperationException("Window is not initialized");

        if (_handlers.TryGetValue(typeof(TMessage), out var handler))
        {
            ((IMessageHandler<TMessage>)handler).Handle(message);
        }
        else
        {
            throw new InvalidOperationException($"No handler registered for message type {typeof(TMessage)}");
        }
    }

    public void RegisterMainWindow(IMainWindow mainWindow)
    {
        _window = _window ?? mainWindow;
        InitializeHandlers();
    }

    public ICommandFactory GetDictionary() => _commandFactory;
}