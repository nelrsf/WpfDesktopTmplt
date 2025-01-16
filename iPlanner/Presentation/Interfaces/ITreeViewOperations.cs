using System.Windows;
using System.Windows.Controls;

namespace iPlanner.Presentation.Interfaces
{
    // Interface for tree view operations
    public interface ITreeViewOperations
    {
        void CollapseAllNodes();
        void ExpandAllNodes();
        TreeViewItem GetTreeViewItem(DependencyObject element);
    }
}