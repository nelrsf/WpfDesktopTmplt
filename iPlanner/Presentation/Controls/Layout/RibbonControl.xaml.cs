using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.ViewModels.Layout;
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
            ViewModel = new RibbonViewModel(mediator);
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

        private void ReportsFilter_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}