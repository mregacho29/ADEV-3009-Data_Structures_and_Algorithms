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
        public Point ParentPoint { get; set; } // Fixed property name

        // Constructor
        public Point(int row, int column, Point parentPoint = null)
        {
            Row = row;
            Column = column;
            ParentPoint = parentPoint;
        }
        // Methods
        public override string ToString() => $"[{Row}, {Column}]";


        // Direction properties
        public Point South => new Point(Row + 1, Column);
        public Point East => new Point(Row, Column + 1);
        public Point West => new Point(Row, Column - 1);
        public Point North => new Point(Row - 1, Column);

        // Get all directions
        public List<Point> GetDirections() => new List<Point> { South, East, West, North };
    }
}
