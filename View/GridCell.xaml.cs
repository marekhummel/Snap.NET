using System.Windows.Controls;
using System.Windows.Media;

namespace SnapNET.View
{
    /// <summary>
    /// Single cell of <see cref="GridSelector"/>
    /// </summary>
    public partial class GridCell : UserControl
    {

        // ***** Private members *****

        private bool _isHighlighted = false;


        // ***** Public members *****

        /// <summary>
        /// Index of this cell in the grid
        /// </summary>
        public (int Row, int Column) Index { get; set; }

        /// <summary>
        /// Flag if this cell is selected
        /// </summary>
        public bool IsHighlighted {
            get => _isHighlighted;
            set {
                _isHighlighted = value;
                Rect.Fill = _isHighlighted ? Brushes.Green : Brushes.Red;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public GridCell()
        {
            InitializeComponent();
        }
    }
}
