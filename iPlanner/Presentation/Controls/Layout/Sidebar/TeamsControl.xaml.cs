using iPlanner.Core.Application.DTO.Teams;
using iPlanner.Presentation.ViewModels.Teams;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace iPlanner.Presentation.Controls.Sidebar
{
    public partial class TeamsControl : UserControl
    {
        private TeamsViewModel _viewModel;

        public TeamsControl()
        {
            InitializeComponent();
            _viewModel = AppServices.GetService<TeamsViewModel>();
            DataContext = _viewModel;
        }

        private void OnAddTeam(object sender, RoutedEventArgs e)
        {
            _viewModel.AddTeam();
        }

        private void OnEditTeam(object sender, RoutedEventArgs e)
        {
            _viewModel.EditTeam();
        }

        private void OnDeleteTeams(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteTeams();
        }

        private void OnViewTeam(object sender, RoutedEventArgs e)
        {
            _viewModel.ViewTeamDetails();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTeamsList = ((ListView)sender).SelectedItems.Cast<TeamDTO>().ToList();
            ObservableCollection<TeamDTO> selectedTeams = [.. selectedTeamsList];


            _viewModel.UpdateSelectedTeams(selectedTeams);
        }
    }
}
