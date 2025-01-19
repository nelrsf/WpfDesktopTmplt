using iPlanner.Core.Application.DTO.Reports;
using iPlanner.Core.Application.DTO.Teams;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.ViewModels;
using iPlanner.Presentation.ViewModels.Reports;
using System.Collections.ObjectModel;

namespace iPlanner.Presentation.Controls.Layout.RibbonTools
{
    public class ReportsFilterViewmodel : ViewModelBase
    {
        private ITeamService _teamService;
        private ReportListViewModel? _reportListViewModel;
        public ReportListViewModel? ReportListViewModel
        {
            get
            {
                return _reportListViewModel;
            }
            set
            {
                if (value == null) return;
                _reportListViewModel = value;
                ReportFilterDTO = _reportListViewModel.ReportsFilter;
            }
        }

        private ReportFilterDTO? _reportFiterDTO;
        public ReportFilterDTO? ReportFilterDTO
        {
            get
            {
                return _reportFiterDTO;
            }
            set
            {
                _reportFiterDTO = value;
                OnPropertyChanged();
            }
        }

        private bool _canExecute;
        public bool CanExecute
        {
            get
            {
                return _canExecute;
            }
            set
            {
                _canExecute = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TeamDTO> AvailableTeams { get; set; }


        public ReportsFilterViewmodel(ITeamService teamService)
        {
            _teamService = teamService;
            AvailableTeams = new ObservableCollection<TeamDTO>();
            LoadTems();
        }

        private async void LoadTems()
        {
            ICollection<TeamDTO> teams = await _teamService.GetAll();
            foreach (TeamDTO team in teams)
            {
                AvailableTeams.Add(team);
            }
        }

        internal void ClearFilter()
        {
            if (_reportListViewModel == null) return;
            _reportListViewModel.ReportFilterManager.ClearFilter();
            ReportFilterDTO = _reportListViewModel.ReportsFilter;
        }

        internal async void ApplyFilter()
        {
            if (_reportListViewModel == null) return;
            await _reportListViewModel.ApplyFilter();
        }
    }
}