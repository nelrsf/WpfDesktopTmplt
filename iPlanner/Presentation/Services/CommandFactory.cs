using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Commands;
using iPlanner.Presentation.Commands.Reports;
using iPlanner.Presentation.Commands.Teams;
using iPlanner.Presentation.Commands.Window;
using System.Collections;

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

    public class CommandFactory : ICommandFactory
    {

        private IDictionary dictionary;


        public CommandFactory()
        {
            dictionary = new Dictionary<CommandType, ICommand>();
            initializeCommands();
        }

        private void initializeCommands()
        {
            dictionary.Clear();
            dictionary.Add(CommandType.ArrangeVertical, AppServices.GetService<ArrangeVerticalCommand>());
            dictionary.Add(CommandType.ArrangeHorizontal, AppServices.GetService <ArrangeHorizontalCommand>());
            dictionary.Add(CommandType.ArrangeCascade, AppServices.GetService <ArrangeCascadeCommand>());
            dictionary.Add(CommandType.ArrangeGrid, AppServices.GetService<ArrangeGridCommand>());
            dictionary.Add(CommandType.OpenNewViewDialog, AppServices.GetService<OpenNewDialog>());
            dictionary.Add(CommandType.InsertNewView, AppServices.GetService<InsertNewViewCommand>());
            dictionary.Add(CommandType.SelectTab, AppServices.GetService<SelectTabCommand>());
            dictionary.Add(CommandType.ToggleSideBar, AppServices.GetService<ToggleSideBarCommand>());
            dictionary.Add(CommandType.AddTeamMember, AppServices.GetService<AddMemberCommand>());
            dictionary.Add(CommandType.AddTeam, AppServices.GetService<AddTeamCommand>());
            dictionary.Add(CommandType.DeleteTeams, AppServices.GetService<RemoveTeamsCommand>());
            dictionary.Add(CommandType.CloseForm, AppServices.GetService<CloseFormCommand>());
            dictionary.Add(CommandType.RemoveLocationsFromReport, AppServices.GetService<RemoveLocationReportCommand>());
            dictionary.Add(CommandType.AddLocationsToReport, AppServices.GetService<AddLocationReportCommand>());
            dictionary.Add(CommandType.CreateReport, AppServices.GetService<CreateReportCommand>());
        }

        public ICommand? GetCommand(CommandType commandType)
        {
            return dictionary[commandType] as ICommand;
        }


    }


}
