using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace A3_MaCrizzaRegacho
{
    class Maze
    {
        // Auto-implemented property for the row length of the maze
        // It has a private setter, meaning it can only be set within the class
        // Returns the number of rows in the maze
        public int RowLength { get; private set; }

        // Returns the number of columns in the maze
        public int ColumnLength { get; private set; }



        // Auto-implemented property for the starting point of the maze
        public Point StartingPoint { get; private set; }

        // Private field to store the maze as a jagged array of characters
        private char[][] charMaze;

        // Private field to store the path as a stack of points
        private Stack<Point> pathStack;








        /// <summary>
        /// Returns the maze as a jagged array of characters.
        /// </summary>
        /// <returns>A jagged array of characters representing the maze</returns>
        public char[][] GetMaze() => charMaze;




        /// <summary>
        /// Initializes a new instance of the Maze class from a file.
        /// </summary>
        /// <param name="filePath">The path to the file containing the maze definition</param>
        public Maze(string filePath)
        {
            // Read all lines from the file, ignoring empty lines
            var lines = File.ReadAllLines(filePath)
                            .Where(line => !string.IsNullOrWhiteSpace(line)) // Ignore empty lines and check if line is not null
                            .ToArray(); // Convert to array for easier access

            // Read the first line for row and column lengths
            var firstLine = lines[0].Split(' '); // Split the line by space
            RowLength = int.Parse(firstLine[0]); // Convert string of the first element as row length to int
            ColumnLength = int.Parse(firstLine[1]); // Convert string of the second element as column length to int

            // Read the second line for the starting point
            var secondLine = lines[1].Split(' '); // Split the line by space
            StartingPoint = new Point(int.Parse(secondLine[0]), int.Parse(secondLine[1])); // Convert the two elements to int and create a new Point

            // Initialize the maze
            charMaze = new char[RowLength][]; // Initialize the jagged array with the row length
            for (int i = 0; i < RowLength; i++) // Iterate through the rows
            {
                var line = lines[i + 2]; // Get the line from the array, starting from the third line
                charMaze[i] = line.ToCharArray(); // Convert the line to a char array and assign it to the row
            }
        }









        /// <summary>
        /// Constructor initializes a new instance with an existing maze and starting point.
        /// </summary>
        /// <param name="startRow">The starting row index in the maze</param>
        /// <param name="startColumn">The starting column index in the maze.</param>
        /// <param name="existingMaze">A 2D character array representing the maze.</param>
        public Maze(int startRow, int startColumn, char[][] existingMaze)
        {
            // Check if the existing maze is null or has invalid dimensions
            if (existingMaze == null || existingMaze.Length == 0 || existingMaze[0].Length == 0)
            {
                throw new ArgumentException("Invalid maze dimensions.");
            }

            // Check if the starting row is out of bounds
            if (startRow < 0 || startRow >= existingMaze.Length)
            {
                throw new IndexOutOfRangeException("Invalid row value.");
            }

            // Check if the starting column is out of bounds
            if (startColumn < 0 || startColumn >= existingMaze[0].Length)
            {
                throw new IndexOutOfRangeException("Invalid column value.");
            }

            // Check if the starting point is a wall
            if (existingMaze[startRow][startColumn] == 'W')
            {
                throw new ApplicationException("Starting point cannot be a wall.");
            }

            // Check if the starting point is the exit
            if (existingMaze[startRow][startColumn] == 'E')
            {
                throw new ApplicationException("Starting point cannot be the exit.");
            }

            // Set the row length of the maze
            RowLength = existingMaze.Length;

            // Set the column length of the maze
            ColumnLength = existingMaze[0].Length;

            // Set the starting point of the maze
            StartingPoint = new Point(startRow, startColumn);

            // Assign the existing maze to the class field
            charMaze = existingMaze;
        }










        /// <summary>
        /// Prints the maze as a string.
        /// </summary>
        /// <returns>A string representation of the maze</returns>
        public string PrintMaze()
        {
            // Initialize an empty string to store the printed maze
            string printedMaze = "";

            // Iterate through each row of the maze
            // Initialize the loop variable 'rowIndex' to 0, which rep. the starting row index
            // Continue the loop as long as 'rowIndex' is less than the total number of rows in 'charMaze'
            // After each iteration, increment 'rowIndex' by 1
            for (int rowIndex = 0; rowIndex < charMaze.Length; rowIndex++)
            {
                // Iterate through each column of the current row
                // Initialize the loop variable 'columnIndex' to 0, which rep. the starting column index
                // Continue the loop as long as 'columnIndex' is less than the total number of columns in the current row of 'charMaze'
                // After each iteration, increment 'columnIndex' by 1
                for (int columnIndex = 0; columnIndex < charMaze[rowIndex].Length; columnIndex++)
                {
                    // Append the current character to the printed maze string
                    // It will build the string rep. of the maze by adding each character in the current row
                    printedMaze += charMaze[rowIndex][columnIndex];
                }

                // If it's not the last row, add a newline character
                // Ensures that each row is separated by a newline, making the maze easier to read
                if (rowIndex != charMaze.Length - 1)
                {
                    printedMaze += "\n";
                }
            }
            // Return the complete string representation of the maze
            return printedMaze;
        }










        /// <summary>
        /// Depth-first search algorithm to find the exit in the maze.
        /// </summary>
        /// <returns>A string representation of the path to the exit or a message indicating no exit was found</returns>
        public string DepthFirstSearch()
        {
            // Initialize the stack for the search path and push the starting point onto it
            Stack<Point> searchPathStack = new Stack<Point>(); // Create a new stack to keep track of the search path
            searchPathStack.Push(StartingPoint); // Push the starting point onto the stack

            // Initialize the stack to store the final path to the exit
            pathStack = new Stack<Point>();

            // Initialize the search result as null
            string searchResult = null;

            // Continue the search while the stack is not empty
            while (!searchPathStack.IsEmpty()) // Loop until the search path stack is empty
            {
                // Get the current point from the top of the stack
                Point currentPoint = searchPathStack.Top();

                // Mark the current point as visited if it's not the exit
                if (charMaze[currentPoint.Row][currentPoint.Column] != 'E') // Check if the current point is not the exit
                {
                    charMaze[currentPoint.Row][currentPoint.Column] = 'V'; // Mark the current point as visited
                }

                // Check if the current point is the exit
                if (charMaze[currentPoint.Row][currentPoint.Column] == 'E') // Check if the current point is the exit
                {
                    // Get the size of the search path stack as the path length
                    int pathLength = searchPathStack.Size;

                    // Create a list to store the steps of the path
                    List<Point> pathSteps = new List<Point>(); // Create a new list to store the steps of the path
                    pathSteps.Add(searchPathStack.Pop()); // Pop the exit point from the stack and add it to the path steps

                    // Mark the path to follow with '.' as we backtrack
                    while (!searchPathStack.IsEmpty()) // Loop until the search path stack is empty
                    {
                        charMaze[searchPathStack.Top().Row][searchPathStack.Top().Column] = '.'; // Mark the current point as part of the path
                        pathSteps.Add(searchPathStack.Pop()); // Pop the current point from the stack and add it to the path steps
                    }

                    // Reverse the steps to get the correct order from start to exit
                    pathSteps.Reverse();
                    string pathString = ""; // Initialize an empty string to store the path steps

                    // Build the string representation of the steps
                    foreach (Point step in pathSteps) // Iterate through each step in the path steps
                    {
                        pathString += step + "\n"; // Append the step to the path string with a newline
                        pathStack.Push(step); // Store the path in the path stack
                    }

                    // Set the returned value with the path and the maze
                    searchResult = $"Path to follow from Start [{StartingPoint.Row}, {StartingPoint.Column}] to Exit [{currentPoint.Row}, {currentPoint.Column}] - {pathLength} steps:\n" +
                                   pathString + PrintMaze(); // Set the search result with the path and the maze

                    break; // Exit the loop as the exit has been found
                }

                // Try to move to the next cell in the order: down, right, left, up
                else if (currentPoint.Row + 1 < RowLength && (charMaze[currentPoint.Row + 1][currentPoint.Column] == ' ' || charMaze[currentPoint.Row + 1][currentPoint.Column] == 'E')) // Check if moving down is possible
                {
                    searchPathStack.Push(new Point(currentPoint.Row + 1, currentPoint.Column)); // Push the point below the current point onto the stack
                }
                else if (currentPoint.Column + 1 < ColumnLength && (charMaze[currentPoint.Row][currentPoint.Column + 1] == ' ' || charMaze[currentPoint.Row][currentPoint.Column + 1] == 'E')) // Check if moving right is possible
                {
                    searchPathStack.Push(new Point(currentPoint.Row, currentPoint.Column + 1)); // Push the point to the right of the current point onto the stack
                }
                else if (currentPoint.Column - 1 >= 0 && (charMaze[currentPoint.Row][currentPoint.Column - 1] == ' ' || charMaze[currentPoint.Row][currentPoint.Column - 1] == 'E')) // Check if moving left is possible
                {
                    searchPathStack.Push(new Point(currentPoint.Row, currentPoint.Column - 1)); // Push the point to the left of the current point onto the stack
                }
                else if (currentPoint.Row - 1 >= 0 && (charMaze[currentPoint.Row - 1][currentPoint.Column] == ' ' || charMaze[currentPoint.Row - 1][currentPoint.Column] == 'E')) // Check if moving up is possible
                {
                    searchPathStack.Push(new Point(currentPoint.Row - 1, currentPoint.Column)); // Push the point above the current point onto the stack
                }
                // If no move is possible, backtrack by popping the stack
                else
                {
                    searchPathStack.Pop(); // Pop the current point from the stack to backtrack
                    // If the stack is empty, it means no exit was found
                    if (searchPathStack.IsEmpty()) // Check if the search path stack is empty
                    {
                        searchResult = "No exit found in maze!\n\n" + PrintMaze(); // Set the search result to indicate no exit was found
                        break; // Exit the loop as no exit was found
                    }
                }
            }

            return searchResult;
        }




        ///// <summary>
        ///// Returns a stack of points representing the path to follow from the starting point to the exit.
        ///// </summary>
        ///// <returns>A stack of points representing the path to follow</returns>
        //public Stack<Point> GetPathToFollow()
        //{

        //    // Check if the pathStack is null, which means that DepthFirstSearch has not been executed
        //    if (pathStack == null)
        //    {
        //        throw new ApplicationException("DepthFirstSearch must be executed before calling GetPathToFollow.");
        //    }

        //    // Create a clone of the pathStack using the CloneStack method
        //    Stack<Point> clone = CloneStack(pathStack);

        //    // Reverse the cloned stack.
        //    Stack<Point> reversedStack = new Stack<Point>(); // Create a new stack to store the reversed path
        //    while (!clone.IsEmpty()) // Loop until the cloned stack is empty
        //    {
        //        reversedStack.Push(clone.Pop()); // Pop each point from the cloned stack and push it onto the reversed stack
        //    }
        //    return reversedStack;
        //}






        ///// <summary>
        ///// Clones a stack of points.
        ///// </summary>
        ///// <param name="originalStack">The original stack to clone</param>
        ///// <returns>A cloned stack with the same elements as the original stack</returns>
        //private Stack<Point> CloneStack(Stack<Point> originalStack)
        //{
        //    Stack<Point> clonedStack = new Stack<Point>(); // Create a new stack to store the cloned elements
        //    Stack<Point> tempStack = new Stack<Point>(); // Create a temporary stack to reverse the order

        //    // Transfer elements to a temporary stack to reverse the order
        //    while (!originalStack.IsEmpty()) // Loop until the original stack is empty
        //    {
        //        tempStack.Push(originalStack.Pop()); // Pop each point from the original stack and push it onto the temporary stack
        //    }

        //    // Transfer elements back to the original stack and into the clone
        //    while (!tempStack.IsEmpty()) // Loop until the temporary stack is empty
        //    {
        //        Point point = tempStack.Pop(); // Pop each point from the temporary stack
        //        originalStack.Push(point); // Push the point back onto the original stack
        //        clonedStack.Push(point); // Push the point onto the cloned stack
        //    }
        //    return clonedStack; // Return the cloned stack
        //}
















































        public string BreadthFirstSearch()
        {
            // Check if the starting point is the exit.
            if (charMaze[StartingPoint.Row][StartingPoint.Column] == 'E')
            {
                throw new ApplicationException("Starting point cannot be the exit.");
            }

            // Enqueue the starting point.
            Queue<Point> queue = new Queue<Point>();
            queue.Enqueue(StartingPoint);
            pathStack = new Stack<Point>();
            Point endPoint = null;

            // BFS algorithm loop:
            while (!queue.IsEmpty() && endPoint == null)
            {
                // Dequeue a location for processing.
                Point current = queue.Dequeue();

                // Mark the current point as visited in the maze (if not Start or Exit).
                if (!current.Equals(StartingPoint) && charMaze[current.Row][current.Column] != 'E')
                {
                    charMaze[current.Row][current.Column] = 'V';
                }

                // Choose new directions to move based on the (S.E.W.N) pattern.
                List<Point> neighbors = GetNeighborsInOrder(current);
                foreach (Point neighbor in neighbors)
                {
                    // Only consider the neighbor if it is either an open path ' ' or the exit 'E'
                    if (charMaze[neighbor.Row][neighbor.Column] == ' ' || charMaze[neighbor.Row][neighbor.Column] == 'E')
                    {
                        // Set the current point as the Parent Point for the neighbor.
                        neighbor.ParentPoint = current;

                        // If this neighbor is the exit, set endPoint and break out of the loop.
                        if (charMaze[neighbor.Row][neighbor.Column] == 'E')
                        {
                            endPoint = neighbor;
                            break;  // Exit the neighbor loop.
                        }

                        // Otherwise, mark the neighbor as visited and enqueue it.
                        charMaze[neighbor.Row][neighbor.Column] = 'V';
                        queue.Enqueue(neighbor);
                    }
                }

                // If the exit was found in the neighbor loop, break out of the outer loop.
                if (endPoint != null)
                {
                    break;
                }
            }

            // If no exit was found, simply return the maze as is.
            if (endPoint == null)
            {
                return $"No exit found in maze!\n\n{PrintMaze()}";
            }

            // Reconstruct the path (when exit is found) by backtracking from the exit to the start.
            Stack<Point> localPathStack = new Stack<Point>();
            Point step = endPoint;
            while (step != null)
            {
                localPathStack.Push(step);
                step = step.ParentPoint;
            }

            // Mark the path in the maze and build the path string.
            // Use '.' to represent the path from start to exit.
            string pathString = "";
            Stack<Point> reversedStack = new Stack<Point>();

            while (!localPathStack.IsEmpty())
            {
                Point p = localPathStack.Pop();
                if (charMaze[p.Row][p.Column] != 'E' && charMaze[p.Row][p.Column] != 'S')
                {
                    charMaze[p.Row][p.Column] = '.';
                }
                // Append the point to the path string.
                pathString += $"{p}\n";
                reversedStack.Push(p);
            }

            pathStack = reversedStack;

            // Return the complete output including the path and the final maze state.
            return $"Path to follow from Start {StartingPoint} to Exit {endPoint} - {pathStack.Size} steps:\n{pathString}{PrintMaze()}";
        }

        // Private method to get neighbors of a point in SEWN order.
        private List<Point> GetNeighborsInOrder(Point current)
        {
            List<Point> neighbors = new List<Point>();

            Point[] directions = {
                new Point(current.Row + 1, current.Column, current), // South
                new Point(current.Row, current.Column + 1, current),   // East
                new Point(current.Row, current.Column - 1, current),   // West
                new Point(current.Row - 1, current.Column, current)    // North
            };

            foreach (var direction in directions)
            {
                // Ensure the neighbor is within the maze boundaries.
                if (direction.Row >= 0 && direction.Row < RowLength &&
                    direction.Column >= 0 && direction.Column < ColumnLength)
                {
                    neighbors.Add(direction);
                }
            }

            return neighbors;
        }









        /// <summary>
        /// Returns a stack of points representing the path to follow from the starting point to the exit.
        /// </summary>
        /// <returns>A stack of points representing the path to follow</returns>
        public Stack<Point> GetPathToFollow()
        {
            // Check if the pathStack is null, which means that BreadthFirstSearch has not been executed
            if (pathStack == null)
            {
                throw new ApplicationException("BreadthFirstSearch must be executed before calling GetPathToFollow.");
            }

            // Create a clone of the pathStack using the CloneStack method
            Stack<Point> clone = CloneStack(pathStack);

            // Reverse the cloned stack.
            Stack<Point> reversedStack = new Stack<Point>(); // Create a new stack to store the reversed path
            while (!clone.IsEmpty()) // Loop until the cloned stack is empty
            {
                reversedStack.Push(clone.Pop()); // Pop each point from the cloned stack and push it onto the reversed stack
            }
            return reversedStack;
        }

        /// <summary>
        /// Clones a stack of points.
        /// </summary>
        /// <param name="originalStack">The original stack to clone</param>
        /// <returns>A cloned stack with the same elements as the original stack</returns>
        private Stack<Point> CloneStack(Stack<Point> originalStack)
        {
            Stack<Point> clonedStack = new Stack<Point>(); // Create a new stack to store the cloned elements
            Stack<Point> tempStack = new Stack<Point>(); // Create a temporary stack to reverse the order

            // Transfer elements to a temporary stack to reverse the order
            while (!originalStack.IsEmpty()) // Loop until the original stack is empty
            {
                tempStack.Push(originalStack.Pop()); // Pop each point from the original stack and push it onto the temporary stack
            }

            // Transfer elements back to the original stack and into the clone
            while (!tempStack.IsEmpty()) // Loop until the temporary stack is empty
            {
                Point point = tempStack.Pop(); // Pop each point from the temporary stack
                originalStack.Push(point); // Push the point back onto the original stack
                clonedStack.Push(point); // Push the point onto the cloned stack
            }
            return clonedStack; // Return the cloned stack
        }






    }
}


