using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SnapNET.Model.Settings
{
    internal class GridSettings
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        public int HorizontalMargin { get; set; }
        public int VerticalMargin { get; set; }


        public int LeftMargin { get; set; }
        public int RightMargin { get; set; }
        public int TopMargin { get; set; }
        public int BottomMargin { get; set; }


        public Rect GetTileRectAtIndex(int i, int j, int screenWidth, int screenHeight)
        {
            if (i < 0 || j < 0 || i >= Rows || j >= Columns)
                throw new ArgumentOutOfRangeException("Indices out of range");

            int tileWidth = ((screenWidth - LeftMargin - RightMargin) - (Rows - 1) * HorizontalMargin) / Rows;
            int tileHeight = ((screenHeight - LeftMargin - RightMargin) - (Columns - 1) * HorizontalMargin) / Columns;

            int tileLeft = LeftMargin + i * (tileWidth + HorizontalMargin);
            int tileTop = TopMargin +j * (tileHeight + VerticalMargin);

            return new Rect(tileLeft, tileTop, tileWidth, tileHeight);
        }
    }
}
