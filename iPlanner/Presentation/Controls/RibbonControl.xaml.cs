using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace iPlanner.Presentation.Controls
{
    public partial class RibbonControl : UserControl
    {

        public RibbonControl()
        {
            InitializeComponent();
            IMediator mediator = AppServices.GetService<IMediator>();
            DataContext = new RibbonViewModel(mediator);
        }

        private void ArrangeVertical_Click(object sender, RoutedEventArgs e)
        {
            ((RibbonViewModel)DataContext).ArrangeVertical_Click(sender, e);
        }

        private void ArrangeHorizontal_Click(object sender, RoutedEventArgs e)
        {
            ((RibbonViewModel)DataContext).ArrangeHorizontal_Click(sender, e);
        }

        private void ArrangeCascade_Click(object sender, RoutedEventArgs e)
        {
            ((RibbonViewModel)DataContext).ArrangeCascade_Click(sender, e);
        }

        private void ArrangeGrid_Click(object sender, RoutedEventArgs e)
        {
            ((RibbonViewModel)DataContext).ArrangeGrid_Click(sender, e);
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            ((RibbonViewModel)DataContext).OpenNewViewDialog();
        }

        private void HomeButton_Click(Object sender, RoutedEventArgs e)
        {
            ((RibbonViewModel)DataContext).AddHomeTab();
        }

        private void ToggleSideBar(object sender, RoutedEventArgs e)
        {
            ((RibbonViewModel)DataContext).ToggleSideBar();
        }
    }
}