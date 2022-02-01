using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        /// <summary>
        /// Current selection (as indices)
        /// </summary>
        public (int left, int top, int width, int height) Selection {
            get => (ValueTuple<int, int, int, int>)GetValue(SelectionProperty);
            set => SetValue(SelectionProperty, value);
        }

        /// <summary>
        /// Command which will be executed when selection has been made
        /// </summary>
        public ICommand SelectionCommand {
            get => (ICommand)GetValue(SelectionCommandProperty);
            set => SetValue(SelectionCommandProperty, value);
        }


        // *** DependencyProperties ***

        // ToDo: Implement setter
        public static readonly DependencyProperty SelectionProperty =
            DependencyProperty.Register(nameof(Selection), typeof(ValueTuple<int, int, int, int>), typeof(GridSelector));

        public static readonly DependencyProperty SelectionCommandProperty =
            DependencyProperty.Register(nameof(SelectionCommand), typeof(ICommand), typeof(GridSelector));


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
            mainGrid.RowDefinitions.Clear();
            mainGrid.ColumnDefinitions.Clear();
            mainGrid.Children.Clear();
            _cells = new List<GridCell>();

            // Add row / col definitions
            for (int i = 0; i < Rows; i++) {
                mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            for (int j = 0; j < Columns; j++) {
                mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            // Add hoverable rects
            for (int r = 0; r < Rows; r++) {
                for (int c = 0; c < Columns; c++) {
                    // Create rect
                    var rect = new GridCell() { Index = (r, c) };

                    // Add to grid
                    Grid.SetRow(rect, r);
                    Grid.SetColumn(rect, c);
                    _ = mainGrid.Children.Add(rect);
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

            // ** Left starts selection, right aborts
            switch (e.ChangedButton) {
                case MouseButton.Right:
                    // Reset selection
                    ResetSelection();
                    break;

                case MouseButton.Left:
                    // Capture and track the mouse.
                    _mouseDown = true;
                    _mouseDownPos = e.GetPosition(mainGrid);
                    _ = mainGrid.CaptureMouse();
                    break;
            }
        }

        /// <summary>
        /// Called when the mouse is released on the control to implement selection
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.ChangedButton != MouseButton.Left) {
                return;
            }

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
            ResetSelection();
        }

        /// <summary>
        /// Called when the mouse is moving over the control to implement selection
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!_mouseDown) {
                return;
            }

            // ** Clamp mousepos in selector area
            var mousePos = e.GetPosition(mainGrid);

            // ** Although the selection rectangle needs to be relative to the main grid, clamping needs to be done on the whole control
            var realMousePos = e.GetPosition(this);
            var (clampedX, clampedY) = (Util.Clamp(0, ActualWidth, realMousePos.X),
                                        Util.Clamp(0, ActualHeight, realMousePos.Y));
            mousePos = new Point(mousePos.X + (clampedX - realMousePos.X), mousePos.Y + (clampedY - realMousePos.Y));


            // ** Update highlighted rects
            double left = Math.Min(_mouseDownPos.X, mousePos.X);
            double width = Math.Abs(mousePos.X - _mouseDownPos.X);
            double top = Math.Min(_mouseDownPos.Y, mousePos.Y);
            double height = Math.Abs(mousePos.Y - _mouseDownPos.Y);
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
        /// Resets the selection (either on mouse up or right click)
        /// </summary>
        private void ResetSelection()
        {
            _mouseDown = false;
            mainGrid.ReleaseMouseCapture();
            Selection = (0, 0, 0, 0);
            // Reset highlighting
            foreach (var cell in _cells) {
                cell.IsHighlighted = false;
            }
        }
    }
}
