using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces.Repository;

namespace iPlanner.Core.Application.Services
{
    public class LocationService : ILocationService
    {
        private ILocationsRepository _locationsRepository;

        public LocationService(ILocationsRepository locationsRepository)
        {
            _locationsRepository = locationsRepository;
        }

        public async Task<ICollection<LocationItemDTO>> GetLocations()
        {
            return await _locationsRepository.GetLocations();
        }


    }
}
