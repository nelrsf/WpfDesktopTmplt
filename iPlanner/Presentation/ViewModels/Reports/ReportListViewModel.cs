using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services;
using iPlanner.Presentation.Services.MediatorMessages;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace iPlanner.Presentation.ViewModels.Reports
{

    public class ReportListViewModel : ViewModelBase
    {
        private readonly IReportService _reportService;
        private ObservableCollection<ReportDTO> _reports;
        private ReportDTO? _selectedReport;
        private readonly IMediator _mediatorService;

        public ObservableCollection<ReportDTO> Reports
        {
            get => _reports;
            set
            {
                _reports = value;
                OnPropertyChanged();
            }
        }

        public ReportDTO SelectedReport
        {
            get => _selectedReport;
            set
            {
                _selectedReport = value;
                OnPropertyChanged();
            }
        }

        public ReportListViewModel(IReportService reportService, IMediator mediatorService)
        {
            _reportService = reportService;
            _reports = new ObservableCollection<ReportDTO>();
            _mediatorService = mediatorService;

        }

        private void ViewDetails(object parameter)
        {
            if (parameter is ReportDTO report)
            {
                SelectedReport = report;
                // Raise event or navigate to details
            }
        }

        public async Task LoadReportsAsync()
        {
            var reports = await _reportService.GetReportsAsync();
            Reports = new ObservableCollection<ReportDTO>(reports);
        }

        internal void OpenCreateReportForm()
        {
            string viewName = ControlFactory.REPORTS_FORM;
            ControlFactory controlFactory = new ControlFactory();
            var content = controlFactory.GetControl(viewName);
            if (content is IFormControl<ReportDTO> formControl) {
                formControl.CreateNewReport();
            }
            
            ViewMessage message = new ViewMessage(viewName, content);
            message.sender = this;
            _mediatorService.Notify(message);
            
        }
    }
}
