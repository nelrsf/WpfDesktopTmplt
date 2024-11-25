namespace WpfDesktopTmplt.Core.Application.Interfaces
{
    public interface IMediator
    {
        public void notify(object sender, int command);

        public void RegisterMainWindow(IMainWindow window);

        public void notify(object sender, int commandFlag, object commandParameter);
    }
}
