using iPlanner.Presentation.Controls;
using iPlanner.Presentation.Controls.Teams;
using System.Windows.Controls;

namespace iPlanner.Presentation.Services
{
    public class ControlFactory
    {
        public const string CALENDAR_CONTROL = "Calendar";
        public const string DASHBOARD_CONTROL = "Dashboard";
        public const string NOTIFICATIONS_CONTROL = "Notifications";
        public const string HOME_CONTROL = "Home";
        public const string REPORT_LIST_CONTROL = "Report List";
        public const string TEAMS_FORM_CONTROL = "_teams Form";
        public const string REPORTS_FORM = "Reports Form";

        public UserControl GetControl(string name)
        {
            return name switch
            {
                CALENDAR_CONTROL => new CalendarControl(),
                DASHBOARD_CONTROL => new DashboardControl(),
                NOTIFICATIONS_CONTROL => new NotificationsControl(),
                REPORT_LIST_CONTROL => new ReportListControl(),
                TEAMS_FORM_CONTROL => new TeamFormControl(),
                REPORTS_FORM => new ReportEditorControl(),
                _ => new WelcomeControl()
            };
        }
    }
}
