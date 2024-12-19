namespace iPlanner.Presentation.Services
{
    public class SelectionManager<T>
    {
        private ICollection<T> _selectedItems;

        public SelectionManager()
        {
            _selectedItems = new List<T>();
        }

        public SelectionManager(ICollection<T> initialCollection)
        {
            _selectedItems = initialCollection;
        }

        public int Count => _selectedItems.Count;


        public bool IsEmpty => _selectedItems.Count == 0;


        public ICollection<T> SelectedItems => _selectedItems;

        public bool Add(T item)
        {
            if (item == null || _selectedItems.Contains(item))
                return false;

            _selectedItems.Add(item);
            return true;
        }

        public int AddRange(IEnumerable<T> items)
        {
            if (items == null)
                return 0;

            int addedCount = 0;
            foreach (var item in items)
            {
                if (Add(item))
                    addedCount++;
            }
            return addedCount;
        }


        public bool Remove(T item)
        {
            if (item == null)
                return false;

            return _selectedItems.Remove(item);
        }

        public int RemoveRange(IEnumerable<T> items)
        {
            if (items == null)
                return 0;

            int removedCount = 0;
            foreach (var item in items)
            {
                if (Remove(item))
                    removedCount++;
            }
            return removedCount;
        }

        public bool Contains(T item)
        {
            if (item == null)
                return false;

            return _selectedItems.Contains(item);
        }

        public bool Toggle(T item)
        {
            if (Contains(item))
            {
                Remove(item);
                return false;
            }
            else
            {
                Add(item);
                return true;
            }
        }

        public void Clear()
        {
            _selectedItems.Clear();
        }


        public void SetSingleSelection(T item)
        {
            Clear();
            Add(item);
        }

        public void SetSelection(IEnumerable<T> items)
        {
            Clear();
            AddRange(items);
        }

        public bool ContainsAny(IEnumerable<T> items)
        {
            return items?.Any(item => Contains(item)) ?? false;
        }


        public bool ContainsAll(IEnumerable<T> items)
        {
            return items?.All(item => Contains(item)) ?? false;
        }
    }
}