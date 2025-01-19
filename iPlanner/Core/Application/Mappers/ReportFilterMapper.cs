using iPlanner.Core.Application.DTO.Reports;
using iPlanner.Core.Application.DTO.Teams;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Entities.Reports;
using iPlanner.Core.Entities.Teams;

namespace iPlanner.Core.Application.Mappers
{
    class ReportFilterMapper : IMapper<ReportFilterDTO, ReportFilter>
    {
        private IMapper<TeamDTO, Team> _teamMapper;
        public ReportFilterMapper(IMapper<TeamDTO, Team> teamMapper) {
            _teamMapper = teamMapper;
        }
        public ReportFilterDTO ToDTO(ReportFilter entity)
        {
            return new ReportFilterDTO
            {
                DateEnd = entity.DateEnd,
                DateInit = entity.DateInit,
                Team = _teamMapper.ToDTO(entity.Team ?? new Team())
            };
        }

        public ReportFilter ToEntity(ReportFilterDTO dto)
        {
            return new ReportFilter
            {
                DateEnd = dto.DateEnd,
                DateInit = dto.DateInit,
                Team = _teamMapper.ToEntity(dto.Team)
            };
        }
    }
}
