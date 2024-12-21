using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Entities.Locations;
using iPlanner.Core.Entities.Reports;
using iPlanner.Core.Entities.Teams;
using System.Collections.ObjectModel;

namespace iPlanner.Core.Application.Mappers
{
    public class LocationItemMapper : IMapper<LocationItemDTO, LocationItem>
    {
        public LocationItemDTO ToDTO(LocationItem entity)
        {
            LocationItemDTO dto = new LocationItemDTO()
            {
                Name = entity._name,
                Icon = entity.Icon
            };

            if (entity.Children == null || entity.Children?.Count == 0)
            {
                dto.Children = new ObservableCollection<LocationItemDTO>();
            }
            else
            {
                dto.Children = new ObservableCollection<LocationItemDTO>(
                        entity.Children.Select(l => ToDTO(l))
                    );
            }
            return dto;
        }

        public LocationItem ToEntity(LocationItemDTO dto)
        {
            LocationItem entity = new LocationItem(dto.Id, dto.Name, dto.Icon);
            if (dto.Children == null || dto.Children?.Count == 0)
            {
                entity.Children = new ObservableCollection<LocationItem>();
            }
            else
            {
                entity.Children = new ObservableCollection<LocationItem>(
                        dto.Children.Select(l => ToEntity(l))
                    );
            }
            return entity;
        }
    }
    public class ActivityMapper : IMapper<ActivityDTO, Activity>
    {
        private IMapper<LocationItemDTO, LocationItem> _locationMapper;
        public ActivityMapper(IMapper<LocationItemDTO, LocationItem> locationItemMapper)
        {
            _locationMapper = locationItemMapper;
        }
        public ActivityDTO ToDTO(Activity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Error al mapear, entidad no puede ser null");
            }

            ActivityDTO dto = new ActivityDTO()
            {
                Description = entity.Description
            };

            if (dto.Locations == null)
            {
                dto.Locations = new ObservableCollection<LocationItemDTO>();
            }
            else
            {
                dto.Locations = new ObservableCollection<LocationItemDTO>(
                    entity.Locations.Select(l => _locationMapper.ToDTO(l))
                    );
            }
            return dto;
        }

        public Activity ToEntity(ActivityDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("Error al mapear, entidad no puede ser null");
            }
            Activity entity = new Activity()
            {
                Description = dto.Description
            };
            if (dto.Locations == null)
            {
                entity.Locations = new ObservableCollection<LocationItem>();
            }
            else
            {
                entity.Locations = new ObservableCollection<LocationItem>(
                        dto.Locations.Select(l => _locationMapper.ToEntity(l))
                    );
            }
            return entity;
        }
    }

    public class ReportMapper : IMapper<ReportDTO, Report>
    {
        private IMapper<ActivityDTO, Activity> _activityMapper;
        private IMapper<TeamDTO, Team> _teamMapper;
        public ReportMapper(IMapper<ActivityDTO, Activity> activityMapper, IMapper<TeamDTO, Team> teamMapper)
        {
            _activityMapper = activityMapper;
            _teamMapper = teamMapper;
        }
        public ReportDTO ToDTO(Report entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Error al mapear, entidad no puede ser null");
            }

            ReportDTO dto = new ReportDTO()
            {
                ReportId = entity.ReportId,
                Date = entity.Date,
                TimeEnd = entity.TimeEnd,
                TimeInit = entity.TimeInit,
            };

            if (entity.Team != null)
            {
                dto.Team = _teamMapper.ToDTO(entity.Team);
            }

            if (entity.Activities == null)
            {
                dto.Activities = new ObservableCollection<ActivityDTO>();
            }
            else
            {
                dto.Activities = new ObservableCollection<ActivityDTO>(
                        entity.Activities.Select(a => _activityMapper.ToDTO(a))
                    );
            }
            return dto;

        }

        public Report ToEntity(ReportDTO dto)
        {
            Report report = new Report()
            {
                ReportId = dto.ReportId,
                Date = dto.Date,
                TimeEnd = dto.TimeEnd,
                TimeInit = dto.TimeInit,
            };
            if (dto.Team != null)
            {
                report.Team = _teamMapper.ToEntity(dto.Team);
            }

            if (dto.Activities == null)
            {
                report.Activities = new ObservableCollection<Activity>();
            }
            else
            {
                report.Activities = new ObservableCollection<Activity>(
                    dto.Activities.Select(a => _activityMapper.ToEntity(a))
                );
            }
            return report;
        }
    }
}
