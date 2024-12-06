using AvalonDock.Layout;
using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Commands;
using iPlanner.Presentation.Commands.Teams;
using System.Collections.ObjectModel;
using System.Windows;

namespace iPlanner.Presentation.Services
{
    public class AppMediatorService : IMediator
    {
        private ICommandFactory _commandFactory;
        private IMainWindow? window;

        public AppMediatorService(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
        }


        public void Notify(object sender, CommandType commandType)
        {
            if (window == null)
            {
                return;
            }

            if(_commandFactory == null) return;

            ICommand? command = _commandFactory.GetCommand(commandType);

            if (command != null) 
            command.Execute(window);

        }

        public void Notify(object sender, CommandType commandType, object commandParameter)
        {
            if (window == null)
            {
                return;
            }

            if (commandParameter == null) return;


            if (commandType == CommandType.InsertNewView)
            {
                InsertNewViewCommand insertNewViewCommand = AppServices.GetService<InsertNewViewCommand>();
                insertNewViewCommand.ViewName = commandParameter as string;
                insertNewViewCommand.Execute(window);
            }

            if (commandType == CommandType.SelectTab)
            {
                SelectTabCommand selectTabCommand = AppServices.GetService<SelectTabCommand>();
                selectTabCommand.Document = commandParameter as LayoutDocument;
                selectTabCommand.Execute(window);
            }

            if (commandType == CommandType.AddTeamMember)
            {
                AddMemberCommand addMemberCommand = AppServices.GetService<AddMemberCommand>();
                if (commandParameter is TeamDTO)
                {
                    addMemberCommand.Execute(commandParameter);
                }
            }


            CloseTeamsFormCommand closeTeamsFormCommand = AppServices.GetService<CloseTeamsFormCommand>();
            if (commandType == CommandType.SaveTeam)
            {
                SaveTeamsCommand saveCommand = AppServices.GetService<SaveTeamsCommand>();
                closeTeamsFormCommand.MainWindow = window;
                if (commandParameter is TeamDTO)
                {
                    saveCommand.Execute(commandParameter);
                    MessageBox.Show("Equipo guardado con exito");
                    closeTeamsFormCommand.Execute(sender);
                }
            }

            if (commandType == CommandType.DeleteTeam)
            {
                RemoveTeamsCommand removeTeamsCommand = AppServices.GetService<RemoveTeamsCommand>();
                if (commandParameter is ObservableCollection<TeamDTO>)
                {
                    removeTeamsCommand.Execute(commandParameter);
                }
            }

            if(commandType == CommandType.CloseTeamsForm)
            {
                closeTeamsFormCommand.Execute(sender);
            }
        }

        public void RegisterMainWindow(IMainWindow mainWindow)
        {
            if (window == null)
            {
                window = mainWindow;
            }
        }

        public ICommandFactory GetDictionary() { return _commandFactory; }

    }
}
