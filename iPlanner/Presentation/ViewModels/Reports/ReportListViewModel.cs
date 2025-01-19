using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.DTO.Reports;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Commands;
using iPlanner.Presentation.Commands.Reports;
using iPlanner.Presentation.Controls;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace iPlanner.Presentation.ViewModels.Reports
{

    public class ReportListViewModel : ViewModelBase
    {
        public ReportFilterDTO ReportsFilter { get; set; }
        public readonly ReportFilterManager ReportFilterManager;

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

        public ReportListViewModel(
            IReportService reportService,
            IMediator mediatorService,
            IControlAbstractFactory abstractFactory)
        {
            _reportService = reportService;
            _reports = new ObservableCollection<ReportDTO>();
            _mediatorService = mediatorService;
            _controlAbstractFactory = abstractFactory;
            ReportFilterManager = new ReportFilterManager(this);
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

        public async Task ApplyFilter() {
            try
            {
                var reports = await _reportService.GetReportsAsyncByFilter(ReportsFilter);
                Reports = new ObservableCollection<ReportDTO>(reports);
            }
            catch (Exception ex) { 
                MessageBox.Show(ex.Message);
            }

        }

        public async Task LoadReportsAsync()
        {
            if(ReportsFilter != null)
            {
                await ApplyFilter();
                return;
            }
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

    public class ReportFilterManager
    {
        private ReportListViewModel _reportListViewModel;

        public ReportFilterManager(ReportListViewModel reportListViewModel)
        {
            _reportListViewModel = reportListViewModel;

        }

        public void InitializeFilters()
        {
            if (_reportListViewModel.ReportsFilter == null)
            {
                _reportListViewModel.ReportsFilter = new ReportFilterDTO();
                ResetDates();
            }
        }

        public void ClearFilter()
        {
            _reportListViewModel.ReportsFilter.DateInit = null;
            _reportListViewModel.ReportsFilter.DateEnd = null;
            _reportListViewModel.ReportsFilter.Team = null;
        }

        private void ResetDates()
        {
            _reportListViewModel.ReportsFilter.DateEnd = DateTime.Now;
            _reportListViewModel.ReportsFilter.DateInit = DateTime.Now.AddDays(-7);
        }

        public static void SyncReportsFilterTools(UserControl control)
        {
            IMediator mediator = AppServices.GetService<IMediator>();
            mediator.Notify(new SyncReportFilterMessage(control, typeof(SyncReportFilterCommand)));
        }
    }
}
