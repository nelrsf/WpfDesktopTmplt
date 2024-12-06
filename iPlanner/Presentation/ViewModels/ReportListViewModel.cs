using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Entities.Reports;
using System.Collections.ObjectModel;

namespace iPlanner.Presentation.ViewModels
{

    public class ReportListViewModel : ViewModelBase
    {
        private readonly IReportService _reportService;
        private ObservableCollection<Report> _reports;
        private Report? _selectedReport;

        public ObservableCollection<Report> Reports
        {
            get => _reports;
            set
            {
                _reports = value;
                OnPropertyChanged();
            }
        }

        public Report SelectedReport
        {
            get => _selectedReport;
            set
            {
                _selectedReport = value;
                OnPropertyChanged();
            }
        }

        public ReportListViewModel(IReportService reportService)
        {
            _reportService = reportService;
            _reports = new ObservableCollection<Report>();

            /*               ViewDetailsCommand = new RelayCommand(ViewDetails);
                           RefreshCommand = new RelayCommand(async () => await LoadReportsAsync());*/
        }

        private void ViewDetails(object parameter)
        {
            if (parameter is Report report)
            {
                SelectedReport = report;
                // Raise event or navigate to details
            }
        }

        public async Task LoadReportsAsync()
        {
            var reports = await _reportService.GetReportsAsync();
            Reports = new ObservableCollection<Report>(reports);
        }
    }
}
