using System.Windows.Controls;

namespace iPlanner.Presentation.Interfaces
{
    public interface IControlAbstractFactory
    {
        public IFormControl<Model> CreateFormControl<Model, Control>(Model model, bool readOnlyModel) where Model : class where Control : IFormControl<Model>, new();
        public IFormControl<Model> CreateFormControl<Model, Control>() where Model : class where Control : IFormControl<Model>, new();
        public UserControl CreateControl(Type type);
    }
}
