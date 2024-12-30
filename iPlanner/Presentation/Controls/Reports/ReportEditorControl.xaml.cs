using iPlanner.Core.Application.AppMediator;
using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services;
using iPlanner.Presentation.ViewModels.Reports;
using System.Windows;
using System.Windows.Controls;

namespace iPlanner.Presentation.Controls
{
    public partial class ReportEditorControl : UserControl, IFormControl<ReportDTO>
    {
        private ReportEditorViewModel _viewModel;
        private DragNDropManager<List<LocationItemDTO>> dragNDropManager;

        public ReportEditorViewModel ViewModel
        {
            get => _viewModel;
            private set
            {
                _viewModel = value;
                DataContext = _viewModel;
            }
        }

        public ReportEditorControl()
        {
            InitializeComponent();

            dragNDropManager = new DragNDropManager<List<LocationItemDTO>>();

            dragNDropManager.OnItemDropped += HandleLocationDropped;

            // Get services from DI container
            var reportService = AppServices.GetService<IReportService>();
            var teamService = AppServices.GetService<ITeamService>();
            var mediator = AppServices.GetService<IMediator>();

            // Initialize ViewModel
            ViewModel = new ReportEditorViewModel(teamService, reportService, mediator);
            ViewModel._reportEditorControl = this;

        }

        private void HandleLocationDropped(DropEventArgs<List<LocationItemDTO>> dropData)
        {
            if (dropData.Data is List<LocationItemDTO> locations)
            {
                if (dropData.Sender is Grid grid)
                {
                    if (grid.DataContext is ActivityDTO currentActivity)
                    {
                        _viewModel.AddLocations(locations, currentActivity);
                    }
                }
            }
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            dragNDropManager.HandleDragEnter(sender, e);
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            dragNDropManager.HandleDragOver(sender, e);
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            if (!ViewModel.IsEditable) return;
            dragNDropManager.HandleDrop(sender, e);
        }


        public void ViewReport(ReportDTO report)
        {
            ViewModel.InitializeForView(report);
        }


        public void EditReport(ReportDTO report)
        {
            ViewModel.InitializeForEdit(report);
        }


        public void CreateNewReport()
        {
            ViewModel.InitializeForNewReport();
        }


        public void OnReportSaved(EventHandler<ReportDTO> handler)
        {
            ViewModel.ReportSaved += handler;
        }


        public void OnOperationCancelled(EventHandler handler)
        {
            ViewModel.OperationCancelled += handler;
        }


        public void OnAddActivity(object sender, EventArgs e)
        {
            ViewModel.AddActivity();
        }

        private void OnDeleteLocation(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var location = (LocationItemDTO)button.DataContext;
            var activity = (ActivityDTO)button.Tag;
            List<LocationItemDTO> locations = new List<LocationItemDTO>();
            locations.Add(location);
            ViewModel.DeleteLocation(activity, locations);
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var datePicker = (DatePicker)sender;
            DateTime? date = datePicker.SelectedDate;
            ViewModel.OnDateChanged(date);
        }

        private void OnSaveReport(object sender, EventArgs e)
        {
            if (ViewModel.IsNewReport)
            {
                ViewModel.CreateReport();
            }
            else
            {
                ViewModel.EditReport();
            }
        }

        private void OnCancelReport(object sender, EventArgs e)
        {
            ViewModel.CloseForm();
        }
    }
}