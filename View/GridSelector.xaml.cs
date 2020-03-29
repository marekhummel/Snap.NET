using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

using SnapNET.Model;

namespace SnapNET.View
{
    /// <summary>
    /// Interaction logic for GridSelector.xaml
    /// </summary>
    public partial class GridSelector : UserControl
    {

        // ***** PRIVATE FIELDS *****

        private int _rows;
        private int _columns;
        private List<List<Rectangle>> _rects;
        private bool _mouseDown = false;
        private Point _mouseDownPos;

        public int Rows {
            get => _rows;
            set {
                _rows = value;
                UpdateGrid();
            }
        }

        public int Columns {
            get => _columns;
            set {
                _columns = value;
                UpdateGrid();
            }
        }


        public GridSelector()
        {
            InitializeComponent();
            _rects = new List<List<Rectangle>>();
        }


        private void UpdateGrid()
        {
            // Reset grid
            rectGrid.RowDefinitions.Clear();
            rectGrid.ColumnDefinitions.Clear();
            rectGrid.Children.Clear();
            _rects = new List<List<Rectangle>>();

            // Add row / col definitions
            for (int i = 0; i < Rows; i++)
                rectGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            for (int j = 0; j < Columns; j++)
                rectGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            // Add hoverable rects
            for (int i = 0; i < Rows; i++) {
                var row = new List<Rectangle>();
                for (int j = 0; j < Columns; j++) {
                    // Create rect
                    var rect = new Rectangle { Fill = Brushes.Yellow, Margin = new Thickness(2) };

                    // Add to grid
                    Grid.SetRow(rect, i);
                    Grid.SetColumn(rect, j);
                    _ = rectGrid.Children.Add(rect);
                    row.Add(rect);
                }
                _rects.Add(row);
            }

        }






        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            // Capture and track the mouse.
            _mouseDown = true;
            _mouseDownPos = e.GetPosition(mainGrid);
            _ = mainGrid.CaptureMouse();

            // Initial placement of the drag selection box.         
            Canvas.SetLeft(selectionBox, _mouseDownPos.X);
            Canvas.SetTop(selectionBox, _mouseDownPos.Y);
            selectionBox.Width = 0;
            selectionBox.Height = 0;

            // Make the drag selection box visible.
            selectionBox.Visibility = Visibility.Visible;
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            // Release the mouse capture and stop tracking it.
            _mouseDown = false;
            mainGrid.ReleaseMouseCapture();

            // Hide the drag selection box.
            selectionBox.Visibility = Visibility.Collapsed;

            var mouseUpPos = e.GetPosition(mainGrid);
            // TODO: 
            //
            // The mouse has been released, check to see if any of the items 
            // in the other canvas are contained within mouseDownPos and 
            // mouseUpPos, for any that are, select them!
            //
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!_mouseDown)
                return;

            // Clamp mousepos in selector area
            var mousePos = e.GetPosition(mainGrid);

            // Although the selection rectangle needs to be relative to the main grid, clamping needs to be done on the whole control
            var realMousePos = e.GetPosition(this);
            var (clampedX, clampedY) = (Util.Clamp(0, ActualWidth, realMousePos.X), 
                                        Util.Clamp(0, ActualHeight, realMousePos.Y));
            mousePos = new Point(mousePos.X + (clampedX - realMousePos.X), mousePos.Y + (clampedY - realMousePos.Y));

            // Update selection rectangle
            double left = Math.Min(_mouseDownPos.X, mousePos.X);
            double width = Math.Abs(mousePos.X - _mouseDownPos.X);
            double top = Math.Min(_mouseDownPos.Y, mousePos.Y);
            double height = Math.Abs(mousePos.Y - _mouseDownPos.Y);

            Canvas.SetLeft(selectionBox, left);
            Canvas.SetTop(selectionBox, top);
            selectionBox.Width = width;
            selectionBox.Height = height;

            // Update highlighted rects
            var selection = new Rect(left, top, width, height);
            foreach (var rect in _rects.SelectMany(row => row)) {
                var rectLoc = rect.TransformToAncestor(mainGrid).Transform(new Point(0, 0));
                var r = new Rect(rectLoc.X, rectLoc.Y, rect.ActualWidth, rect.ActualHeight);
                bool intersect = selection.IntersectsWith(r);
                rect.Fill = intersect ? Brushes.Green : Brushes.Red;
            }
        }
    }
}
