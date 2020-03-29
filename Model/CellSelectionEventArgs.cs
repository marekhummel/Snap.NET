using System;

namespace SnapNET.Model
{
    public class CellSelectionEventArgs : EventArgs
    {
        /// <summary>
        /// First row of selection
        /// </summary>
        public int RowStart { get; private set; }

        /// <summary>
        /// Width of selection
        /// </summary>
        public int RowSpan { get; private set; }

        /// <summary>
        /// First column of selection
        /// </summary>
        public int ColumnStart { get; private set; }

        /// <summary>
        /// Height of selection
        /// </summary>
        public int ColumnSpan { get; private set; }


        /// <summary>
        /// Creates new <see cref="CellSelectionEventArgs"/>
        /// </summary>
        /// <param name="rowstart">The first row of the selection</param>
        /// <param name="rowspan">The width of the selection (regarding indices)</param>
        /// <param name="colstart">The first column of the selection</param>
        /// <param name="colspan">The height of the selection (regarding indices)</param>
        public CellSelectionEventArgs(int rowstart, int rowspan, int colstart, int colspan)
        {
            RowStart = rowstart;
            RowSpan = rowspan;
            ColumnStart = colstart;
            ColumnSpan = colspan;
        }

    }
}
