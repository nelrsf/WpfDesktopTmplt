using iPlanner.Core.Application.DTO.Teams;
using iPlanner.Presentation.Commands.Teams;
using iPlanner.Presentation.Controls.Teams;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.Base;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace iPlanner.Presentation.ViewModels.Teams
{
    public class TeamFormViewModel : INotifyPropertyChanged, IFormViewModel, INotification
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

        public TaskCompletionSource<bool> TaskCompletionSource { get; set; }

        public TeamFormViewModel(FormMode mode = FormMode.Create)
        {
            _appMediatorService = AppServices.GetService<IMediator>();
            SelectedMembers = new List<TeamMemberDTO>();
            TaskCompletionSource = new TaskCompletionSource<bool>();
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
            if (Team == null) return;
            TeamMessage teamMessage = new TeamMessage(typeof(AddMemberCommand), Team);
            _appMediatorService.Notify(teamMessage);
        }

        public void SaveForm()
        {
            if (Team == null) return;
            if (_mode == FormMode.Create)
            {
                TeamMessage teamMessage = new TeamMessage(typeof(AddTeamCommand), Team);
                teamMessage.sender = this;
                CloseFormMessage closeFormMessage = new CloseFormMessage();
                closeFormMessage.sender = this;
                teamMessage.innerMessages = new List<MessageBase> { closeFormMessage };
                teamMessage.TaskCompletionSource = TaskCompletionSource;
                _appMediatorService.Notify(teamMessage);
                
            }
        }

        public void CloseForm()
        {
            CloseFormMessage closeFormMessage = new CloseFormMessage();
            closeFormMessage.sender = this;
            _appMediatorService.Notify(closeFormMessage);
        }

        public UserControl? GetUserControl()
        {
            return TeamsFormControl;
        }
    }
}