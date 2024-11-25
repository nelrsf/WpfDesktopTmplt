namespace WpfDesktopTmplt.Core.Application.Interfaces
{
    internal interface IViewModelLocator
    {
        public T GetViewModel<T>() where T : class;
    }
}
