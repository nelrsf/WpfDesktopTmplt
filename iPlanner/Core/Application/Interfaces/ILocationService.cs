using iPlanner.Core.Entities.Locations;

namespace iPlanner.Core.Application.Services
{
    public interface ILocationService
    {
        public Task<ICollection<LocationItem>> GetLocations();
    }
}
