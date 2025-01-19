using iPlanner.Presentation;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.ViewModels.Layout;
using System.Windows;

namespace iPlanner
{
    public partial class MainWindow : Window
    {
        private IMediator _appMediator;
        private MainWindowViewModel _viewModel;

        public MainWindowViewModel ViewModel { get { return _viewModel; } }

        public MainWindow()
        {
            MainWindowProvider.SetMainWindow(this);

            InitializeComponent();

            _appMediator = AppServices.GetService<IMediator>();

            _viewModel = new MainWindowViewModel(this);
            DataContext = _viewModel;

            DockingManagerEventsHandler dockingManagerEventsHandler = AppServices.GetService<DockingManagerEventsHandler>();
            dockingManagerEventsHandler.RegisterDockingManager();
            dockingManagerEventsHandler.RegisterSubscriber(_viewModel.OnDockingManagerContentChange);
            dockingManagerEventsHandler.OnDockingManagerActiveContentNotFound += _viewModel.OnDockingManagerActiveContentNotFound;

            Loaded += (s, e) =>
            {
                _viewModel.IntializeDockingManager();
                _viewModel.InitializeSidePanel();
            };
        }

        private void TabButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.TabButton_Click(sender, e);
        }
    }
}