﻿namespace iPlanner.Presentation.Commands.Reports
{
    public class AddLocationReportCommand : LocationsReportBase
    {
        public event EventHandler? CanExecuteChanged;

        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute()
        {
            base.Execute();
            foreach (var location in Locations)
            {
                if (!Activity.Locations.Any(l => l.Id == location.Id) && location.Id > 0)
                {
                    Activity.Locations.Add(location);
                }
            }
            message.TaskCompletionSource?.SetResult(true);
        }
    }
}
