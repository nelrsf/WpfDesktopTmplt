using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces.Repository;
using LocationsTools.src.Controllers;
using LocationsTools.src.Model.Equipments;
using LocationsTools.src.Model.Locations;

namespace iPlanner.Infrastructure.Locations
{
    public class ExternalLibraryLocationsRepository : ILocationsRepository
    {
        private List<MVNetwork>? networks;
        private List<LocationItemDTO> locationItems;
        ILocationsController locationsController;

        public ExternalLibraryLocationsRepository()
        {
            locationsController = new LocationControllerEFC();
            locationItems = new List<LocationItemDTO>();
        }
        public async Task<ICollection<LocationItemDTO>> GetLocations()
        {
            if (locationsController == null)
            {
                return new List<LocationItemDTO>();
            }

            networks = await locationsController.getMVNetworks();
            foreach (MVNetwork network in networks)
            {
                locationItems.Add(GetLocationItemByType(network));
            }

            return locationItems;
        }

        private LocationItemDTO? GetLocationItemByType(MVNetwork network)
        {
            if (network == null)
            {
                return null;
            }
            LocationItemDTO locationItem = new LocationItemDTO(network.id, network.description, "transmission");

            if (network.Substations != null && network.Substations.Count > 0)
            {
                LocationItemDTO substationsNode = new LocationItemDTO(-1, "Subestaciones", "home-bolt");
                locationItem.Children.Add(substationsNode);
                foreach (Substation substation in network.Substations)
                {
                    substationsNode.Children.Add(GetLocationItemByType(substation));
                }
            }

            if (network.Structures != null && network.Structures.Count > 0)
            {
                LocationItemDTO structuresNode = new LocationItemDTO(-1, "Estructuras", "transmission");
                locationItem.Children.Add(structuresNode);
                foreach (Structure structure in network.Structures)
                {
                    structuresNode.Children.Add(GetLocationItemByType(structure));
                }
            }

            return locationItem;
        }

        private LocationItemDTO GetLocationItemByType(Substation substation)
        {
            if (substation == null)
            {
                return null;
            }
            LocationItemDTO locationItem = new LocationItemDTO(substation.id, substation.description, "home-bolt");
            if (substation.Equipments != null && substation.Equipments.Count > 0)
            {
                foreach (Equipment equipment in substation.Equipments)
                {
                    locationItem.Children.Add(GetLocationItemByType(equipment));
                }
            }
            return locationItem;
        }

        private LocationItemDTO GetLocationItemByType(Structure structure)
        {
            if (structure == null)
            {
                return null;
            }
            LocationItemDTO locationItem = new LocationItemDTO(structure.id, structure.description, "transmission");
            if (structure.Equipments != null && structure.Equipments.Count > 0)
            {
                foreach (Equipment equipment in structure.Equipments)
                {
                    locationItem.Children.Add(GetLocationItemByType(equipment));
                }
            }
            return locationItem;
        }

        private LocationItemDTO GetLocationItemByType(Equipment equipment)
        {
            if (equipment == null)
            {
                return null;
            }
            LocationItemDTO locationItem = new LocationItemDTO(equipment.id, equipment.description, "switch");
            if (equipment.InnerEquipments != null && equipment.InnerEquipments.Count > 0)
            {
                foreach (Equipment equipment1 in equipment.InnerEquipments)
                {
                    locationItem.Children.Add(GetLocationItemByType(equipment1));
                }
            }
            return locationItem;
        }
    }
}
