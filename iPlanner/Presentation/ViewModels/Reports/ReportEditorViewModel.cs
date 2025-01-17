﻿using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.DTO.Reports;
using iPlanner.Core.Application.DTO.Teams;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Commands.Reports;
using iPlanner.Presentation.Controls;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.Base;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;


namespace iPlanner.Presentation.ViewModels.Reports
{
    public class ReportEditorViewModel : INotifyPropertyChanged, IFormViewModel
    {
        private readonly ITeamService _teamService;
        private readonly IMediator _mediator;
        private readonly IReportService _reportService;

        public ReportFilterDTO ReportFilterDTO { get; set; }
        public ReportEditorControl _reportEditorControl;
        private ReportDTO _currentReport;
        private TeamDTO _selectedTeam;
        private bool _isEditable;
        private bool _isNewReport;
        private string _headerText;
        private string _subheaderText;
        private string _saveButtonText;


        public ReportDTO CurrentReport
        {
            get => _currentReport;
            set
            {
                _currentReport = value;
                OnPropertyChanged();
            }
        }

        public TeamDTO? SelectedTeam
        {
            get => _selectedTeam;
            set
            {
                _selectedTeam = value;
                if (_currentReport != null)
                {
                    _currentReport.Team = value;
                }
                OnPropertyChanged();
            }
        }

        public bool IsNewReport
        {
            get => _isNewReport;
            set
            {
                _isNewReport = value;
                OnPropertyChanged();
            }
        }
        public bool IsEditable
        {
            get => _isEditable;
            set
            {
                _isEditable = value;
                OnPropertyChanged();
            }
        }

        public string HeaderText
        {
            get => _headerText;
            set
            {
                _headerText = value;
                OnPropertyChanged();
            }
        }

        public string SubheaderText
        {
            get => _subheaderText;
            set
            {
                _subheaderText = value;
                OnPropertyChanged();
            }
        }

