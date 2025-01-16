using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Commands;
using iPlanner.Presentation.Commands.Reports;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;

namespace iPlanner.Presentation.Services.AppMediator.MediatorHandlers
{
    public class ReportMessageHandler : IMessageHandler<ReportMessage>
    {
        private AddLocationReportCommand AddLocationsToReport;
        private RemoveLocationReportCommand RemoveLocationsFromReport;
        private CreateReportCommand CreateReportCommand;
        private UpdateReportCommand UpdateReportCommand;
        private CloseFormCommand CloseFormCommand;
        private DeleteReportCommand DeleteReportCommand;
        private CloseFormMessageHandler CloseFormMessageHandler;
        private IReportService _reportService;


        public ReportMessageHandler()
        {
            _reportService = AppServices.GetService<IReportService>();
            AddLocationsToReport = new AddLocationReportCommand();
            RemoveLocationsFromReport = new RemoveLocationReportCommand();
            CreateReportCommand = new CreateReportCommand(_reportService);
            UpdateReportCommand = new UpdateReportCommand(_reportService);
            DeleteReportCommand = new DeleteReportCommand(_reportService);
            CloseFormCommand = new CloseFormCommand();
            CloseFormMessageHandler = new CloseFormMessageHandler(CloseFormCommand);

        }

        public void Handle(ReportMessage message)
        {

            if (message.CommandType == typeof(AddLocationReportCommand))
            {
                AddLocationsToReport.SetMessage(message);
                AddLocationsToReport.Execute();
            }
            else if (message.CommandType == typeof(RemoveLocationReportCommand))
            {
                RemoveLocationsFromReport.SetMessage(message);
                RemoveLocationsFromReport.Execute();
            }
            else if (message.CommandType == typeof(CreateReportCommand))
            {
                CreateReportCommand.SetMessage(message);
                CreateReportCommand.Execute();
                CloseFormMessageHandler.Handle(message);
            }
            else if (message.CommandType == typeof(DeleteReportCommand))
            {
                DeleteReportCommand = new DeleteReportCommand(_reportService);
                DeleteReportCommand.SetMessage(message);
                DeleteReportCommand.Execute();

            }
            else if (message.CommandType == typeof(UpdateReportCommand))
            {
                UpdateReportCommand = new UpdateReportCommand(_reportService);
                UpdateReportCommand.SetMessage(message);
                UpdateReportCommand.Execute();
                CloseFormMessageHandler.Handle(message);
            }
        }
    }
}
