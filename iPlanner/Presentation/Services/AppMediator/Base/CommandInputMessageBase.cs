using iPlanner.Presentation.Interfaces;

namespace iPlanner.Presentation.Services.AppMediator.Base
{
    public abstract class CommandInputMessageBase<Message> where Message : MessageBase
    {
        protected Message? message { get; set; }

        public void SetMessage(Message message)
        {
            this.message = message;
        }

        public void NotifyMessage(bool result)
        {
            if (message is INotification notification)
            {
                notification.TaskCompletionSource?.SetResult(result);
            }
        }
    }
}
