using AvalonDock.Layout;
using WpfDesktopTmplt.Core.Application.Interfaces;
using WpfDesktopTmplt.Presentation.Commands;

namespace WpfDesktopTmplt.Presentation.Services
{
    public class AppMediatorService : IMediator
    {
        private ICommandDictionary commandDictionary;
        private IMainWindow window;
        private static AppMediatorService _instance;
        private AppMediatorService(ICommandDictionary dictionary)
        {
            commandDictionary = dictionary;
        }

        public static AppMediatorService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AppMediatorService(new CommandDictionary());
            }
            return _instance;
        }

        public void notify(object sender, int commandFlag)
        {
            if (window == null)
            {
                return;
            }

            ICommand command = commandDictionary.GetCommand(commandFlag);
            command.Execute(window);

        }

        public void notify(object sender, int commandFlag, object commandParameter)
        {
            if (window == null)
            {
                return;
            }

            if(commandParameter == null) return;


            if (commandFlag == CommandDictionary.InsertNewView)
            {
                InsertNewViewCommand insertNewViewCommand = new InsertNewViewCommand();
                insertNewViewCommand.ViewName = commandParameter as string;
                insertNewViewCommand.Execute(window);
            }

            if(commandFlag == CommandDictionary.SelectTab)
            {
                SelectTabCommand selectTabCommand = new SelectTabCommand();
                selectTabCommand.Document = commandParameter as LayoutDocument;
                selectTabCommand.Execute(window);
            }
        }

        public void RegisterMainWindow(IMainWindow mainWindow)
        {
            if (window == null)
            {
                window = mainWindow;
            }
        }

        public ICommandDictionary GetDictionary() { return commandDictionary; }

    }
}
