﻿using System.Windows;
using System.Windows.Controls;

namespace SnapNET.View
{
    /// <summary>
    /// Single cell of <see cref="GridSelector"/>
    /// </summary>
    public partial class GridCell : UserControl
    {

        // ***** Public members *****

        /// <summary>
        /// Index of this cell in the grid
        /// </summary>
        public (int Row, int Column) Index { get; set; }


        /// <summary>
        /// Flag if this cell is selected
        /// </summary>
        public bool IsHighlighted {
            get => (bool)GetValue(IsHighlightedProperty);
            set => SetValue(IsHighlightedProperty, value);
        }


        // *** DependencyProperties ***

        public static readonly DependencyProperty IsHighlightedProperty =
            DependencyProperty.Register(nameof(IsHighlighted), typeof(bool), typeof(GridCell));


        // ***** Constructor *****


        /// <summary>
        /// Constructor
        /// </summary>
        public GridCell()
        {
            InitializeComponent();
        }
    }
}
