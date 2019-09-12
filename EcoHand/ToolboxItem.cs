using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace EcoHand
{
    // Representa un item seleccionable en el Toolbox/>.
    public class ToolboxItem : ContentControl
    {

        // almacena en caché el punto de inicio de la operación de arrastre
        private Point? dragStartPoint = null;

        static ToolboxItem()
        {

            // establece la clave para hacer referencia al estilo de este control
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ToolboxItem), new FrameworkPropertyMetadata(typeof(ToolboxItem)));
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            this.dragStartPoint = new Point?(e.GetPosition(this));
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton != MouseButtonState.Pressed)
                this.dragStartPoint = null;

            if (this.dragStartPoint.HasValue)
            {

                // XamlWriter.Save () tiene limitaciones en exactamente lo que se serializa; solución a corto plazo solamente.

                string xamlString = XamlWriter.Save(this.Content);
                DragObject dataObject = new DragObject();
                dataObject.Xaml = xamlString;

                WrapPanel panel = VisualTreeHelper.GetParent(this) as WrapPanel;
                if (panel != null)
                {

                    // el tamaño deseado para DesignerCanvas es el tamaño del elemento de Toolbox estirado
                    double scale = 1.3;
                    dataObject.DesiredSize = new Size(panel.ItemWidth * scale, panel.ItemHeight * scale);
                }

                DragDrop.DoDragDrop(this, dataObject, DragDropEffects.Copy);

                e.Handled = true;
            }
        }
    }


    // Envuelve información del objeto arrastrado en una clase
    public class DragObject
    {

        // cadena Xaml que representa el contenido serializado
        public String Xaml { get; set; }


        // Define el ancho y la altura de DesignerItem cuando este DragObject se suelta en DesignerCanvas

        public Size? DesiredSize { get; set; }
    }
}
