using iPlanner.Core.Application.Services;
using iPlanner.Core.Entities.Locations;
using LocationsTools.src.Controllers;
using LocationsTools.src.Model.Equipments;
using LocationsTools.src.Model.Locations;

namespace iPlanner.Infrastructure.Locations
{
    public class LocationService : ILocationService
    {
        private List<MVNetwork>? networks;
        private List<LocationItem>? locationItems;
        ILocationsController locationsController;

        public LocationService()
        {
            locationsController = new LocationControllerEFC();
            locationItems = new List<LocationItem>();
        }
        public async Task<ICollection<LocationItem>> GetLocations()
        {
            if (locationsController == null)
            {
                return new List<LocationItem>();
            }

            networks = await locationsController.getMVNetworks();
            foreach (MVNetwork network in networks)
            {
                locationItems.Add(GetLocationItemByType(network));
            }

            return locationItems;
        }

        private LocationItem? GetLocationItemByType(MVNetwork network)
        {
            if (network == null)
            {
                return null;
            }
            LocationItem locationItem = new LocationItem(network.id, network.description, "transmission");

            if (network.Substations != null && network.Substations.Count > 0)
            {
                LocationItem substationsNode = new LocationItem(-1, "Subestaciones", "home-bolt");
                locationItem.Children.Add(substationsNode);
                foreach (Substation substation in network.Substations)
                {
                    substationsNode.Children.Add(GetLocationItemByType(substation));
                }
            }

            if (network.Structures != null && network.Structures.Count > 0)
            {
                LocationItem structuresNode = new LocationItem(-1, "Estructuras", "transmission");
                locationItem.Children.Add(structuresNode);
                foreach (Structure structure in network.Structures)
                {
                    structuresNode.Children.Add(GetLocationItemByType(structure));
                }
            }

            return locationItem;
        }

        private LocationItem GetLocationItemByType(Substation substation)
        {
            if (substation == null)
            {
                return null;
            }
            LocationItem locationItem = new LocationItem(substation.id, substation.description, "home-bolt");
            if (substation.Equipments != null && substation.Equipments.Count > 0)
            {
                foreach (Equipment equipment in substation.Equipments)
                {
                    locationItem.Children.Add(GetLocationItemByType(equipment));
                }
            }
            return locationItem;
        }

        private LocationItem GetLocationItemByType(Structure structure)
        {
            if (structure == null)
            {
                return null;
            }
            LocationItem locationItem = new LocationItem(structure.id, structure.description, "transmission");
            if (structure.Equipments != null && structure.Equipments.Count > 0)
            {
                foreach (Equipment equipment in structure.Equipments)
                {
                    locationItem.Children.Add(GetLocationItemByType(equipment));
                }
            }
            return locationItem;
        }

        private LocationItem GetLocationItemByType(Equipment equipment)
        {
            if (equipment == null)
            {
                return null;
            }
            LocationItem locationItem = new LocationItem(equipment.id, equipment.description, "switch");
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
