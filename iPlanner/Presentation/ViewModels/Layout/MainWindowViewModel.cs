using AvalonDock.Layout;
using iPlanner.Core.Application.DTO.Reports;
using iPlanner.Presentation.Commands.Reports;
using iPlanner.Presentation.Controls;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;
using iPlanner.Presentation.ViewModels.Reports;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace iPlanner.Presentation.ViewModels.Layout
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindow mainWindow;
        private IMediator? _mediator;
        private IControlAbstractFactory _controlAbstractFactory;
        public ObservableCollection<LayoutDocument> Documents { get; private set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        private SidePanelManager _sidePanelManager;
        private DockingManagerUpdater _dockingManagerUpdater;
        private DocumentManager _documentManager;

        public MainWindowViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            _mediator = AppServices.GetService<IMediator>();
            _controlAbstractFactory = AppServices.GetService<IControlAbstractFactory>();
            _dockingManagerUpdater = AppServices.GetService<DockingManagerUpdater>();
            _sidePanelManager = AppServices.GetService<SidePanelManager>();
            _documentManager = new DocumentManager(_mediator, _controlAbstractFactory);
            Documents = _documentManager.Documents;
        }

        public void IntializeDockingManager()
        {
            _dockingManagerUpdater.UpdateDockingManager();
        }

        public void InitializeSidePanel()
        {
            _sidePanelManager.InitializeSidePanel();
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddView(object content, string viewName)
        {
            _documentManager.AddView(content, viewName);
            _dockingManagerUpdater.UpdateDockingManager();
        }

        public void TabButton_Click(object sender, RoutedEventArgs e)
        {
            _sidePanelManager.TabButton_Click(sender, e);
        }

        public void UpdateDockingManager()
        {
            _dockingManagerUpdater.UpdateDockingManager();
        }

        public void OnDockingManagerContentChange(UserControl control)
        {
            ReportFilterManager.SyncReportsFilterTools(control);
        }

        internal void OnDockingManagerActiveContentNotFound(object? sender, EventArgs e)
        {
            ReportFilterManager.SyncReportsFilterTools(null);
        }
    }
}
