using iPlanner.Presentation.Controls;
using iPlanner.Presentation.Controls.Teams;
using iPlanner.Presentation.Interfaces;
using System.Windows.Controls;

namespace iPlanner.Presentation.Services
{
    public enum CustomControlType
    {
        CALENDAR_CONTROL,
        DASHBOARD_CONTROL,
        NOTIFICATIONS_CONTROL,
        HOME_CONTROL,
        REPORT_LIST_CONTROL,
        TEAMS_FORM_CONTROL,
        REPORTS_FORM
    }

    public class ControlFactory : IControlAbstractFactory
    {

        private Dictionary<Type, Func<UserControl>> CustomControlDictionary;

        public ControlFactory()
        {
            InitializeCustomControls();
        }

        private void InitializeCustomControls()
        {
            CustomControlDictionary = new Dictionary<Type, Func<UserControl>>();
            CustomControlDictionary.Add(typeof(CalendarControl), () => new CalendarControl());
            CustomControlDictionary.Add(typeof(NotificationsControl), () => new NotificationsControl());
            CustomControlDictionary.Add(typeof(DashboardControl), () => new DashboardControl());
            CustomControlDictionary.Add(typeof(WelcomeControl), () => new WelcomeControl());
            CustomControlDictionary.Add(typeof(TeamFormControl), () => new TeamFormControl());
            CustomControlDictionary.Add(typeof(ReportListControl), () => new ReportListControl());
            CustomControlDictionary.Add(typeof(TeamScheduleControl), () => new TeamScheduleControl());
        }

        public UserControl CreateControl(Type type)
        {
            return CustomControlDictionary[type].Invoke();
        }

        public IFormControl<Model> CreateFormControl<Model, Control>(Model entity, bool readonlyMode) where Model : class where Control : IFormControl<Model>, new()
        {
            IFormControl<Model> control = new Control();

            if (readonlyMode)
            {
                control.ViewReport(entity);
            }
            else
            {
                control.EditReport(entity);
            }

            return control;

        }

        public IFormControl<Model> CreateFormControl<Model, Control>() where Model : class where Control : IFormControl<Model>, new()
        {
            IFormControl<Model> control = new Control();
            control.CreateNewReport();

            return control;

        }


    }
}
