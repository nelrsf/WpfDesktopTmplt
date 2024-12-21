using iPlanner.Core.Application.DTO;
using iPlanner.Presentation.Services.MediatorMessages;

namespace iPlanner.Presentation.Commands.Reports
{
    class RemoveLocationReportCommand : LocationsReportBase
    {
        public event EventHandler? CanExecuteChanged;

        public override bool CanExecute(ReportMessage? parameter)
        {
            return true;
        }

        public override void Execute(ReportMessage? parameter)
        {
            base.Execute(parameter);
            List<LocationItemDTO> locationsToRemove = new List<LocationItemDTO>();
            foreach (LocationItemDTO location in Locations)
            {
                locationsToRemove.Add(location);
            }

            foreach (LocationItemDTO location in locationsToRemove)
            {
                Activity.Locations.Remove(location);
            }
        }
    }
}
