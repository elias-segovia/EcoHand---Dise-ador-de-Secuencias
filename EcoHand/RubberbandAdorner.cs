﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace EcoHand
{
    public class RubberbandAdorner : Adorner
    {
        private Point? startPoint;
        private Point? endPoint;
        private Pen rubberbandPen;

        private DesignerCanvas designerCanvas;

        public RubberbandAdorner(DesignerCanvas designerCanvas, Point? dragStartPoint)
            : base(designerCanvas)
        {
            this.designerCanvas = designerCanvas;
            this.startPoint = dragStartPoint;
            rubberbandPen = new Pen(Brushes.LightSlateGray, 1);
            rubberbandPen.DashStyle = new DashStyle(new double[] { 2 }, 1);
        }

        protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!this.IsMouseCaptured)
                    this.CaptureMouse();

                endPoint = e.GetPosition(this);
                UpdateSelection();
                this.InvalidateVisual();
            }
            else
            {
                if (this.IsMouseCaptured) this.ReleaseMouseCapture();
            }

            e.Handled = true;
        }

        protected override void OnMouseUp(System.Windows.Input.MouseButtonEventArgs e)
        {

            // liberar captura del mouse
            if (this.IsMouseCaptured) this.ReleaseMouseCapture();


            // elimina este adorno de la capa de adorno
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.designerCanvas);
            if (adornerLayer != null)
                adornerLayer.Remove(this);

            e.Handled = true;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);


            // sin antecedentes, el evento OnMouseMove no se activaría. 
            //Alternativa: implementar un Canvas como elemento secundario de este adorno, como lo hace ConnectionAdorner.

            dc.DrawRectangle(Brushes.Transparent, null, new Rect(RenderSize));

            if (this.startPoint.HasValue && this.endPoint.HasValue)
                dc.DrawRectangle(Brushes.Transparent, rubberbandPen, new Rect(this.startPoint.Value, this.endPoint.Value));
        }

        private void UpdateSelection()
        {
            designerCanvas.SelectionService.ClearSelection();

            Rect rubberBand = new Rect(startPoint.Value, endPoint.Value);
            foreach (Control item in designerCanvas.Children)
            {
                Rect itemRect = VisualTreeHelper.GetDescendantBounds(item);
                Rect itemBounds = item.TransformToAncestor(designerCanvas).TransformBounds(itemRect);

                if (rubberBand.Contains(itemBounds))
                {
                    if (item is Connection)
                        designerCanvas.SelectionService.AddToSelection(item as ISelectable);
                    else
                    {
                        DesignerItem di = item as DesignerItem;
                        if (di.ParentID == Guid.Empty)
                            designerCanvas.SelectionService.AddToSelection(di);
                    }
                }
            }
        }
    }
}