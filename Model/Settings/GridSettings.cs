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
        /// Returns location and dimension of an grid cell
        /// </summary>
        /// <param name="i">The column of the cell</param>
        /// <param name="j">The row of the cell</param>
        /// <param name="screenWidth"></param>
        /// <param name="screenHeight"></param>
        /// <returns></returns>
        public Rect GetTileRectAtIndex(int i, int j, int screenWidth, int screenHeight)
        {
            if (i < 0 || j < 0 || i >= Rows || j >= Columns)
                throw new ArgumentOutOfRangeException("Indices out of range");

            int tileWidth = ((screenWidth - LeftMargin - RightMargin) - (Columns - 1) * HorizontalMargin) / Columns;
            int tileHeight = ((screenHeight - TopMargin - BottomMargin) - (Rows - 1) * VerticalMargin) / Rows;

            int tileLeft = LeftMargin + i * (tileWidth + HorizontalMargin);
            int tileTop = TopMargin + j * (tileHeight + VerticalMargin);

            return new Rect(tileLeft, tileTop, tileWidth, tileHeight);
        }
    }
}
