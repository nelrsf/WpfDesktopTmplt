using System.Collections;
using System.DirectoryServices;
using WpfDesktopTmplt.Core.Application.Interfaces;
using WpfDesktopTmplt.Presentation.Commands;
using WpfDesktopTmplt.Presentation.Commands.Window;

namespace WpfDesktopTmplt.Presentation.Services
{

    public class CommandDictionary : ICommandDictionary
    {
        //Constantes
        public static readonly int ArrangeVertical = 0;
        public static readonly int ArrangeHorizontal = 1;
        public static readonly int ArrangeCascade = 2;
        public static readonly int ArrangeGrid = 3;
        public static readonly int OpenNewViewDialog = 4;
        public static readonly int InsertNewView = 5;
        public static readonly int SelectTab = 6;
        public static readonly int ToggleSideBar = 7;
        private IDictionary dictionary;

        public CommandDictionary()
        {
            dictionary = new Dictionary<int, ICommand>();
            initializeCommands();
        }

        private void initializeCommands()
        {
            dictionary.Clear();
            dictionary.Add(ArrangeVertical, new ArrangeVerticalCommand());
            dictionary.Add(ArrangeHorizontal, new ArrangeHorizontalCommand());
            dictionary.Add(ArrangeCascade, new ArrangeCascadeCommand());
            dictionary.Add(ArrangeGrid, new ArrangeGridCommand());
            dictionary.Add(OpenNewViewDialog, new OpenNewDialog());
            dictionary.Add(InsertNewView, new InsertNewViewCommand());
            dictionary.Add(SelectTab, new SelectTabCommand());
            dictionary.Add(ToggleSideBar, new ToggleSideBarCommand());
        }

        public ICommand GetCommand(int index)
        {
            return (ICommand)dictionary[index];
        }


    }


}
