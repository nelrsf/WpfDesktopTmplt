using iPlanner.Core.Application.DTO;
using iPlanner.Presentation.ViewModels.Teams;
using System.Windows.Controls;

namespace iPlanner.Presentation.Controls.Teams
{
    public partial class TeamFormControl : UserControl
    {
        private TeamFormViewModel _viewModel;

        public TeamFormControl()
        {
            InitializeComponent();
            _viewModel = new TeamFormViewModel();
            _viewModel.TeamsFormControl = this;
            this.DataContext = _viewModel;
        }

        private void AddMember_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.AddMember();
        }

        private void RowCheckBox_Unchecked(object sender, System.Windows.RoutedEventArgs e) {
            if (!(sender is CheckBox)) return;

            CheckBox checkBox = (CheckBox)sender;

            if (checkBox.IsChecked == false)
            {
                _viewModel.RemoveSelectedMember((TeamMemberDTO)checkBox.DataContext);
            }
        }

        private void RowCheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!(sender is CheckBox)) return;
            
            CheckBox checkBox = (CheckBox)sender;
            
            if(checkBox.IsChecked == true)
            {
                _viewModel.AddSelectedMember((TeamMemberDTO)checkBox.DataContext);
            }
        }

        private void SaveForm(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.SaveForm();
        }

        private void CloseForm(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.CloseForm();
        }
    }
}