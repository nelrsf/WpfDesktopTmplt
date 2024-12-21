using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Commands;
using iPlanner.Presentation.Commands.Window;
using iPlanner.Presentation.Controls;
using iPlanner.Presentation.Services.MediatorMessages;

namespace iPlanner.Presentation.ViewModels.Layout
{
    public class RibbonViewModel
    {
        IMediator _mediator;

        public RibbonViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }


        public void ArrangeVertical_Click(object sender, EventArgs e)
        {
            _mediator.Notify(new CommandMessage(typeof(ArrangeVerticalCommand)));
        }

        public void ArrangeHorizontal_Click(object sender, EventArgs e)
        {
            _mediator.Notify(new CommandMessage(typeof(ArrangeHorizontalCommand)));
        }

        public void ArrangeCascade_Click(object sender, EventArgs e)
        {
            _mediator.Notify(new CommandMessage(typeof(ArrangeCascadeCommand)));
        }

        public void ArrangeGrid_Click(object sender, EventArgs e)
        {
            _mediator.Notify(new CommandMessage(typeof(ArrangeGridCommand)));
        }

        internal void OpenNewViewDialog()
        {
            _mediator.Notify(new CommandMessage(typeof(OpenNewDialog)));
        }

        internal void AddHomeTab()
        {
            _mediator.Notify(new ViewMessage("Bienvenido", new WelcomeControl()));
        }

        internal void ToggleSideBar()
        {
            _mediator.Notify(new CommandMessage(typeof(ToggleSideBarCommand)));
        }
    }

}
