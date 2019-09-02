using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace EcoHand
{
    public class Connector : Control, INotifyPropertyChanged
    {

        // punto de inicio para el arrastre, relativo a DesignerCanvas
        private Point? dragStartPoint = null;

        public ConnectorOrientation Orientation { get; set; }

        // posicion central de este Connector relativo al DesignerCanvas
        private Point position;
        public Point Position
        {
            get { return position; }
            set
            {
                if (position != value)
                {
                    position = value;
                    OnPropertyChanged("Position");
                }
            }
        }


        // se obtiene el DesignerItem al que pertenece este conector;
        //recuperado de DataContext, que está seteado en la plantilla DesignerItem

        private DesignerItem parentDesignerItem;
        public DesignerItem ParentDesignerItem
        {
            get
            {
                if (parentDesignerItem == null)
                    parentDesignerItem = this.DataContext as DesignerItem;

                return parentDesignerItem;
            }
        }


        // realiza un seguimiento de las conexiones que enlazan con este conector
        private List<Connection> connections;
        public List<Connection> Connections
        {
            get
            {
                if (connections == null)
                    connections = new List<Connection>();
                return connections;
            }
        }

        public Connector()
        {

            // disparado cuando cambia el diseño
            base.LayoutUpdated += new EventHandler(Connector_LayoutUpdated);
        }


        // cuando el diseño cambia, se actualiza la propiedad de posición
        void Connector_LayoutUpdated(object sender, EventArgs e)
        {
            DesignerCanvas designer = GetDesignerCanvas(this);
            if (designer != null)
            {

                // obtiene la posición central de este conector en relación con DesignerCanvas
                this.Position = this.TransformToAncestor(designer).Transform(new Point(this.Width / 2, this.Height / 2));
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DesignerCanvas canvas = GetDesignerCanvas(this);
            if (canvas != null)
            {

                // posición relativa a DesignerCanvas
                this.dragStartPoint = new Point?(e.GetPosition(canvas));
                e.Handled = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);


            // si no se presiona el botón del mouse, no tenemos operación de arrastre, ...
            if (e.LeftButton != MouseButtonState.Pressed)
            this.dragStartPoint = null;

            // pero si se presiona el botón del mouse y se establece el valor del punto de inicio, tenemos uno
            if (this.dragStartPoint.HasValue)
            {
                // crea ConnectionAdorner 
                DesignerCanvas canvas = GetDesignerCanvas(this);
                if (canvas != null)
                {
                    AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(canvas);
                    if (adornerLayer != null)
                    {
                        ConnectorAdorner adorner = new ConnectorAdorner(canvas, this);
                        if (adorner != null)
                        {
                            adornerLayer.Add(adorner);
                            e.Handled = true;
                        }
                    }
                }
            }
        }

        internal ConnectorInfo GetInfo()
        {
            ConnectorInfo info = new ConnectorInfo();
            info.DesignerItemLeft = DesignerCanvas.GetLeft(this.ParentDesignerItem);
            info.DesignerItemTop = DesignerCanvas.GetTop(this.ParentDesignerItem);
            info.DesignerItemSize = new Size(this.ParentDesignerItem.ActualWidth, this.ParentDesignerItem.ActualHeight);
            info.Orientation = this.Orientation;
            info.Position = this.Position;
            return info;
        }


        // iterar a través del árbol visual para obtener el DesignerCanvas padre
        private DesignerCanvas GetDesignerCanvas(DependencyObject element)
        {
            while (element != null && !(element is DesignerCanvas))
                element = VisualTreeHelper.GetParent(element);

            return element as DesignerCanvas;
        }

        #region INotifyPropertyChanged Members


        // se puede usar DependencyProperties también para informar a otros sobre cambios de propiedad
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
    }

    // proporciona información compacta sobre un conector; utilizado para el algoritmo de enrutamiento,
    //en lugar de entregar un conector completo

    internal struct ConnectorInfo
    {
        public double DesignerItemLeft { get; set; }
        public double DesignerItemTop { get; set; }
        public Size DesignerItemSize { get; set; }
        public Point Position { get; set; }
        public ConnectorOrientation Orientation { get; set; }
    }

    public enum ConnectorOrientation
    {
        None,
        Left,
        Top,
        Right,
        Bottom
    }
}
