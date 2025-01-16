using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.ViewModels.Layout;
using System.Windows;

namespace iPlanner
{
    public partial class MainWindow : Window, IMainWindow
    {
        private IMediator _appMediator;
        private MainWindowViewModel _viewModel;

        public MainWindowViewModel ViewModel { get { return _viewModel; } }

        public MainWindow()
        {
            InitializeComponent();
            _appMediator = AppServices.GetService<IMediator>();
            _appMediator.RegisterMainWindow(this);

            _viewModel = new MainWindowViewModel(this);
            DataContext = _viewModel;

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