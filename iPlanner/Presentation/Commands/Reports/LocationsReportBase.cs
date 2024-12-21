using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Services.MediatorMessages;
using System.Collections.ObjectModel;

namespace iPlanner.Presentation.Commands.Reports
{
    public class LocationsReportBase : ICommand<ReportMessage>
    {
        public event EventHandler? CanExecuteChanged;

        protected ReportDTO? Report;
        protected ActivityDTO? Activity;
        protected ICollection<LocationItemDTO>? Locations;

        public virtual bool CanExecute(ReportMessage? parameter)
        {
            return true;
        }

        public virtual void Execute(ReportMessage? parameter)
        {
            ProccessMessage(parameter);
        }

        private void ProccessMessage(ReportMessage? reportMessage)
        {

            if (reportMessage == null) return;

            Report = reportMessage.Report;
            Activity = reportMessage.Activity;
            Locations = reportMessage.Locations;


            if (Locations == null)
                throw new ArgumentNullException(nameof(Locations));
            if (Activity == null)
                throw new ArgumentNullException(nameof(Activity));
            if (Activity.Locations == null)
                Activity.Locations = new ObservableCollection<LocationItemDTO>();
        }
    }
}
