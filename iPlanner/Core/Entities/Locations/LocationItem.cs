using System.Collections.ObjectModel;

namespace iPlanner.Core.Entities.Locations
{
    public class LocationItem
    {
        private bool _isSelected;
        public string _name { get; set; }

        private int LocationId { get; set; }
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
