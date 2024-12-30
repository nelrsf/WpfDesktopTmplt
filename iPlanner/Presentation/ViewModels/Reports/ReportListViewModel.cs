using iPlanner.Core.Application.AppMediator;
using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Commands;
using iPlanner.Presentation.Commands.Reports;
using iPlanner.Presentation.Controls;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.MediatorMessages;
using System.Collections.ObjectModel;

namespace iPlanner.Presentation.ViewModels.Reports
{

    public class ReportListViewModel : ViewModelBase
    {
        private readonly IReportService _reportService;
        private ObservableCollection<ReportDTO> _reports;
        private ReportDTO? _selectedReport;
        private readonly IMediator _mediatorService;
        private readonly IControlAbstractFactory _controlAbstractFactory;

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

        public ReportListViewModel(IReportService reportService, IMediator mediatorService, IControlAbstractFactory abstractFactory)
        {
            _reportService = reportService;
            _reports = new ObservableCollection<ReportDTO>();
            _mediatorService = mediatorService;
            _controlAbstractFactory = abstractFactory;

        }

        public void ViewDetails(ReportDTO report)
        {
            string viewName = "Detalles del reporte";
            IFormControl<ReportDTO> content = _controlAbstractFactory.CreateFormControl<ReportDTO, ReportEditorControl>();
            content.ViewReport(report);
            ViewMessage message = new ViewMessage(typeof(InsertNewViewCommand), viewName, content);
            message.sender = this;
            _mediatorService.Notify(message);
        }

        public void EditReport(ReportDTO report)
        {
            string viewName = "Editar reporte";
            IFormControl<ReportDTO> content = _controlAbstractFactory.CreateFormControl<ReportDTO, ReportEditorControl>();
            content.EditReport(report);
            ViewMessage message = new ViewMessage(typeof(InsertNewViewCommand), viewName, content);
            message.sender = this;
            _mediatorService.Notify(message);
        }



        public async Task LoadReportsAsync()
        {
            var reports = await _reportService.GetReportsAsync();
            Reports = new ObservableCollection<ReportDTO>(reports);
        }

        internal void OpenCreateReportForm()
        {
            string viewName = "Crear reporte";
            IFormControl<ReportDTO> content = _controlAbstractFactory.CreateFormControl<ReportDTO, ReportEditorControl>();
            content.CreateNewReport();
            ViewMessage message = new ViewMessage(typeof(InsertNewViewCommand), viewName, content);
            message.sender = this;
            _mediatorService.Notify(message);

        }

        internal void DeleteReport(ReportDTO report)
        {
            ReportMessage reportMessage = new ReportMessage(typeof(DeleteReportCommand));
            reportMessage.Report = report;
            reportMessage.sender = this;
            _mediatorService.Notify(reportMessage);
            
        }
    }
}
