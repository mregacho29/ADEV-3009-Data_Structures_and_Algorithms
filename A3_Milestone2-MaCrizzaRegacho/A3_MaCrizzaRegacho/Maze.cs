using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3_MaCrizzaRegacho
{
    public class Maze
    {
        public int RowLength { get; private set; }
        public int ColumnLength { get; private set; }
        public Point StartingPoint { get; private set; }
        private char[][] charMaze;
        private Stack<Point> pathStack;
        private Point? EndPoint;

        public char[][] GetMaze() => charMaze;

        public Maze(string filePath)
        {
            var lines = File.ReadAllLines(filePath)
                            .Where(line => !string.IsNullOrWhiteSpace(line))
                            .ToArray();

            var firstLine = lines[0].Split(' ');
            RowLength = int.Parse(firstLine[0]);
            ColumnLength = int.Parse(firstLine[1]);

            var secondLine = lines[1].Split(' ');
            StartingPoint = new Point(int.Parse(secondLine[0]), int.Parse(secondLine[1]));

            charMaze = new char[RowLength][];
            for (int i = 0; i < RowLength; i++)
            {
                var line = lines[i + 2];
                charMaze[i] = line.ToCharArray();
            }

            pathStack = new Stack<Point>();
            EndPoint = null;
        }

        public Maze(int startRow, int startColumn, char[][] existingMaze)
        {
            if (existingMaze == null || existingMaze.Length == 0 || existingMaze[0].Length == 0)
            {
                throw new ArgumentException("Invalid maze dimensions.");
            }

            if (startRow < 0 || startRow >= existingMaze.Length)
            {
                throw new IndexOutOfRangeException("Invalid row value.");
            }

            if (startColumn < 0 || startColumn >= existingMaze[0].Length)
            {
                throw new IndexOutOfRangeException("Invalid column value.");
            }

            if (existingMaze[startRow][startColumn] == 'W')
            {
                throw new ApplicationException("Starting point cannot be a wall.");
            }

            if (existingMaze[startRow][startColumn] == 'E')
            {
                throw new ApplicationException("Starting point cannot be the exit.");
            }

            RowLength = existingMaze.Length;
            ColumnLength = existingMaze[0].Length;
            StartingPoint = new Point(startRow, startColumn);
            charMaze = existingMaze;

            pathStack = new Stack<Point>();
            EndPoint = null;
        }

        public string PrintMaze()
        {
            string printedMaze = "";
            for (int rowIndex = 0; rowIndex < charMaze.Length; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < charMaze[rowIndex].Length; columnIndex++)
                {
                    printedMaze += charMaze[rowIndex][columnIndex];
                }
                if (rowIndex != charMaze.Length - 1)
                {
                    printedMaze += "\n";
                }
            }
            return printedMaze;
        }

        private bool PointIsExit(Point p) => charMaze[p.Row][p.Column] == 'E';

        private void MarkPoint(Point p, char mark) => charMaze[p.Row][p.Column] = mark;

        private bool IsValidMove(Point p) =>
            p.Row >= 0 && p.Row < RowLength &&
            p.Column >= 0 && p.Column < ColumnLength &&
            (charMaze[p.Row][p.Column] == ' ' || charMaze[p.Row][p.Column] == 'E');














        // NON RECURSIVE DEPTH FIRST SEARCH
        private string DepthFirstNonRecursive ()
        {
            pathStack = new Stack<Point>();
            pathStack.Push(StartingPoint);

            while (!pathStack.IsEmpty() && EndPoint == null)
            {
                Point currentPoint = pathStack.Top();
                if (PointIsExit(currentPoint))
                {
                    EndPoint = currentPoint;
                    //break;
                }
                else
                {
                    MarkPoint(currentPoint, 'V');

                    Point southPoint    = new Point(currentPoint.Row + 1, currentPoint.Column);
                    Point eastPoint     = new Point(currentPoint.Row, currentPoint.Column + 1);
                    Point westPoint     = new Point(currentPoint.Row, currentPoint.Column - 1);
                    Point northPoint    = new Point(currentPoint.Row - 1, currentPoint.Column);

                    if (IsValidMove(southPoint))
                    {
                        pathStack.Push(southPoint);
                    }
                    else if (IsValidMove(eastPoint))
                    {
                        pathStack.Push(eastPoint);
                    }
                    else if (IsValidMove(westPoint))
                    {
                        pathStack.Push(westPoint);
                    }
                    else if (IsValidMove(northPoint))
                    {
                        pathStack.Push(northPoint);
                    }
                    else
                    {
                        pathStack.Pop();
                    }
                }

            }

            string result = "";

            if (EndPoint == null)
            {
                result = $"No exit found in maze!\n\n";
            }
            else
            {
                Stack<Point> reversedStack = new Stack<Point>();
                while (!pathStack.IsEmpty())
                {
                    result = $"{pathStack.Top()}\n{result}";
                    MarkPoint(pathStack.Top(), '.');
                    reversedStack.Push(pathStack.Pop());
                }
                pathStack = reversedStack;

                result = $"Path to follow from Start {StartingPoint} to Exit {EndPoint} - {pathStack.Size} steps:\n{result}";
            }

            result = $"{result}{PrintMaze()}";
            return result;
        }




        /// <summary>
        /// Depth First Search using recursion.
        /// </summary>
        private string DepthFirstRecursiveExitLast()
        {
            pathStack = new Stack<Point>();
            pathStack.Push(StartingPoint);

            while (!pathStack.IsEmpty() && EndPoint == null)
            {
                Point p = pathStack.Top();
                if (PointIsExit(p))
                {
                    EndPoint = p;
                    break; //EXIT FOUND
                }

                MarkPoint(p, 'V');

                bool moved = false;
                List<Point> directions = p.GetDirections(); // S E W N

                foreach (Point point in directions)
                {
                    if (IsValidMove(point))
                    {
                        pathStack.Push(point);
                        moved = true;
                        break;
                    }
                }

                if (!moved)
                {
                    pathStack.Pop();
                }
            }

            string result = "";

            if (EndPoint == null)
            {
                result = $"No exit found in maze!\n\n";
            }
            else
            {
                Stack<Point> reversedStack = new Stack<Point>();
                while (!pathStack.IsEmpty())
                {
                    result = $"{pathStack.Top()}\n{result}";
                    MarkPoint(pathStack.Top(), '.');
                    reversedStack.Push(pathStack.Pop());
                }
                pathStack = reversedStack;

                result = $"Path to follow from Start {StartingPoint} to Exit {EndPoint} - {pathStack.Size} steps:\n{result}";
            }

            result = $"{result}{PrintMaze()}";
            return result;
        }









        // reverse recursion with exit pushed first
        private string DepthFirstRecursiveExitFirst(Point p)
        {
            if (PointIsExit(p))
            {
                EndPoint = p;
                return "";
            }

            foreach (var pointDirection in p.GetDirections()) // loops over S E W N in order
            {
                if (IsValidMove(pointDirection)) // E or space
                { 
                    MarkPoint(pointDirection, 'V'); // wont change E to V
                    string result = DepthFirstRecursiveExitFirst(pointDirection);

                    if (EndPoint != null)
                    {
                        pathStack.Push(pointDirection);
                        MarkPoint(pointDirection, '.');
                        return $"{pointDirection}\n{result}";
                    }
                }
            }
            return "";
        }






        public string DepthFirstSearch()
        {
            return DepthFirstRecursiveExitLast();
        }









        public Stack<Point> GetPathToFollow()
        {
            if (pathStack == null)
            {
                throw new ApplicationException("DepthFirstSearch must be executed before calling GetPathToFollow.");
            }

            Stack<Point> clone = CloneStack(pathStack);
            Stack<Point> reversedStack = new Stack<Point>();
            while (!clone.IsEmpty())
            {
                reversedStack.Push(clone.Pop());
            }
            return reversedStack;
        }

        private Stack<Point> CloneStack(Stack<Point> originalStack)
        {
            Stack<Point> clonedStack = new Stack<Point>();
            Stack<Point> tempStack = new Stack<Point>();

            while (!originalStack.IsEmpty())
            {
                tempStack.Push(originalStack.Pop());
            }

            while (!tempStack.IsEmpty())
            {
                Point point = tempStack.Pop();
                originalStack.Push(point);
                clonedStack.Push(point);
            }
            return clonedStack;
        }
    }
}
