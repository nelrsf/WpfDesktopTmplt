using System.Windows.Controls;

namespace iPlanner.Presentation.Controls.Sidebar
{
    public class TreeViewSearchManager<T>
    {
        public event EventHandler<bool> OnSearchCompleted;
        public event EventHandler OnSearchInit;

        private readonly TreeView _treeView;
        private int _currentSearchIndex = -1;
        private List<T> _searchResults = new List<T>();
        private readonly Func<T, string, bool> _searchExpression;
        private readonly Func<T, ICollection<T>> _getChildreIterationCollection;
        private CancellationTokenSource _searchCancellation;
        private readonly TreeViewItemFinder _itemFinder;

        public TreeViewSearchManager(TreeView treeView, Func<T, string, bool> searchExpression,
            Func<T, ICollection<T>> getChildrenCollection)
        {
            _treeView = treeView;
            _searchExpression = searchExpression;
            _getChildreIterationCollection = getChildrenCollection;
            _searchCancellation = new CancellationTokenSource();
            _itemFinder = new TreeViewItemFinder();
        }


        public List<T> Search(string term, ICollection<T> locations)
        {
            var searchResult = new List<T>();
            try
            {
                // Usamos Stack para búsqueda en profundidad más eficiente
                var stack = new Stack<T>(locations);

                while (stack.Count > 0)
                {
                    // Verificamos cancelación en cada iteración
                    _searchCancellation.Token.ThrowIfCancellationRequested();

                    var location = stack.Pop();

                    // Verificamos si el elemento actual coincide con la búsqueda
                    if (_searchExpression(location, term))
                    {
                        searchResult.Add(location);
                    }

                    // Agregamos los hijos al stack para procesamiento
                    var children = _getChildreIterationCollection(location);
                    if (children != null && children.Any())
                    {
                        foreach (var child in children.Reverse())
                        {
                            stack.Push(child);
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Limpiamos los resultados si se cancela la búsqueda
                searchResult.Clear();
            }
            catch (Exception)
            {
                // Manejo de otros errores si es necesario
                searchResult.Clear();
            }

            return searchResult;
        }


        public void Dispose()
        {
            _searchCancellation?.Cancel();
            _searchCancellation?.Dispose();
            _searchResults?.Clear();
        }
    }
}
