using System.Windows.Controls;

namespace iPlanner.Presentation.Interfaces
{
    public interface IFormControl<T> where T : class
    {
        void ViewReport(T entity);


        void EditReport(T entity);


        public void CreateNewReport();
    }
}
