using iPlanner.Presentation.Services.MediatorMessages;

namespace iPlanner.Presentation.Commands.Reports
{
    public class AddLocationReportCommand : LocationsReportBase
    {
        public event EventHandler? CanExecuteChanged;

        public override bool CanExecute(ReportMessage? parameter)
        {
            return true;
        }

        public override void Execute(ReportMessage? parameter)
        {
            base.Execute(parameter);
            foreach (var location in Locations)
            {
                if (!Activity.Locations.Any(l => l.Id == location.Id) && location.Id > 0)
                {
                    Activity.Locations.Add(location);
                }
            }
        }
    }
}
