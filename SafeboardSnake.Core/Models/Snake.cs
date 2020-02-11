using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using SafeboardSnake.Core.Helpers;

namespace SafeboardSnake.Core.Models
{
    /// <summary>
    /// Has a collection of points that define snake-state on board
    /// </summary>
    public class Snake
    {
        /// <summary>
        /// Collection of cells that define snake state
        /// </summary>
        public LinkedList<Cell> SnakePieces { get; set; }
            = new LinkedList<Cell>();

        /// <summary>
        /// Returns head first cell - it is a head of snake
        /// </summary>      
        public Cell GetHead() => SnakePieces.First();

        /// <summary>
        /// Adds new piece to snake when food is eaten.
        /// It is added at the end in the direction of last 2 cells.
        /// </summary>
        public void AddNewPiece()
        {
            var lastCell = SnakePieces.Last();
            var differenceCell = lastCell - SnakePieces.GetLastButOne();

            SnakePieces.AddLast(new Cell()
                {
                    X = lastCell.X + differenceCell.X,
                    Y = lastCell.Y + differenceCell.Y
                }
            );
        }

        /// <summary>
        /// Moves snake in passed direction
        /// </summary>
        /// <param name="direction"></param>
        public void MoveInDirection(Direction direction)
        {
            // deleting last piece of snake
            SnakePieces.RemoveLast();

            // inserting new one at the beginning of snake-pieces-list

            switch (direction)
            {
                case Direction.Top:
                    SnakePieces.AddFirst(Cell.GetTopCell(GetHead()));
                    break;
                case Direction.Right:
                    SnakePieces.AddFirst(Cell.GetRightCell(GetHead()));
                    break;
                case Direction.Down:
                    SnakePieces.AddFirst(Cell.GetDownCell(GetHead()));
                    break;
                case Direction.Left:
                    SnakePieces.AddFirst(Cell.GetLeftCell(GetHead()));
                    break;

                default:
                    throw new ArgumentException($"Direction {direction} is not supported for snake movement");
            }
        }

        /// <summary>
        /// Returns true if snake has eaten itself
        /// </summary>
        /// <returns></returns>
        public bool IsEatenByYourself()
        {
            var headCell = GetHead();
            foreach (var snakePiece in SnakePieces.Skip(1))
            {
                if (headCell.Equals(snakePiece))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns a string containing information about all pieces of snake
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder("Snake: ");

            foreach (var cell in SnakePieces)
            {
                strBuilder.Append(cell + " ");
            }

            return strBuilder.ToString();
        }

        public void LocateSnakeAtStart(IEnumerable<Cell> initialCells)
        {
            SnakePieces.Clear();

            foreach (var cell in initialCells)
            {
                SnakePieces.AddLast(cell);
            }
        }
    }
}
