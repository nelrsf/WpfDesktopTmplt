using iPlanner.Core.Application.DTO.Teams;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Entities.Teams;

namespace iPlanner.Core.Application.Mappers
{
    public class ScheduleTeamDataMapper : IMapper<ScheduleTeamDataDTO, ScheduleTeamData>
    {
        public ScheduleTeamDataDTO ToDTO(ScheduleTeamData entity)
        {
            return new ScheduleTeamDataDTO
            {
                TeamItems = entity.TeamItems.Select(item => new ScheduleTeamItemDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    TotalHours = item.TotalHours,
                    CompletionPercentage = item.CompletionPercentage,
                    HasConflicts = item.HasConflicts
                }).ToList(),
                ConflictItems = entity.ConflictItems.Select(conflict => new ConflictItemDTO
                {
                    TeamName = conflict.TeamName,
                    Date = conflict.Date,
                    Description = conflict.Description
                }).ToList()
            };
        }

        public ScheduleTeamData ToEntity(ScheduleTeamDataDTO dto)
        {
            return new ScheduleTeamData
            {
                TeamItems = dto.TeamItems.Select(item => new ScheduleTeamItem(new Team
                {
                    Id = item.Id,
                    Name = item.Name
                })
                {
                    TotalHours = item.TotalHours,
                    HasConflicts = item.HasConflicts
                }).ToList(),
                ConflictItems = dto.ConflictItems.Select(conflict => new ConflictItem
                {
                    TeamName = conflict.TeamName,
                    Date = conflict.Date,
                    Description = conflict.Description
                }).ToList()
            };
        }
    }
}
