using iPlanner.Presentation.Services.AppMediator.Base;
using System.Windows.Controls;

namespace iPlanner.Presentation.Services.AppMediator.MediatorMessages
{
    public class SyncReportFilterMessage : MessageBase
    {
        private readonly UserControl _control;

        public UserControl Control { get { return _control; } }

        public SyncReportFilterMessage(UserControl Control, Type commandType) : base(commandType)
        {
            _control = Control;
        }

    }
}