using System.Windows.Controls;
using WpfDesktopTmplt.Presentation.Controls;

namespace WpfDesktopTmplt.Presentation.Services
{
    public class ControlFactory
    {
        public static string CALENDAR_CONTROL = "Calendar";
        public static string DASHBOARD_CONTROL = "Dashboard";
        public static string NOTIFICATIONS_CONTROL = "Notifications";
        public static string HOME_CONTROL = "Home";
        public Type GetControl(string name)
        {
            if (name == CALENDAR_CONTROL)
                return typeof(CalendarControl);
            else if (name == DASHBOARD_CONTROL)
                return typeof(DashboardControl);
            else if (name == NOTIFICATIONS_CONTROL)
                return typeof(NotificationsControl);
            else
                return typeof(WelcomeControl);
        }
    }
}
