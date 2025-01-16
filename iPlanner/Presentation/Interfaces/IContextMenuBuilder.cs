using System.Windows.Controls;

namespace iPlanner.Presentation.Interfaces
{
    public interface IContextMenuBuilder
    {
        IContextMenuBuilder AddMenuItem(string header, Action action, Func<bool> visibilityCondition = null);
        IContextMenuBuilder Clear();
        ContextMenu Build();
    }
}