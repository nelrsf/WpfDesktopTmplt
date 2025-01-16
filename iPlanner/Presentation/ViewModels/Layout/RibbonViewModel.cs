using iPlanner.Core.Application.DTO.Reports;
using iPlanner.Presentation.Commands;
using iPlanner.Presentation.Commands.Window;
using iPlanner.Presentation.Controls;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;

namespace iPlanner.Presentation.ViewModels.Layout
{
    public class RibbonViewModel : ViewModelBase
    {
        IMediator _mediator;

        public ReportFilterDTO ReportFilterDTO { get; set; }
        private RibbonFilterReportTools _filterReportTools;

        public RibbonFilterReportTools FilterReportTools
        {
            get { return _filterReportTools; }
            set { _filterReportTools = value; }
        }


        public RibbonViewModel(IMediator mediator, RibbonFilterReportTools filterReportTools)
        {
            _mediator = mediator;
            _filterReportTools = filterReportTools;
            _filterReportTools.ReportFilterChanged += _filterReportTools_ReportFilterChanged;
        }

        private void _filterReportTools_ReportFilterChanged(object? sender, ReportFilterDTO e)
        {
            ReportFilterDTO = null;
            ReportFilterDTO = e;
            OnPropertyChanged(nameof(ReportFilterDTO));
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
            _mediator.Notify(new ViewMessage(typeof(InsertNewViewCommand), "Bienvenido", new WelcomeControl()));
        }

        internal void ToggleSideBar()
        {
            _mediator.Notify(new CommandMessage(typeof(ToggleSideBarCommand)));
        }
    }

}
