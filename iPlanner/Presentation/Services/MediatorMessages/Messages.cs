using AvalonDock.Layout;
using iPlanner.Core.Application.AppMediator.Base;
using iPlanner.Core.Application.DTO;
using iPlanner.Presentation.Commands;
using iPlanner.Presentation.Interfaces;
using System.Collections.ObjectModel;

namespace iPlanner.Presentation.Services.MediatorMessages
{
    public class CloseFormMessage : MessageBase
    {
        public new IFormViewModel? sender { get; set; }

        public CloseFormMessage() : base(typeof(CloseFormCommand))
        {
        }
    }
    public class ReportMessage : MessageBase
    {

        public ReportDTO? Report { get; set; }
        public ActivityDTO? Activity { get; set; }
        public ICollection<LocationItemDTO>? Locations { get; set; }

        public ReportMessage(Type commandType) : base(commandType) { }
    }
    public class CommandMessage : MessageBase
    {
        public new Type CommandType { get; }

        public CommandMessage(Type commandType) : base(commandType)
        {
            CommandType = commandType;
        }
    }

    public class OpenViewMessage : MessageBase
    {
        public OpenViewMessage(Type commandType, string viewName) : base(commandType) { }

    }


    public class ViewMessage : MessageBase
    {
        public string ViewName { get; }
        public object Content;

        public ViewMessage(Type commandType, string viewName, object content) : base(commandType)
        {
            ViewName = viewName;
            Content = content;
        }
    }

    public class TabMessage : MessageBase
    {
        public LayoutDocument Document { get; }

        public TabMessage(Type commandType, LayoutDocument document) : base(commandType)
        {
            Document = document;
        }
    }

    public class TeamMessage : MessageBase
    {
        public TeamDTO? TeamToCreate { get; }
        public ObservableCollection<TeamDTO>? TeamsToRemove { get; }


        public TeamMessage(Type commandType, TeamDTO team) : base(commandType)
        {
            TeamToCreate = team;
        }

        public TeamMessage(Type commandType, ObservableCollection<TeamDTO> temsToDelete) : base(commandType)
        {
            TeamsToRemove = temsToDelete;
        }
    }

}
