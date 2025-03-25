using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3_MaCrizzaRegacho
{
    /// <summary>
    /// Represents a point in a grid.
    /// </summary>
    public class Point
    {
        // Properties
        public int Row { get; }
        public int Column { get; }
        public Point Parentpoint { get; set; }

        // Constructor
        public Point(int row, int column, Point parentpoint = null) => (Row, Column, Parentpoint) = (row, column, parentpoint);

        // Methods
        public override string ToString() => $"[{Row}, {Column}]";

    }
}
