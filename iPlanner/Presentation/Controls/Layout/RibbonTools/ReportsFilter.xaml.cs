using System.Windows.Controls;

namespace iPlanner.Presentation.Controls.Layout.RibbonTools
{
    public partial class ReportsFilter : UserControl
    {
        public ReportsFilterViewmodel ReportsFilterViewmodel;
        public ReportsFilter()
        {
            InitializeComponent();
            ReportsFilterViewmodel = AppServices.GetService<ReportsFilterViewmodel>();
            DataContext = ReportsFilterViewmodel;
        }

        private void ClearFilter(object sender, System.Windows.RoutedEventArgs e)
        {
            ReportsFilterViewmodel.ClearFilter();
        }

        private void ApplyFilter(object sender, System.Windows.RoutedEventArgs e)
        {
            ReportsFilterViewmodel.ApplyFilter();
        }
    }
}
