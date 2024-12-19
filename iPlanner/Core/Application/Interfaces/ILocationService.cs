using iPlanner.Core.Application.DTO;

namespace iPlanner.Core.Application.Services
{
    public interface ILocationService
    {
        public Task<ICollection<LocationItemDTO>> GetLocations();
    }
}
