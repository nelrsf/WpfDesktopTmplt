using System.Windows;

namespace iPlanner.Presentation.Services.AppMediator.Base
{
    public abstract class MessageBase
    {
        public object? sender;
        public Type CommandType { get; set; }
        public MainWindow? window { get; set; }

        public ICollection<MessageBase>? innerMessages { get; set; }

        protected MessageBase(Type commandType)
        {
            CommandType = commandType;
        }
    }
}
