using System.Windows;
using System.Windows.Controls;
using WpfDesktopTmplt.Presentation.Controls;
using WpfDesktopTmplt.Presentation.Services;

namespace WpfDesktopTmplt.Presentation.Dialogs
{
    public partial class NewViewDialog : UserControl
    {
        private Window _parentWindow;

        public NewViewDialog()
        {
            InitializeComponent();
            DataContext = new NewViewDialogViewModel();
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
                var viewName = selectedItem.Name;
                AppMediatorService mediator = AppMediatorService.GetInstance();
                mediator.notify(this, CommandDictionary.InsertNewView, viewName);
                _parentWindow.Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _parentWindow.Close();
        }
    }
}

