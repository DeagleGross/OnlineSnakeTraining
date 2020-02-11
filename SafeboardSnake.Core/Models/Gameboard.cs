using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeboardSnake.Core.Models
{
    public class Gameboard
    {
        /// <summary>
        /// Amount of cells or pixels as width of gameboard
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Amount of cells or pixels as height of gameboard
        /// </summary>
        public int Height { get; set; }

        public Gameboard() { }

        public Gameboard(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Returns true if cell is inside gameboard
        /// </summary>
        /// <param name="cell">cell that is wanted to be checked</param>
        /// <returns>true if cell is inside a gameboard</returns>
        public bool IsInsideGameboard(Cell cell)
        {
            if (cell.X < 0 || cell.Y < 0)
            {
                return false;
            }

            if (cell.X >= Width || cell.Y >= Height)
            {
                return false;
            }

            return true;
        }
    }
}
