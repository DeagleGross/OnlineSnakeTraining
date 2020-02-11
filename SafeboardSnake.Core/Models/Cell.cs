using System;

namespace SafeboardSnake.Core.Models
{
    public class Cell : IEquatable<Cell>
    {
        /// <summary>
        /// X-axis coordinate of cell
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Y-axis coordinate of cell
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Returns cell object which coordinates is a result of minus operation
        /// between two cell objects
        /// </summary>
        /// <param name="c1">cell from which to take</param>
        /// <param name="c2">cell which is 'minused'</param>
        /// <returns>coordinate-difference between two cells packed in cell object</returns>
        public static Cell operator -(Cell c1, Cell c2)
        {
            return new Cell()
            {
                X = c1.X - c2.X,
                Y = c1.Y - c2.Y
            };
        }

        /// <summary>
        /// Returns cell object which coordinates is a result of plus operation
        /// between two cell objects
        /// </summary>
        /// <param name="c1">one of cells to plus</param>
        /// <param name="c2">another cell to plus</param>
        /// <returns>coordinate-sum between two cells packed in cell object</returns>
        public static Cell operator +(Cell c1, Cell c2)
        {
            return new Cell()
            {
                X = c1.X + c2.X,
                Y = c1.Y + c2.Y
            };
        }

        /// <summary>
        /// Returns new cell that is 1 cell on top of passed cell
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static Cell GetTopCell(Cell cell)
        {
            return new Cell()
            {
                X = cell.X,
                Y = cell.Y - 1
            };
        }

        /// <summary>
        /// Returns new cell that is 1 cell on right of passed cell
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static Cell GetRightCell(Cell cell)
        {
            return new Cell()
            {
                X = cell.X + 1,
                Y = cell.Y
            };
        }

        /// <summary>
        /// Returns new cell that is 1 cell lower of passed cell
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static Cell GetDownCell(Cell cell)
        {
            return new Cell()
            {
                X = cell.X,
                Y = cell.Y + 1
            };
        }

        /// <summary>
        /// Returns new cell that is 1 cell on left of passed cell
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static Cell GetLeftCell(Cell cell)
        {
            return new Cell()
            {
                X = cell.X - 1,
                Y = cell.Y
            };
        }

        /// <summary>
        /// Returns true if passed coordinates are the same as this object
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool HasSameCoordinates(int x, int y)
        {
            return (X == x && Y == y);
        }

        /// <summary>
        /// Returns true if coordinates of other cell is equal in values to this-cell coordinates
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Cell other)
        {
            if (other == null)
            {
                return false;
            }

            return (this.X == other.X && this.Y == other.Y);
        }

        public override string ToString()
        {
            return $"{{{X}:{Y}}}";
        }
    }
}
