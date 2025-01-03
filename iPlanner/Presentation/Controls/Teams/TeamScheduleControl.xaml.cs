using iPlanner.Core.Application.Services;
using iPlanner.Presentation.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace iPlanner.Presentation.Controls
{
    public partial class TeamScheduleControl : UserControl
    {
        private TeamScheduleViewModel _viewModel;
        public TeamScheduleControl()
        {
            InitializeComponent();
            ITeamScheduleService teamScheduleService = AppServices.GetService<ITeamScheduleService>();
            _viewModel = new TeamScheduleViewModel(teamScheduleService);
            DataContext = _viewModel;
            Loaded += TeamScheduleControl_Loaded;

        }

        private void TeamScheduleControl_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.Initialize();
        }

        private void YearSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Event handling is done through bindings
        }

        private void WeekSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Event handling is done through bindings
        }
    }
}