        public string SaveButtonText
        {
            get => _saveButtonText;
            set
            {
                _saveButtonText = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TeamDTO> Teams { get; private set; }



        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<ReportDTO> ReportSaved;
        public event EventHandler OperationCancelled;



        public ReportEditorViewModel(ITeamService teamService, IReportService reportService, IMediator mediator)
        {
            _mediator = mediator;
            _teamService = teamService;
            _reportService = reportService;
            Teams = new ObservableCollection<TeamDTO>();
            ReportFilterDTO = new ReportFilterDTO();

            // Cargar frentes de trabajo
            LoadTeamsAsync();
        }

        public void InitializeForNewReport()
        {
            _isNewReport = true;
            IsEditable = true;
            HeaderText = "Crear Nuevo Reporte";
            SubheaderText = "Complete los detalles del nuevo reporte";
            SaveButtonText = "Guardar";

            CurrentReport = _reportService.InitializeNewReport();

        }

        public void InitializeForEdit(ReportDTO report)
        {
            if (report == null)
                throw new ArgumentNullException(nameof(report));

            _isNewReport = false;
            IsEditable = true;
            HeaderText = "Editar Reporte";
            SubheaderText = "Modifique los detalles del reporte";
            SaveButtonText = "Actualizar";

            CurrentReport = report;
            //SelectedTeam = report.Team;
        }

        public void InitializeForView(ReportDTO report)
        {
            if (report == null)
                throw new ArgumentNullException(nameof(report));

            _isNewReport = false;
            IsEditable = false;
            HeaderText = "Ver Reporte";
            SubheaderText = "Detalles del reporte";
            SaveButtonText = string.Empty;

            CurrentReport = report;
            //SelectedTeam = report.Team;
        }

        private void SelectCurrentTeam()
        {
            if (CurrentReport?.Team != null)
            {
                SelectedTeam = Teams.FirstOrDefault(t => t.Id == CurrentReport.Team.Id);
            }
        }

        private async Task LoadTeamsAsync()
        {
            try
            {
                var teams = await _teamService.GetAll();
                Teams.Clear();
                foreach (var team in teams)
                {
                    Teams.Add(team);
                }
                SelectCurrentTeam();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los equipos", ex);
            }
        }

        public void AddActivity()
        {
            if (CurrentReport.Activities == null)
            {
                CurrentReport.Activities = new ObservableCollection<ActivityDTO>();
            }

            ActivityDTO activityDTO = new ActivityDTO();
            CurrentReport.Activities.Add(activityDTO);
            var UpdatedReport = CurrentReport;
            CurrentReport = null;
            CurrentReport = UpdatedReport;
            OnPropertyChanged(nameof(CurrentReport));
        }

        public async void AddLocations(List<LocationItemDTO> locations, ActivityDTO activity)
        {
            ReportMessage reportMessage = new ReportMessage(typeof(AddLocationReportCommand));
            reportMessage.TaskCompletionSource = new TaskCompletionSource<bool>();
            reportMessage.Activity = activity;
            reportMessage.Locations = locations;
            reportMessage.Report = CurrentReport;
            _mediator.Notify(reportMessage);
            Task<bool> task = reportMessage.TaskCompletionSource.Task;
            try
            {
                bool result = await task;
                if (result)
                {
                    var updatedReport = CurrentReport;
                    CurrentReport = null;
                    CurrentReport = updatedReport;

                    OnPropertyChanged($"{nameof(CurrentReport)}.{nameof(CurrentReport.Activities)}");
                }
            } catch
            {
                MessageBox.Show("Error al agregar ubicaciones al reporte", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void DeleteLocation(ActivityDTO activity, List<LocationItemDTO> locations)
        {
            ReportMessage reportMessage = new ReportMessage(typeof(RemoveLocationReportCommand));
            reportMessage.Activity = activity;
            reportMessage.Locations = locations;
            reportMessage.Report = CurrentReport;
            reportMessage.TaskCompletionSource = new TaskCompletionSource<bool>();
            _mediator.Notify(reportMessage);
            Task<bool> task = reportMessage.TaskCompletionSource.Task;
            try
            {
                bool result = await task;
                if (result)
                {
                    var updatedReport = CurrentReport;
                    CurrentReport = null;
                    CurrentReport = updatedReport;
                    OnPropertyChanged($"{nameof(CurrentReport)}.{nameof(CurrentReport.Activities)}");
                }
            }
            catch
            {
                MessageBox.Show("Error al eliminar ubicaciones del reporte", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        internal void OnDateChanged(DateTime? displayDate)
        {
            if(CurrentReport == null)
            {
                return;
            }
            CurrentReport.Date = displayDate;
            CurrentReport = _reportService.RefreshReportDate(CurrentReport);
        }

        internal void CreateReport()
        {
            ReportMessage reportMessage = new ReportMessage(typeof(CreateReportCommand));
            reportMessage.Report = CurrentReport;
            reportMessage.sender = this;
            CloseFormMessage closeFormMessage = new CloseFormMessage();
            closeFormMessage.sender = this;
            reportMessage.innerMessages = new List<MessageBase> { closeFormMessage };
            _mediator.Notify(reportMessage);
        }

        public UserControl GetUserControl()
        {
            return _reportEditorControl;
        }

        internal void CloseForm()
        {
            CloseFormMessage closeFormMessage = new CloseFormMessage();
            closeFormMessage.sender = this;
            _mediator.Notify(closeFormMessage);
        }

        internal void EditReport()
        {
            ReportMessage reportMessage = new ReportMessage(typeof(UpdateReportCommand));
            reportMessage.Report = CurrentReport;
            reportMessage.sender = this;
            CloseFormMessage closeFormMessage = new CloseFormMessage();
            closeFormMessage.sender = this;
            reportMessage.innerMessages = new List<MessageBase> { closeFormMessage };
            _mediator.Notify(reportMessage);
        }

        internal void DelecteActivity(ActivityDTO activity)
        {
            var updateReport = CurrentReport;
            updateReport.Activities.Remove(activity);
            CurrentReport = null;
            CurrentReport = updateReport;
            OnPropertyChanged(nameof(CurrentReport));
        }
    }
}