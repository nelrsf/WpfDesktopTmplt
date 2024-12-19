using System.Windows.Input;
using System.Windows;

namespace iPlanner.Presentation.Services
{
    public class DropEventArgs<T> : EventArgs
    {
        public object Sender { get; }
        public T Data { get; }

        public DropEventArgs(object sender, T data)
        {
            Sender = sender;
            Data = data;
        }
    }
    public class DragNDropManager <T>
    {
        public Point? StartPoint { get; private set; }
        public FrameworkElement? DragElement { get; private set; }
        private bool isDragging = false;

        public delegate void DropEventHandler<T>(T data);
        public event DropEventHandler<DropEventArgs<T>> OnItemDropped;

        public T DataTransfer { get; set; }


        // Métodos para el destino del drop
        public void HandleDragEnter(object sender, DragEventArgs e)
        {
            Type expectedType = typeof(T);
            if (e.Data.GetDataPresent(expectedType))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        public void HandleDragOver(object sender, DragEventArgs e)
        {
            // Similar al DragEnter
            Type expectedType = typeof(T);
            if (e.Data.GetDataPresent(expectedType))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        public void HandleDrop(object sender, DragEventArgs e)
        {
            Type expectedType = typeof(T);
            if (e.Data.GetDataPresent(expectedType))
            {
                var droppedData = e.Data.GetData(expectedType);
                DropEventArgs<T> dropEventArgs = new DropEventArgs<T>(sender, (T)droppedData);
                OnItemDropped?.Invoke(dropEventArgs);
            }
            e.Handled = true;
        }

        // Método para iniciar el proceso de arrastre
        public void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragElement = sender as FrameworkElement;
            StartPoint = e.GetPosition(null);
            CaptureMouse();
        }

        // Método para el movimiento del mouse
        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!isDragging && DragElement != null && DragElement.IsMouseCaptured)
            {
                if (IsDragging(sender, e))
                {
                    isDragging = true;
                    StartDrag();
                    CleanUp();
                }
            }
        }

        // Método para finalizar el proceso
        public void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            CleanUp();
        }

        // Verifica si debe iniciar el arrastre
        public bool IsDragging(object sender, MouseEventArgs e)
        {
            if (StartPoint == null) return false;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point mousePos = e.GetPosition(null);
                Vector diff = StartPoint.Value - mousePos;
                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    return true;
                }
            }
            return false;
        }

        // Captura el mouse
        private void CaptureMouse()
        {
            DragElement?.CaptureMouse();
        }

        // Inicia la operación de arrastre
        private void StartDrag()
        {
            if (DataTransfer != null)
            {
                DragDrop.DoDragDrop(DragElement, DataTransfer, DragDropEffects.Copy);
            }
        }

        // Limpia el estado
        private void CleanUp()
        {
            if (DragElement != null && DragElement.IsMouseCaptured)
            {
                DragElement.ReleaseMouseCapture();
            }
            DragElement = null;
            StartPoint = null;
            isDragging = false;
        }
    }
}