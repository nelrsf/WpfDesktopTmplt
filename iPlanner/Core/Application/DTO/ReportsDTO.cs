using iPlanner.Core.Entities.Locations;
using iPlanner.Core.Entities.Reports;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace iPlanner.Core.Application.DTO
{
    public class ReportDTO : INotifyPropertyChanged
    {
        private string _reportId;
        private TeamDTO _team;
        private DateTime? _date;
        private TimeSpan? _timeInit;
        private TimeSpan? _timeEnd;
        private ObservableCollection<ActivityDTO> _activities;

        public string ReportId
        {
            get => _reportId;
            set
            {
                _reportId = value;
                OnPropertyChanged();
            }
        }

        public TeamDTO Team
        {
            get => _team;
            set
            {
                _team = value;
                OnPropertyChanged();
            }
        }

        public DateTime? Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan? TimeInit
        {
            get => _timeInit;
            set
            {
                _timeInit = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan? TimeEnd
        {
            get => _timeEnd;
            set
            {
                _timeEnd = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ActivityDTO> Activities
        {
            get => _activities;
            set
            {
                _activities = value;
                OnPropertyChanged();
            }
        }


        public ReportDTO()
        {
            Activities = new ObservableCollection<ActivityDTO>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ActivityDTO : INotifyPropertyChanged
    {
        private ObservableCollection<LocationItemDTO> _locations;
        private string _description;

        public ObservableCollection<LocationItemDTO> Locations
        {
            get => _locations;
            set
            {
                _locations = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public ActivityDTO()
        {
            Locations = new ObservableCollection<LocationItemDTO>();

/*            if (activity != null)
            {
                Description = activity.Description;

                if (activity.Locations != null)
                {
                    foreach (var location in activity.Locations)
                    {
                        Locations.Add(new LocationItemDTO(location));
                    }
                }
            }*/
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class LocationItemDTO : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _icon;
        private ObservableCollection<LocationItemDTO> _children;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<LocationItemDTO> Children
        {
            get => _children;
            set
            {
                _children = value;
                OnPropertyChanged();
            }
        }

        public LocationItemDTO(LocationItem location)
        {
            Children = new ObservableCollection<LocationItemDTO>();

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
            Children = new ObservableCollection<LocationItemDTO>();
        }

        public LocationItemDTO()
        {
            Children = new ObservableCollection<LocationItemDTO>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}