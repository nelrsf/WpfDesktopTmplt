using System.Windows;
using System.Windows.Controls;
using WpfDesktopTmplt.Core.Application.Interfaces;
using WpfDesktopTmplt.Infrastructure.DependencyInjection;
using WpfDesktopTmplt.Presentation.Services;
using WpfDesktopTmplt.Presentation.ViewModels;

namespace WpfDesktopTmplt.Presentation.Controls
{
    public partial class RibbonControl : UserControl
    {
        
        public RibbonControl()
        {
            InitializeComponent();
            IMediator mediator = AppMediatorService.GetInstance();
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