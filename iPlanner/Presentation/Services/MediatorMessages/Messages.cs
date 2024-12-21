using AvalonDock.Layout;
using iPlanner.Core.Application.DTO;
using System.Collections.ObjectModel;

namespace iPlanner.Presentation.Services.MediatorMessages
{
    public abstract class MessageBase
    {
        public object? sender;

        public CommandType CommandType { get; }

        protected MessageBase(CommandType commandType)
        {
            CommandType = commandType;
        }
    }

    public class ReportMessage : MessageBase
    {

        public ReportDTO Report { get; set; }
        public ActivityDTO Activity { get; set; }
        public ICollection<LocationItemDTO> Locations { get; set; }

        public ReportMessage(CommandType commandType) : base(commandType) { }
    }
    public class CommandMessage
    {
        public Type CommandType { get; }

        public CommandMessage(Type commandType)
        {
            CommandType = commandType;
        }
    }

    public class OpenViewMessage : MessageBase
    {
        public CommandType CommandType { get; }

        public OpenViewMessage(CommandType commandType, string viewName) : base(commandType) { }

    }


    public class ViewMessage : MessageBase
    {
        public string ViewName { get; }
        public object Content;

        public ViewMessage(string viewName, object content) : base(CommandType.InsertNewView)
        {
            ViewName = viewName;
            Content = content;
        }
    }

    public class TabMessage : MessageBase
    {
        public LayoutDocument Document { get; }

        public TabMessage(LayoutDocument document) : base(CommandType.SelectTab)
        {
            Document = document;
        }
    }

    public class TeamMessage : MessageBase
    {
        public TeamDTO? TeamToCreate { get; }
        public ObservableCollection<TeamDTO>? TeamsToRemove { get; }

        public TeamMessage(TeamDTO team, CommandType commandType) : base(commandType)
        {
            TeamToCreate = team;
        }

        public TeamMessage(ObservableCollection<TeamDTO> temsToDelete, CommandType commandType) : base(commandType)
        {
            TeamsToRemove = temsToDelete;
        }
    }

}
