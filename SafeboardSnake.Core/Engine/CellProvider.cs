using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafeboardSnake.Core.Const;
using SafeboardSnake.Core.Models;

namespace SafeboardSnake.Core.Engine
{
    public class CellProvider
    {
        private readonly Snake _snake;
        private readonly Gameboard _gameboard;

        public CellProvider(Gameboard gameboard, Snake snake)
        {
            _snake = snake;
            _gameboard = gameboard;
        }

        /// <summary>
        /// Returns collection of cells that are inside a gameboard and where snake is not located
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Cell> GetNonUsedCells()
        {
            var nonUsedCells = new List<Cell>();

            for (int i = 0; i < _gameboard.Height; i++)
            {
                for (int j = 0; j < _gameboard.Width; j++)
                {
                    var tmpCell = new Cell() { X = j, Y = i };
                    if (!_snake.SnakePieces.Contains(tmpCell))
                    {
                        nonUsedCells.Add(tmpCell);
                    }
                }
            }

            return nonUsedCells;
        }

        /// <summary>
        /// Returns collection of cells where snake is located at start of game
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Cell> GetSnakeStartingCells()
        {
            var snakeStartingCells = new List<Cell>();

            var centralCell = new Cell()
            {
                X = _gameboard.Width / 2,
                Y = _gameboard.Height / 2,
            };

            snakeStartingCells.Add(centralCell);
            for (int i = 0; i < Constants.InitialLengthOfSnake - 1; i++)
            {
                snakeStartingCells.Add(snakeStartingCells.Last() + new Cell() { X = 0, Y = 1 });
            }

            return snakeStartingCells;
        }
    }
}
