using WpfDesktopTmplt.Core.Application.Interfaces;
using WpfDesktopTmplt.Presentation.Controls;
using WpfDesktopTmplt.Presentation.Services;

namespace WpfDesktopTmplt.Presentation.ViewModels
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
            _mediator.notify(this, CommandDictionary.ArrangeVertical);
        }

        public void ArrangeHorizontal_Click(object sender, EventArgs e)
        {
            _mediator.notify(this, CommandDictionary.ArrangeHorizontal);
        }

        public void ArrangeCascade_Click(object sender, EventArgs e)
        {
            _mediator.notify(this, CommandDictionary.ArrangeCascade);
        }

        public void ArrangeGrid_Click(object sender, EventArgs e)
        {
            _mediator.notify(this, CommandDictionary.ArrangeGrid);
        }

        internal void OpenNewViewDialog()
        {
            _mediator.notify(this, CommandDictionary.OpenNewViewDialog);
        }

        internal void AddHomeTab()
        {
            _mediator.notify(this, CommandDictionary.InsertNewView, ControlFactory.HOME_CONTROL);
        }

        internal void ToggleSideBar()
        {
            _mediator.notify(this, CommandDictionary.ToggleSideBar);   
        }
    }

}
