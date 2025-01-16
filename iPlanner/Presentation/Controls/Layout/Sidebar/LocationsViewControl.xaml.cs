using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Services;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services;
using iPlanner.Presentation.ViewModels.Locations;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace iPlanner.Presentation.Controls.Sidebar
{
    public partial class LocationsViewControl : UserControl
    {
        private readonly DragNDropManager<List<LocationItemDTO>> _dragManager;
        private readonly LocationsViewModel _viewModel;
        private readonly SelectionManager<LocationItemDTO>? _selectionManager;
        private readonly ITreeViewOperations _treeViewOperations;

        public TreeViewSearchManager<LocationItemDTO>? SearchManager { get; private set; }

        private bool _searchMode = false;
        private ContextMenu _contextMenu;
        private TreeViewItem? _currentContextTreeviewItem;

        public LocationsViewControl()
        {
            InitializeComponent();

            _viewModel = new LocationsViewModel(AppServices.GetService<ILocationService>());
            _dragManager = new DragNDropManager<List<LocationItemDTO>>();
            _selectionManager = new SelectionManager<LocationItemDTO>();
            _treeViewOperations = new TreeViewOperations(LocationsTreeView);

            DataContext = _viewModel;

            InitializeSearchManager();
            InitializeContextMenu();
        }

        private void InitializeContextMenu()
        {
            IContextMenuBuilder builder = new LocationContextMenuBuilder();
            _contextMenu = builder
                .AddMenuItem("Agregar", () => _viewModel.AddLocation())
                .AddMenuItem("Expandir hacia arriba", () => ExpandUp(), () => _searchMode)
                .Build();
            LocationsTreeView.ContextMenu = _contextMenu;

        }

        private void ExpandUp()
        {
            if (_currentContextTreeviewItem?.DataContext is not LocationItemDTO locationItem)
                return;

            if (locationItem.Parent == null)
            {
                _viewModel.FilteredLocations = new ObservableCollection<LocationItemDTO>(_viewModel.Locations);
                _searchMode = false;
                return;
            }

            _viewModel.FilteredLocations.Clear();
            _viewModel.FilteredLocations.Add(locationItem.Parent);

            var treeViewItem = LocationsTreeView.ItemContainerGenerator
                .ContainerFromItem(locationItem.Parent) as TreeViewItem;

            if (treeViewItem != null)
            {
                treeViewItem.IsExpanded = true;
            }
        }

        private void InitializeSearchManager()
        {
            bool SearchExpression(LocationItemDTO location, string term)
                => location.Name.ToLower().Contains(term.ToLower());

            ICollection<LocationItemDTO> GetChildrenContainer(LocationItemDTO location)
                => location.Children;

            SearchManager = new TreeViewSearchManager<LocationItemDTO>(
                LocationsTreeView, SearchExpression, GetChildrenContainer);
        }

        private void TreeView_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            _currentContextTreeviewItem = _treeViewOperations
                .GetTreeViewItem(e.OriginalSource as DependencyObject);
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = (sender as TextBox)?.Text;
            List<LocationItemDTO> locations = null;

            if (searchText?.Length >= 3)
            {
                locations = SearchManager.Search(searchText, _viewModel.Locations);
            }

            UpdateFilteredLocations(locations);
        }

        private void UpdateFilteredLocations(List<LocationItemDTO> locations)
        {
            if (locations?.Count > 0)
            {
                _searchMode = true;
                _viewModel.FilteredLocations = new ObservableCollection<LocationItemDTO>(locations);
            }
            else
            {
                _searchMode = false;
                _viewModel.FilteredLocations = new ObservableCollection<LocationItemDTO>(_viewModel.Locations);
            }
        }



        // Drag and Drop event handlers
        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragManager.DataTransfer = (List<LocationItemDTO>)_selectionManager.SelectedItems;
            _dragManager.OnMouseDown(sender, e);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            _dragManager.OnMouseMove(sender, e);
        }

        private void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _dragManager.OnMouseUp(sender, e);
        }

        // Selection event handlers
        private void OnLocationCheckChanged(CheckBox checkBox, Action<LocationItemDTO> action)
        {
            if (checkBox?.DataContext is LocationItemDTO locationItemDTO)
            {
                action.Invoke(locationItemDTO);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                OnLocationCheckChanged(checkBox, l => _selectionManager.Add(l));
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                OnLocationCheckChanged(checkBox, l => _selectionManager.Remove(l));
            }
        }

        // Public methods delegating to tree view operations
        public void CollapseAllNodes() => _treeViewOperations.CollapseAllNodes();
        public void ExpandAllNodes() => _treeViewOperations.ExpandAllNodes();


        private void OnAddLocation(object sender, RoutedEventArgs e)
        {
            _viewModel.AddLocation();
        }

        private void OnEditLocation(object sender, RoutedEventArgs e)
        {
            _viewModel.EditLocation();
        }

        private void OnDeleteLocation(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteLocation();
        }

        private void OnViewLocation(object sender, RoutedEventArgs e)
        {
            _viewModel.ViewLocation();
        }


    }
    public class MenuItemDefinition
    {
        public string Header { get; set; }
        public Action Action { get; set; }
        public Func<bool> VisibilityCondition { get; set; }
    }

    public class LocationContextMenuBuilder : IContextMenuBuilder
    {
        private readonly List<MenuItemDefinition> _menuItems;

        public LocationContextMenuBuilder()
        {
            _menuItems = new List<MenuItemDefinition>();
        }

        public IContextMenuBuilder AddMenuItem(string header, Action action, Func<bool> visibilityCondition = null)
        {
            _menuItems.Add(new MenuItemDefinition
            {
                Header = header,
                Action = action,
                VisibilityCondition = visibilityCondition ?? (() => true)
            });
            return this;
        }

        public IContextMenuBuilder Clear()
        {
            _menuItems.Clear();
            return this;
        }

        public ContextMenu Build()
        {
            var contextMenu = new ContextMenu();

            foreach (var itemDef in _menuItems)
            {
                var menuItem = new MenuItem { Header = itemDef.Header };
                menuItem.Click += (s, e) => itemDef.Action();

                contextMenu.Items.Add(menuItem);

                if (itemDef.VisibilityCondition != null)
                {
                    contextMenu.Opened += (s, e) =>
                    {
                        menuItem.Visibility = itemDef.VisibilityCondition()
                            ? Visibility.Visible
                            : Visibility.Collapsed;
                    };
                }
            }

            return contextMenu;
        }
    }

    // Implementation of tree view operations
    public class TreeViewOperations : ITreeViewOperations
    {
        private readonly TreeView _treeView;

        public TreeViewOperations(TreeView treeView)
        {
            _treeView = treeView;
        }

        public void CollapseAllNodes()
        {
            foreach (var item in _treeView.Items)
            {
                if (_treeView.ItemContainerGenerator.ContainerFromItem(item) is TreeViewItem treeViewItem)
                {
                    CollapseNode(treeViewItem);
                }
            }
        }

        public void ExpandAllNodes()
        {
            foreach (var item in _treeView.Items)
            {
                if (_treeView.ItemContainerGenerator.ContainerFromItem(item) is TreeViewItem treeViewItem)
                {
                    ExpandNode(treeViewItem);
                }
            }
        }

        public TreeViewItem GetTreeViewItem(DependencyObject element)
        {
            while (element != null)
            {
                if (element is TreeViewItem tvi)
                    return tvi;

                if (element is TreeView)
                    return null;

                element = VisualTreeHelper.GetParent(element);
            }
            return null;
        }

        private void CollapseNode(TreeViewItem item)
        {
            item.IsExpanded = false;
            foreach (var subItem in item.Items)
            {
                if (item.ItemContainerGenerator.ContainerFromItem(subItem) is TreeViewItem subTreeViewItem)
                {
                    CollapseNode(subTreeViewItem);
                }
            }
        }

        private void ExpandNode(TreeViewItem item)
        {
            item.IsExpanded = true;
            foreach (var subItem in item.Items)
            {
                if (item.ItemContainerGenerator.ContainerFromItem(subItem) is TreeViewItem subTreeViewItem)
                {
                    ExpandNode(subTreeViewItem);
                }
            }
        }
    }
}