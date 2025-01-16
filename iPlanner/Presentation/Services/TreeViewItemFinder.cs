using System.Windows.Controls;

public class TreeViewItemFinder
{
    public TreeViewItem? FindTreeViewItem(TreeView? treeView, object? item)
    {
        if (treeView == null || item == null) return null;

        // Aseguramos que el TreeView esté completamente cargado
        treeView.UpdateLayout();

        // Buscamos el item directamente primero
        TreeViewItem? container = treeView.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
        if (container != null) return container;

        // Si no lo encontramos, recorremos los items visibles
        foreach (var treeItem in treeView.Items)
        {
            TreeViewItem? treeViewItem = treeView.ItemContainerGenerator.ContainerFromItem(treeItem) as TreeViewItem;
            if (treeViewItem == null) continue;

            if (treeViewItem.DataContext?.Equals(item) == true)
                return treeViewItem;

            // Expandimos temporalmente para generar los hijos
            bool wasExpanded = treeViewItem.IsExpanded;
            treeViewItem.IsExpanded = true;
            treeViewItem.UpdateLayout();

            container = FindTreeViewItemInChildren(treeViewItem, item);
            if (container != null) return container;

            // Restauramos el estado original si no encontramos el item
            if (!wasExpanded)
                treeViewItem.IsExpanded = false;
        }

        return null;
    }

    private TreeViewItem? FindTreeViewItemInChildren(TreeViewItem parent, object item)
    {
        // Aseguramos que los hijos estén generados
        parent.UpdateLayout();

        for (int i = 0; i < parent.Items.Count; i++)
        {
            var childItem = parent.Items[i];
            TreeViewItem? childContainer = parent.ItemContainerGenerator.ContainerFromItem(childItem) as TreeViewItem;

            if (childContainer == null) continue;

            if (childContainer.DataContext?.Equals(item) == true)
                return childContainer;

            // Expandimos temporalmente para generar los nietos
            bool wasExpanded = childContainer.IsExpanded;
            childContainer.IsExpanded = true;
            childContainer.UpdateLayout();

            var foundItem = FindTreeViewItemInChildren(childContainer, item);
            if (foundItem != null) return foundItem;

            // Restauramos el estado original si no encontramos el item
            if (!wasExpanded)
                childContainer.IsExpanded = false;
        }

        return null;
    }
}
