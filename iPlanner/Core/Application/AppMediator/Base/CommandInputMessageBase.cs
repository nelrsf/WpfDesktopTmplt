namespace iPlanner.Core.Application.AppMediator.Base
{
    public abstract class CommandInputMessageBase<Message> where Message : MessageBase
    {
        protected Message? message { get; set; }

        public void SetMessage(Message message)
        {
            this.message = message;
        }
    }
}
