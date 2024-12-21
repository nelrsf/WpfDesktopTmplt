using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Commands;
using iPlanner.Presentation.Commands.Window;
using iPlanner.Presentation.Interfaces;

namespace iPlanner.Presentation.Services
{
    public enum CommandType
    {
        ArrangeVertical,
        ArrangeHorizontal,
        ArrangeCascade,
        ArrangeGrid,
        OpenNewViewDialog,
        InsertNewView,
        SelectTab,
        ToggleSideBar,
        AddTeamMember,
        AddTeam,
        DeleteTeams,
        CloseForm,
        SaveTeams,
        RemoveLocationsFromReport,
        AddLocationsToReport,
        CreateReport
    }

    public class CommandFactory : ICommandFactory<object>
    {

        private Dictionary<Type, Func<ICommand<object>>> dictionary;


        public CommandFactory()
        {
            dictionary = new Dictionary<Type, Func<ICommand<object>>>();
            initializeCommands();
        }

        private void initializeCommands()
        {
            dictionary.Clear();
            dictionary.Add(typeof(ArrangeVerticalCommand), () => AppServices.GetService<ArrangeVerticalCommand>());
            dictionary.Add(typeof(ArrangeHorizontalCommand), () => AppServices.GetService<ArrangeHorizontalCommand>());
            dictionary.Add(typeof(ArrangeCascadeCommand), () => AppServices.GetService<ArrangeCascadeCommand>());
            dictionary.Add(typeof(ArrangeGridCommand), () => AppServices.GetService<ArrangeGridCommand>());
            dictionary.Add(typeof(OpenNewDialog), () => AppServices.GetService<OpenNewDialog>());
            dictionary.Add(typeof(SelectTabCommand), () => AppServices.GetService<SelectTabCommand>());
            //dictionary.Add(typeof(AddMemberCommand), () => AppServices.GetService<AddMemberCommand>());
            //dictionary.Add(typeof(AddTeamCommand), () => AppServices.GetService<AddTeamCommand>());
            //dictionary.Add(typeof(RemoveTeamsCommand), () => AppServices.GetService<RemoveTeamsCommand>());
            //dictionary.Add(typeof(RemoveLocationReportCommand), () => AppServices.GetService<RemoveLocationReportCommand>());
            //dictionary.Add(typeof(AddLocationReportCommand), () => AppServices.GetService<AddLocationReportCommand>());
            //dictionary.Add(typeof(CreateReportCommand), () => AppServices.GetService<CreateReportCommand>());
        }

        public ICommand<object>? GetCommand(Type type)
        {
            return dictionary[type].Invoke();
        }

    }

    public class CommandWindowFactory : ICommandFactory<IMainWindow>
    {
        private Dictionary<Type, Func<ICommand<IMainWindow>>> dictionary;


        public CommandWindowFactory()
        {
            dictionary = new Dictionary<Type, Func<ICommand<IMainWindow>>>();
            initializeCommands();
        }

        private void initializeCommands()
        {
            dictionary.Clear();
            dictionary.Add(typeof(InsertNewViewCommand), () => AppServices.GetService<InsertNewViewCommand>());
            dictionary.Add(typeof(ToggleSideBarCommand), () => AppServices.GetService<ToggleSideBarCommand>());

        }
        public ICommand<IMainWindow>? GetCommand(Type type)
        {
            return dictionary[type].Invoke();
        }
    }

    public class ViewModelCommandFactory : ICommandFactory<IFormViewModel>
    {
        private Dictionary<Type, Func<ICommand<IFormViewModel>>> dictionary;


        public ViewModelCommandFactory()
        {
            dictionary = new Dictionary<Type, Func<ICommand<IFormViewModel>>>();
            initializeCommands();
        }

        public ICommand<IFormViewModel>? GetCommand(Type type)
        {
            return dictionary[type].Invoke();
        }

        private void initializeCommands()
        {
            dictionary.Clear();
            dictionary.Add(typeof(CloseFormCommand), () => AppServices.GetService<CloseFormCommand>());
        }

    }
}