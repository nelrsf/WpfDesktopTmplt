using iPlanner.Presentation.Controls;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Services;
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
            _mediator.Notify(new CommandMessage(CommandType.ArrangeVertical));
        }

        public void ArrangeHorizontal_Click(object sender, EventArgs e)
        {
            _mediator.Notify(new CommandMessage(CommandType.ArrangeHorizontal));
        }

        public void ArrangeCascade_Click(object sender, EventArgs e)
        {
            _mediator.Notify(new CommandMessage(CommandType.ArrangeCascade));
        }

        public void ArrangeGrid_Click(object sender, EventArgs e)
        {
            _mediator.Notify(new CommandMessage(CommandType.ArrangeGrid));
        }

        internal void OpenNewViewDialog()
        {
            _mediator.Notify(new CommandMessage(CommandType.OpenNewViewDialog));
        }

        internal void AddHomeTab()
        {
            _mediator.Notify(new ViewMessage(ControlFactory.HOME_CONTROL, new WelcomeControl()));
        }

        internal void ToggleSideBar()
        {
            _mediator.Notify(new CommandMessage(CommandType.ToggleSideBar));
        }
    }

}
