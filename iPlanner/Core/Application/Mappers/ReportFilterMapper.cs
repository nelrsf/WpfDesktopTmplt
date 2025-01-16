using iPlanner.Core.Application.DTO.Reports;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Entities.Reports;

namespace iPlanner.Core.Application.Mappers
{
    class ReportFilterMapper : IMapper<ReportFilterDTO, ReportFilter>
    {
        public ReportFilterDTO ToDTO(ReportFilter entity)
        {
            return new ReportFilterDTO
            {
                TimeEnd = entity.TimeEnd,
                TimeInit = entity.TimeInit,
                Date = entity.Date
            };
        }

        public ReportFilter ToEntity(ReportFilterDTO dto)
        {
            return new ReportFilter
            {
                TimeEnd = dto.TimeEnd,
                TimeInit = dto.TimeInit,
                Date = dto.Date
            };
        }
    }
}
