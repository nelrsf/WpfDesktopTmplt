namespace iPlanner.Presentation.Interfaces
{
    public interface INotification
    {
        public TaskCompletionSource<bool>? TaskCompletionSource { get; set; }
    }
}
