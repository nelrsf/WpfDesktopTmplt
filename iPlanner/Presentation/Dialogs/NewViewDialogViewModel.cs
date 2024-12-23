﻿using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Controls;
using iPlanner.Presentation.Services;
using iPlanner.Presentation.Services.MediatorMessages;
using System.Collections.ObjectModel;

namespace iPlanner.Presentation.Dialogs
{
    public class ViewOption
    {
        public string? IconPath { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public string? Name { get; set; }

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
                Title = "Dashboard",
                Description = "Vista general con KPIs y métricas",
                Type = typeof(DashboardControl)
            },
            new ViewOption
            {
                IconPath = "M19,19H5V8H19M16,1V3H8V1H6V3H5C3.89,3 3,3.89 3,5V19A2,2 0 0,0 5,21H19A2,2 0 0,0 21,19V5C21,3.89 20.1,3 19,3H18V1",
                Title = "Calendario",
                Description = "Gestión de eventos y reuniones",
                Type = typeof(CalendarControl)
            },
            new ViewOption
            {
                IconPath = "M21,19V20H3V19L5,17V11C5,7.9 7.03,5.17 10,4.29C10,4.19 10,4.1 10,4A2,2 0 0,1 12,2A2,2 0 0,1 14,4C14,4.1 14,4.19 14,4.29C16.97,5.17 19,7.9 19,11V17L21,19M14,21A2,2 0 0,1 12,23A2,2 0 0,1 10,21",
                Title = "Notificaciones",
                Description = "Centro de alertas y avisos",
                Type = typeof(NotificationsControl)
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
            var message = new ViewMessage(viewOption.Name ?? "", content);
            mediator.Notify(message);
        }
    }

}
