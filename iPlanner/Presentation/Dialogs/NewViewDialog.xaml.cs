using System.Windows;
using System.Windows.Controls;

namespace iPlanner.Presentation.Dialogs
{
    public partial class NewViewDialog : UserControl
    {
        private Window? _parentWindow;
        private NewViewDialogViewModel _viewModel;

        public NewViewDialog()
        {
            InitializeComponent();
            _viewModel = new NewViewDialogViewModel();
            DataContext = _viewModel;
            SetupEventHandlers();
            ShowInNewWindow();
        }

        private void SetupEventHandlers()
        {
            CreateButton.Click += CreateButton_Click;
            CancelButton.Click += CancelButton_Click;
        }

        private void ShowInNewWindow()
        {
            _parentWindow = new Window
            {
                Title = "Nueva Vista",
                Content = this,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = WindowStyle.ToolWindow
            };

            _parentWindow.Show();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewTypeList.SelectedItem is ViewOption selectedItem)
            {
                _viewModel.CreateNewView(selectedItem);
                if (_parentWindow != null) _parentWindow.Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (_parentWindow != null) _parentWindow.Close();
        }
    }
}

