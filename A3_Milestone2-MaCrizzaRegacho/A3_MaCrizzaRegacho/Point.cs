using System;
using System.Collections.Generic;
using System.Globalization;
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


        public List<Point> GetDirections() => new List<Point>() { South, East, West, North };

        public Point South => new Point(Row + 1, Column);

        public Point East => new Point(Row, Column + 1);

        public Point West => new Point(Row, Column - 1);

        public Point North => new Point(Row - 1, Column);

    }
}
