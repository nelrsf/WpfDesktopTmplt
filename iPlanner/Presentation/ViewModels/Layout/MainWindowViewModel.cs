using AvalonDock.Layout;
using iPlanner.Presentation.Commands;
using iPlanner.Presentation.Controls;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

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

        public MainWindowViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            _mediator = AppServices.GetService<IMediator>();
            _controlAbstractFactory = AppServices.GetService<IControlAbstractFactory>();
            Documents = new ObservableCollection<LayoutDocument>();
            InitializeViews();
            _dockingManagerUpdater = new DockingManagerUpdater(this.mainWindow);
            _sidePanelManager = new SidePanelManager(this.mainWindow);
        }

        public void IntializeDockingManager()
        {
            _dockingManagerUpdater.UpdateDockingManager();
        }

        public void InitializeSidePanel()
        {
            _sidePanelManager.InitializeSidePanel();
        }

        private void InitializeViews()
        {
            LayoutDocument? home = new LayoutDocument
            {
                Title = "Bienvenido",
                Content = _controlAbstractFactory.CreateControl(typeof(WelcomeControl)),
                CanClose = true,
            };
            home.Closed += DeleteDocument;
            Documents.Add(home);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DeleteDocument(object? sender, EventArgs e)
        {
            if (sender == null) return;
            Documents.Remove((LayoutDocument)sender);
        }

        public void AddView(object content, string viewName)
        {
            LayoutDocument? newDocument = new LayoutDocument
            {
                CanClose = true,
                Title = viewName,
                Content = content
            };
            newDocument.Closed += DeleteDocument;
            Documents.Add(newDocument);
            _dockingManagerUpdater.UpdateDockingManager();
            _mediator?.Notify(new TabMessage(typeof(SelectTabCommand), newDocument));
        }

        public void TabButton_Click(object sender, RoutedEventArgs e)
        {
            _sidePanelManager.TabButton_Click(sender, e);
        }

        public void UpdateDockingManager()
        {
            _dockingManagerUpdater.UpdateDockingManager();
        }
    }
}
