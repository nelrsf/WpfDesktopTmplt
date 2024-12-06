using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace iPlanner.Presentation.Controls
{
    public class ReadStatusToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isRead = (bool)value;
            return isRead ? Brushes.Transparent : new SolidColorBrush(Color.FromRgb(0, 120, 215));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string type = value as string;
            switch (type)
            {
                case "report":
                    return new SolidColorBrush(Color.FromRgb(52, 152, 219));
                case "reminder":
                    return new SolidColorBrush(Color.FromRgb(155, 89, 182));
                case "message":
                    return new SolidColorBrush(Color.FromRgb(46, 204, 113));
                case "achievement":
                    return new SolidColorBrush(Color.FromRgb(241, 196, 15));
                default:
                    return new SolidColorBrush(Color.FromRgb(149, 165, 166));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public partial class NotificationsControl : UserControl
    {
        public class NotificationItem
        {
            public string? Icon { get; set; }
            public string? Title { get; set; }
            public string? Message { get; set; }
            public string? Time { get; set; }
            public string? Type { get; set; }
            public bool IsRead { get; set; }
            public bool IsImportant { get; set; }
        }

        private ObservableCollection<NotificationItem>? Notifications { get; set; }

        public NotificationsControl()
        {
            InitializeComponent();
            InitializeNotifications();
        }

        private void InitializeNotifications()
        {
            Notifications = new ObservableCollection<NotificationItem>
            {
                new NotificationItem
                {
                    Icon = "📊",
                    Title = "Reporte Mensual Disponible",
                    Message = "El reporte de ventas de octubre está listo para su revisión",
                    Time = "Hace 5 minutos",
                    Type = "report",
                    IsRead = false,
                    IsImportant = true
                },
                new NotificationItem
                {
                    Icon = "📅",
                    Title = "Recordatorio: Reunión de Equipo",
                    Message = "Reunión programada para hoy a las 15:00",
                    Time = "Hace 30 minutos",
                    Type = "reminder",
                    IsRead = false,
                    IsImportant = false
                },
                new NotificationItem
                {
                    Icon = "💬",
                    Title = "Nuevo mensaje de Juan Pérez",
                    Message = "¿Podemos revisar la propuesta comercial?",
                    Time = "Hace 1 hora",
                    Type = "message",
                    IsRead = true,
                    IsImportant = false
                },
                new NotificationItem
                {
                    Icon = "🎯",
                    Title = "Meta de ventas alcanzada",
                    Message = "¡Felicitaciones! Has alcanzado tu meta mensual",
                    Time = "Hace 2 horas",
                    Type = "achievement",
                    IsRead = true,
                    IsImportant = true
                }
            };

            notificationsList.ItemsSource = Notifications;
        }

        private void MarkAllRead_Click(object sender, RoutedEventArgs e)
        {
            foreach (var notification in Notifications)
            {
                notification.IsRead = true;
            }
            notificationsList.ItemsSource = null;
            notificationsList.ItemsSource = Notifications;
        }

        private void NotificationMenu_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var contextMenu = new ContextMenu();
            contextMenu.Items.Add(new MenuItem { Header = "Marcar como leído" });
            contextMenu.Items.Add(new MenuItem { Header = "Marcar como importante" });
            contextMenu.Items.Add(new MenuItem { Header = "Eliminar" });
            button.ContextMenu = contextMenu;
            contextMenu.IsOpen = true;
        }
    }
}