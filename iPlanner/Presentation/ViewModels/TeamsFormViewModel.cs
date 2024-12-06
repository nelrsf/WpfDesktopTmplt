﻿using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Controls.Teams;
using iPlanner.Presentation.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace iPlanner.Presentation.ViewModels
{
    public class TeamFormViewModel : INotifyPropertyChanged
    {
        private IMediator _appMediatorService;
        private TeamDTO? _team;
        private FormMode? _mode;
        private string? _formTitle;
        private string? _formDescription;
        private string? _saveButtonText;
        private List<TeamMemberDTO> SelectedMembers;

        public event PropertyChangedEventHandler? PropertyChanged;
        public TeamFormControl? TeamsFormControl;

        public enum FormMode
        {
            Create,
            Edit,
            View
        }

        public TeamDTO? Team
        {
            get => _team;
            set
            {
                _team = value;
                OnPropertyChanged(nameof(Team));
            }
        }

        public string? FormTitle
        {
            get => _formTitle;
            set
            {
                _formTitle = value;
                OnPropertyChanged(nameof(FormTitle));
            }
        }

        public string? FormDescription
        {
            get => _formDescription;
            set
            {
                _formDescription = value;
                OnPropertyChanged(nameof(FormDescription));
            }
        }

        public string? SaveButtonText
        {
            get => _saveButtonText;
            set
            {
                _saveButtonText = value;
                OnPropertyChanged(nameof(SaveButtonText));
            }
        }

        public TeamFormViewModel(FormMode mode = FormMode.Create)
        {
            _appMediatorService = AppServices.GetService<IMediator>();
            SelectedMembers = new List<TeamMemberDTO>();
            SetMode(mode);
            InitializeTeam();
        }

        public void AddSelectedMember(TeamMemberDTO member)
        {
            if (!SelectedMembers.Contains(member))
            {
                SelectedMembers.Add(member);
            }
        }

        public void RemoveSelectedMember(TeamMemberDTO member)
        {
            if (SelectedMembers.Contains(member))
            {
                SelectedMembers.Remove(member);
            }
        }


        private void SetMode(FormMode mode)
        {
            _mode = mode;
            switch (mode)
            {
                case FormMode.Create:
                    FormTitle = "Crear Nuevo Equipo";
                    FormDescription = "Complete los detalles para crear un nuevo equipo";
                    SaveButtonText = "Crear Equipo";
                    break;
                case FormMode.Edit:
                    FormTitle = "Editar Equipo";
                    FormDescription = "Modifique los detalles del equipo";
                    SaveButtonText = "Guardar Cambios";
                    break;
                case FormMode.View:
                    FormTitle = "Detalles del Equipo";
                    FormDescription = "Información detallada del equipo";
                    SaveButtonText = "Cerrar";
                    break;
            }
        }

        private void InitializeTeam()
        {
            Team = new TeamDTO("", "");
            Team.Leader = new TeamMemberDTO("", "");
            Team.Members = new ObservableCollection<TeamMemberDTO>();
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void AddMember()
        {
            if(Team==null) return;
            _appMediatorService.Notify(this, CommandType.AddTeamMember, Team);
        }

        public void SaveForm()
        {
            if (Team==null) return;
            if (_mode == TeamFormViewModel.FormMode.Create)
            {
                _appMediatorService.Notify(this, CommandType.SaveTeam, Team);
            }
        }

        public void CloseForm()
        {
            _appMediatorService.Notify(this, CommandType.CloseTeamsForm);
        }
    }
}