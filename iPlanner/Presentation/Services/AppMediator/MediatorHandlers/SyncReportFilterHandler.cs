using iPlanner.Presentation.Commands.Reports;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;

namespace iPlanner.Presentation.Services.AppMediator.MediatorHandlers
{

    public class SyncReportFilterHandler : IMessageHandler<SyncReportFilterMessage>
    {
        private SyncReportFilterCommand? _syncReportFilterCommand;

        public void Handle(SyncReportFilterMessage message)
        {
            if (message.CommandType == typeof(SyncReportFilterCommand))
            {
                _syncReportFilterCommand = new SyncReportFilterCommand();
                _syncReportFilterCommand.SetMessage(message);
                _syncReportFilterCommand.Execute();
            }
        }
    }
}
