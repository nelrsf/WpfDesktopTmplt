using iPlanner.Presentation.Controls;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;
using System.Collections.ObjectModel;

namespace iPlanner.Presentation.Dialogs
{
    public class ViewOption
    {
        public string? IconPath { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public Type? Type { get; set; }
    }

    public class NewViewDialogViewModel
    {
        public ObservableCollection<ViewOption> ViewOptions { get; set; }
        private IMediator mediator;
        private ControlFactory controlFactory;

        public NewViewDialogViewModel()
        {
            mediator = AppServices.GetService<IMediator>();
            controlFactory = new ControlFactory();
            ViewOptions = new ObservableCollection<ViewOption>
        {
            new ViewOption
            {
                IconPath = "M19,3H5C3.89,3 3,3.89 3,5V19A2,2 0 0,0 5,21H19A2,2 0 0,0 21,19V5C21,3.89 20.1,3 19,3M19,19H5V5H19V19Z",
                Title = "Control de tiempos",
                Description = "Vista general de tiempos de todos los frentes de trabajo",
                Type = typeof(TeamScheduleControl)
            },
            new ViewOption
            {
                IconPath = "M19,3H5C3.89,3 3,3.89 3,5V19A2,2 0 0,0 5,21H19A2,2 0 0,0 21,19V5C21,3.89 20.1,3 19,3M19,19H5V5H19V19Z",
                Title = "Listado de reportes",
                Description = "Vista general de reportes de trabajo",
                Type = typeof(ReportListControl)
            }
        };
        }

        public void CreateNewView(ViewOption? viewOption)
        {
            if (viewOption == null) return;
            Type? type = viewOption.Type;
            if (type == null) return;
            var content = controlFactory.CreateControl(type);
            if (content == null) return;
            var message = new ViewMessage(type, viewOption.Title ?? "", content);
            mediator.Notify(message);
        }
    }

}
