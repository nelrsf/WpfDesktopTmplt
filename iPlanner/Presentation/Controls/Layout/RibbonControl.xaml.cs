using AvalonDock;
using iPlanner.Core.Application.DTO.Reports;
using iPlanner.Core.Application.DTO.Teams;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.ViewModels.Layout;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace iPlanner.Presentation.Controls
{
    public partial class RibbonControl : UserControl
    {
        public RibbonViewModel ViewModel { get; set; }

        public RibbonControl()
        {
            InitializeComponent();
            IMediator mediator = AppServices.GetService<IMediator>();
            RibbonFilterReportTools filterReportTools = new RibbonFilterReportTools(this);
            ViewModel = new RibbonViewModel(mediator, filterReportTools);
            DataContext = ViewModel;
        }


        private void ArrangeVertical_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ArrangeVertical_Click(sender, e);
        }

        private void ArrangeHorizontal_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ArrangeHorizontal_Click(sender, e);
        }

        private void ArrangeCascade_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ArrangeCascade_Click(sender, e);
        }

        private void ArrangeGrid_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ArrangeGrid_Click(sender, e);
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.OpenNewViewDialog();
        }

        private void HomeButton_Click(Object sender, RoutedEventArgs e)
        {
            ViewModel.AddHomeTab();
        }

        private void ToggleSideBar(object sender, RoutedEventArgs e)
        {
            ViewModel.ToggleSideBar();
        }
    }

    public class RibbonFilterReportTools : INotifyPropertyChanged
    {
        private ReportListControl? control;
        private ReportFilterDTO? _reportFilter;
        private List<TeamDTO>? _availableTeams;
        private MainWindow? MainWindow;
        public event EventHandler<ReportFilterDTO> ReportFilterChanged;
        public event PropertyChangedEventHandler? PropertyChanged;


        public ReportFilterDTO? ReportFilterDTO
        {
            get { return _reportFilter; }
            set { 
                _reportFilter = value; 
                OnPropertyChanged(nameof(ReportFilterDTO));
            }
        }

        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(v, new PropertyChangedEventArgs(v));
        }

        public RibbonFilterReportTools(UserControl control)
        {
            control.Loaded += Control_Loaded;
        }

        private void Control_Loaded(object sender, RoutedEventArgs e)
        {
            Initialize();
        }

        private void Initialize()
        {
            IMainWindow mainWindow = AppServices.GetService<IMainWindow>();
            if (mainWindow == null) throw new Exception("Error al inicializar la barra de filtros de reportes");
            MainWindow = mainWindow as MainWindow;
            DockingManager? dockingManager = MainWindow?.dockingManager;
            if (dockingManager == null) throw new Exception("Error al inicializar la barra de filtros de reportes");
            dockingManager.ActiveContentChanged += DockingManager_ActiveContentChanged;

        }

        private void DockingManager_ActiveContentChanged(object? sender, EventArgs e)
        {
            control = null;
            if (sender is not DockingManager dockingManager) return;
            if (dockingManager == null) return;
            if (dockingManager.DataContext is not MainWindowViewModel mainWindowViewModel) return;

            var documents = mainWindowViewModel.Documents;

            var reportsListControl = documents.Where(
                d =>
                {
                    return d.Content is ReportListControl && d.IsActive == true;
                }).FirstOrDefault();

            if (reportsListControl == null) return;

            control = reportsListControl.Content as ReportListControl;
            var reportsFilter = control?.ReportListViewModel.ReportsFilter;
            if (reportsFilter != null)
            {
                ReportFilterDTO = reportsFilter;
                ReportFilterChanged?.Invoke(this, ReportFilterDTO);
            }

        }

        public void ApplyFilter(ReportFilterDTO filter)
        {
            if (filter == null) return;
            if (control == null) return;
            var vm = control.ReportListViewModel;
            vm.ReportsFilter = filter;
        }
    }
}