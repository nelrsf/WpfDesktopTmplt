using iPlanner.Presentation.Controls;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Services;

namespace iPlanner.Presentation.ViewModels
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
            _mediator.Notify(this, CommandType.ArrangeVertical);
        }

        public void ArrangeHorizontal_Click(object sender, EventArgs e)
        {
            _mediator.Notify(this, CommandType.ArrangeHorizontal);
        }

        public void ArrangeCascade_Click(object sender, EventArgs e)
        {
            _mediator.Notify(this, CommandType.ArrangeCascade);
        }

        public void ArrangeGrid_Click(object sender, EventArgs e)
        {
            _mediator.Notify(this, CommandType.ArrangeGrid);
        }

        internal void OpenNewViewDialog()
        {
            _mediator.Notify(this, CommandType.OpenNewViewDialog);
        }

        internal void AddHomeTab()
        {
            _mediator.Notify(this, CommandType.InsertNewView, ControlFactory.HOME_CONTROL);
        }

        internal void ToggleSideBar()
        {
            _mediator.Notify(this, CommandType.ToggleSideBar);
        }
    }

}
