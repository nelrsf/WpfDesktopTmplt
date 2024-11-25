
using System.Windows;
using WpfDesktopTmplt.Core.Application.Interfaces;
using WpfDesktopTmplt.Presentation.Services;
using WpfDesktopTmplt.Presentation.ViewModels;

namespace WpfDesktopTmplt
{
    public partial class MainWindow : Window, IMainWindow
    {
        private IMediator _appMediator;
        private MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _appMediator = AppMediatorService.GetInstance();
            _appMediator.RegisterMainWindow(this);

            _viewModel = new MainWindowViewModel();
            _viewModel.mainWindow = this;
            DataContext = _viewModel;

            Loaded += (s, e) =>
            {
                _viewModel.IntializeDockingManager(dockingManager);
                _viewModel.InitializeSidePanel();
            };
        }

        private void TabButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.TabButton_Click(sender, e);
        }
    }
}