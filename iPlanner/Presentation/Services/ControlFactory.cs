using iPlanner.Presentation.Controls;
using iPlanner.Presentation.Controls.Teams;

namespace iPlanner.Presentation.Services
{
    public class ControlFactory
    {
        public static string CALENDAR_CONTROL = "Calendar";
        public static string DASHBOARD_CONTROL = "Dashboard";
        public static string NOTIFICATIONS_CONTROL = "Notifications";
        public static string HOME_CONTROL = "Home";
        public static string REPORT_LIST_CONTROL = "Report List";
        public static string TEAMS_FORM_CONTROL = "Teams Form";
        public Type GetControl(string name)
        {
            if (name == CALENDAR_CONTROL)
                return typeof(CalendarControl);
            else if (name == DASHBOARD_CONTROL)
                return typeof(DashboardControl);
            else if (name == NOTIFICATIONS_CONTROL)
                return typeof(NotificationsControl);
            else if (name == REPORT_LIST_CONTROL)
                return typeof(ReportListControl);
            else if (name == TEAMS_FORM_CONTROL)
                return typeof(TeamFormControl);
            else
                return typeof(WelcomeControl);
        }
    }
}
