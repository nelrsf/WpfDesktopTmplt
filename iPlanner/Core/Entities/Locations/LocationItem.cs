using System.Collections.ObjectModel;

namespace iPlanner.Core.Entities.Locations
{
    public class LocationItem
    {
        public string _name { get; set; }

        public int LocationId { get; set; }
        public string Icon { get; set; }


        public LocationItem(int id, string name, string icon)
        {
            _name = name;
            Icon = icon;
            LocationId = id;
        }



        public ObservableCollection<LocationItem> Children { get; set; } = new ObservableCollection<LocationItem>();

    }
}
