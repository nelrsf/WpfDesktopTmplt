using iPlanner.Core.Application.DTO.Teams;
using iPlanner.Core.Entities.Locations;


namespace iPlanner.Core.Application.DTO
{
    public class ReportDTO
    {
        private string _reportId;
        private TeamDTO _team;
        private DateTime? _date;
        private TimeSpan? _timeInit;
        private TimeSpan? _timeEnd;
        private ICollection<ActivityDTO>? _activities;

        public string ReportId
        {
            get => _reportId;
            set
            {
                _reportId = value;
            }
        }

        public TeamDTO Team
        {
            get => _team;
            set
            {
                _team = value;
            }
        }

        public DateTime? Date
        {
            get => _date;
            set
            {
                _date = value;
            }
        }

        public TimeSpan? TimeInit
        {
            get => _timeInit;
            set
            {
                _timeInit = value;
            }
        }

        public TimeSpan? TimeEnd
        {
            get => _timeEnd;
            set
            {
                _timeEnd = value;
            }
        }

        public ICollection<ActivityDTO> Activities
        {
            get => _activities;
            set
            {
                _activities = value;
            }
        }


    }

    public class ActivityDTO
    {
        private ICollection<LocationItemDTO> _locations;
        private string _description;

        public ICollection<LocationItemDTO> Locations
        {
            get => _locations;
            set
            {
                _locations = value;
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
            }
        }

        public ActivityDTO()
        {
            Locations = new List<LocationItemDTO>();
        }

    }

    public class LocationItemDTO
    {
        private int _id;
        private string _name;
        private string _icon;
        private ICollection<LocationItemDTO> _children;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
            }
        }

        public string Icon
        {
            get => _icon;
            set
            {
                _icon = value;
            }
        }

        public ICollection<LocationItemDTO> Children
        {
            get => _children;
            set
            {
                _children = value;
            }
        }

        public LocationItemDTO(LocationItem location)
        {
            Children = new List<LocationItemDTO>();

            if (location != null)
            {
                Id = location.LocationId;
                Name = location._name;
                Icon = location.Icon;

                if (location.Children != null && location.Children.Count > 0)
                {
                    foreach (var child in location.Children)
                    {
                        Children.Add(new LocationItemDTO(child));
                    }
                }
            }
        }

        public LocationItemDTO(int id, string name, string icon)
        {
            Id = id;
            Name = name;
            Icon = icon;
            Children = new List<LocationItemDTO>();
        }

        public LocationItemDTO()
        {
            Children = new List<LocationItemDTO>();
        }

    }
}