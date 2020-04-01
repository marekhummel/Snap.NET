using System;
using System.Windows;

namespace SnapNET.Model.Settings
{
    /// <summary>
    /// Settings for the grid of a monitor
    /// </summary>
    internal class GridSettings
    {

        // ***** Public members  *****

        /// <summary>
        /// The amount of rows
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// The amount of columns
        /// </summary>
        public int Columns { get; set; } 

        /// <summary>
        /// Horizontal (x) distance between two grid cells
        /// </summary>
        public int HorizontalMargin { get; set; }

        /// <summary>
        /// Vertical (y) distance between two grid cells
        /// </summary>
        public int VerticalMargin { get; set; }

        /// <summary>
        /// Distance between the left screen border and the first column
        /// </summary>
        public int LeftMargin { get; set; }

        /// <summary>
        /// Distance between the right screen border and the last column
        /// </summary>
        public int RightMargin { get; set; }

        /// <summary>
        /// Distance between the top screen border and the first row
        /// </summary>
        public int TopMargin { get; set; }

        /// <summary>
        /// Distance between the bottom screen border and the last row
        /// </summary>
        public int BottomMargin { get; set; }




        // ***** Public methods *****

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="monId"></param>
        public GridSettings()
        {
            Rows = 6;
            Columns = 6;
        }

        /// <summary>
        /// Dimensions of grid cell rect
        /// </summary>
        /// <param name="mon"></param>
        /// <param name="idxLeft"></param>
        /// <param name="idxWidth"></param>
        /// <param name="idxTop"></param>
        /// <param name="idxHeight"></param>
        /// <returns></returns>
        public Rect GetTileSpanAtIndices(Monitor.Monitor mon, int idxLeft, int idxWidth, int idxTop, int idxHeight)
        {
            if (idxLeft < 0 || idxLeft + idxWidth > Columns || idxTop < 0 || idxTop + idxHeight > Rows)
                throw new ArgumentOutOfRangeException("Indices out of range");

            // Absolute dimensions of monitor
            var wa = mon.WorkingArea;

            // Dimensions of one tile
            double tileWidth = (wa.Width - LeftMargin - RightMargin - ((Columns - 1) * HorizontalMargin)) / Columns;
            double tileHeight = (wa.Height - TopMargin - BottomMargin - ((Rows - 1) * VerticalMargin)) / Rows;

            // Args of the rect
            double left = wa.Left + LeftMargin + (idxLeft * (tileWidth + HorizontalMargin));
            double top = wa.Top + TopMargin + (idxTop * (tileHeight + VerticalMargin));
            double width = (tileWidth * idxWidth) + (HorizontalMargin * (idxWidth - 1));
            double height = (tileHeight * idxHeight) + (VerticalMargin * (idxHeight - 1));
            return new Rect(left, top, width, height);
        }
    }
}
