using iPlanner.Core.Application.Interfaces;

namespace iPlanner.Presentation.Services.AppMediator.Base
{
    public abstract class MessageBase
    {
        public object? sender;
        public Type CommandType { get; set; }
        public IMainWindow? window { get; set; }

        public ICollection<MessageBase>? innerMessages { get; set; }

        protected MessageBase(Type commandType)
        {
            CommandType = commandType;
        }
    }
}
