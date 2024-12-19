using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Services.MediatorMessages;
using System.Collections.ObjectModel;

namespace iPlanner.Presentation.Commands.Reports
{
    public class AddLocationReportCommand : LocationsReportBase
    {
        public event EventHandler? CanExecuteChanged;

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
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
