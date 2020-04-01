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

        // ***** Private members *****

        private int _rows;
        private int _columns;
        private List<GridCell> _cells;
        private bool _mouseDown;
        private Point _mouseDownPos;


        // ***** Public members *****

        /// <summary>
        /// Amount of rows in the grid
        /// </summary>
        public int Rows {
            get => _rows;
            set {
                _rows = value;
                UpdateGrid();
            }
        }

        /// <summary>
        /// Amount of columns in the grid
        /// </summary>
        public int Columns {
            get => _columns;
            set {
                _columns = value;
                UpdateGrid();
            }
        }



        public (int left, int top, int width, int height) Selection {
            get => (ValueTuple<int, int, int, int>)GetValue(SelectionProperty);
            set => SetValue(SelectionProperty, value);
        }

        // ToDo: Implement setter
        public static readonly DependencyProperty SelectionProperty =
            DependencyProperty.Register(nameof(Selection), typeof(ValueTuple<int, int, int, int>), typeof(GridSelector)
                ); //, new PropertyMetadata((0, 0, 0, 0), new PropertyChangedCallback((d, e) => { }))


        public ICommand SelectionCommand {
            get => (ICommand)GetValue(SelectionCommandProperty);
            set => SetValue(SelectionCommandProperty, value);
        }

        public static readonly DependencyProperty SelectionCommandProperty =
            DependencyProperty.Register(nameof(SelectionCommand), typeof(ICommand), typeof(GridSelector)); //, new PropertyMetadata(0));


        // ***** Constructor *****

        public GridSelector()
        {
            InitializeComponent();
            _cells = new List<GridCell>();
        }


        // ***** Private methods *****

        /// <summary>
        /// Updates the grid (called when rows / cols change)
        /// </summary>
        private void UpdateGrid()
        {
            // Reset grid
            rectGrid.RowDefinitions.Clear();
            rectGrid.ColumnDefinitions.Clear();
            rectGrid.Children.Clear();
            _cells = new List<GridCell>();

            // Add row / col definitions
            for (int i = 0; i < Rows; i++)
                rectGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            for (int j = 0; j < Columns; j++)
                rectGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            // Add hoverable rects
            for (int r = 0; r < Rows; r++) {
                for (int c = 0; c < Columns; c++) {
                    // Create rect
                    var rect = new GridCell() { Index = (r, c) };

                    // Add to grid
                    Grid.SetRow(rect, r);
                    Grid.SetColumn(rect, c);
                    _ = rectGrid.Children.Add(rect);
                    _cells.Add(rect);
                }
            }

        }
        
        /// <summary>
        /// Called when the mouse is pressed down on the control to implement selection
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            // Capture and track the mouse.
            _mouseDown = true;
            _mouseDownPos = e.GetPosition(mainGrid);
            _ = mainGrid.CaptureMouse();

            // Initial placement of the drag selection box
            UpdateSelectionBox(_mouseDownPos.X, _mouseDownPos.Y, 0, 0);
            selectionBox.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Called when the mouse is released on the control to implement selection
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            // Release the mouse capture and stop tracking it.
            _mouseDown = false;
            mainGrid.ReleaseMouseCapture();
            selectionBox.Visibility = Visibility.Collapsed;

            // Raise event so the window can adjust
            var selectedCells = _cells.Where(cell => cell.IsHighlighted);
            if (selectedCells.Any()) {
                int rowMin = selectedCells.OrderBy(cell => cell.Index.Row).First().Index.Row;
                int rowMax = selectedCells.OrderByDescending(cell => cell.Index.Row).First().Index.Row;
                int colMin = selectedCells.OrderBy(cell => cell.Index.Column).First().Index.Column;
                int colMax = selectedCells.OrderByDescending(cell => cell.Index.Column).First().Index.Column;

                Selection = (colMin, rowMin, colMax - colMin + 1, rowMax - rowMin + 1);
                SelectionCommand?.Execute(null);
            }

            // Reset highlighting
            foreach (var cell in _cells)
                cell.IsHighlighted = false;
        }

        /// <summary>
        /// Called when the mouse is moving over the control to implement selection
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!_mouseDown)
                return;

            // ** Clamp mousepos in selector area
            var mousePos = e.GetPosition(mainGrid);

            // ** Although the selection rectangle needs to be relative to the main grid, clamping needs to be done on the whole control
            var realMousePos = e.GetPosition(this);
            var (clampedX, clampedY) = (Util.Clamp(0, ActualWidth, realMousePos.X), 
                                        Util.Clamp(0, ActualHeight, realMousePos.Y));
            mousePos = new Point(mousePos.X + (clampedX - realMousePos.X), mousePos.Y + (clampedY - realMousePos.Y));

            // ** Update selection rectangle
            double left = Math.Min(_mouseDownPos.X, mousePos.X);
            double width = Math.Abs(mousePos.X - _mouseDownPos.X);
            double top = Math.Min(_mouseDownPos.Y, mousePos.Y);
            double height = Math.Abs(mousePos.Y - _mouseDownPos.Y);
            UpdateSelectionBox(left, top, width, height);

            // ** Update highlighted rects
            var selection = new Rect(left, top, width, height);
            foreach (var rectObj in _cells) {
                // Check intersection of grid cell with selection
                var rectLoc = rectObj.TransformToAncestor(mainGrid).Transform(new Point(0, 0));
                var rect = new Rect(rectLoc.X, rectLoc.Y, rectObj.ActualWidth, rectObj.ActualHeight);
                bool intersect = selection.IntersectsWith(rect);
                rectObj.IsHighlighted = intersect;
            }
        }

        /// <summary>
        /// Updates selection box 
        /// </summary>
        /// <param name="left">The X coordinate of the top left point</param>
        /// <param name="top">The Y coordinate of the top left point</param>
        /// <param name="width">The width of the box</param>
        /// <param name="height">The height of the box</param>
        private void UpdateSelectionBox(double left, double top, double width, double height)
        {
            Canvas.SetLeft(selectionBox, left);
            Canvas.SetTop(selectionBox, top);
            selectionBox.Width = width;
            selectionBox.Height = height;
        }
    }
}
