using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using SafeboardSnake.Core.Helpers;
using SafeboardSnake.Core.Models;

namespace SafeboardSnake.WPF.Models
{
    public class CellRecounter
    {
        private List<Brush> _boardCells;
        private int _boardWidth;
        private int _boardHeight;

        private void InitializeBackgroundCells(int width, int height)
        {
            _boardCells = new List<Brush>();
            _boardWidth = width;
            _boardHeight = height;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    _boardCells.Add((j + i) % 2 == 0 ?
                        ColorProvider.BackgroundColorEven :
                        ColorProvider.BackgroundColorOdd );
                }
            }
        }

        private void ColorCellCollection(List<Brush> cells, IEnumerable<Cell> cellsToColor, Brush colorBrush)
        {
            if (cellsToColor == null)
                return;

            foreach (var cellToColor in cellsToColor)
            {
                cells[cellToColor.Y * _boardWidth + cellToColor.X] = colorBrush ;
            }
        }

        private void ColorOnlyCell(List<Brush> cells, Cell cellToColor, Brush colorBrush)
        {
            cells[cellToColor.Y * _boardWidth + cellToColor.X] = colorBrush;
        }

        private void DrawWalls(List<Brush> cells, IEnumerable<Wall> walls)
        {
            foreach (var wall in walls)
            {
                for (int i = 0; i < wall.Height; i++)
                {
                    for (int j = 0; j < wall.Width; j++)
                    {
                        cells[(wall.Y + i) * _boardWidth + (wall.X + j)] = ColorProvider.WallColor;
                    }
                }
            }
        }

        public List<Brush> DrawBoardCells(int boardWidth, int boardHeight, IEnumerable<Player> playerSnakes, 
            IEnumerable<Cell> foodCells, IEnumerable<Wall> walls, IEnumerable<Cell> userSnake)
        {
            if (_boardCells.IsNullOrEmpty())
            {
                InitializeBackgroundCells(boardWidth, boardHeight);
            }

            var cells = new List<Brush>(_boardCells);

            // drawing player-snakes
            foreach (var player in playerSnakes)
            {
                ColorCellCollection(cells, player.Snake, ColorProvider.SnakeColor);
            }

            // my snake
            ColorCellCollection(cells, userSnake, ColorProvider.UserSnakeColor);

            // food and walls
            ColorCellCollection(cells, foodCells, ColorProvider.FoodColor);
            DrawWalls(cells, walls);

            return cells;
        }

        public List<Brush> RedrawBoardCells(GameState formerState, GameState currentState, List<Brush> cells, string myUserName)
        {
            // coloring former food
            foreach (var foodCell in formerState.Food)
            {
                if ((foodCell.X + foodCell.Y) % 2 == 0)
                    ColorOnlyCell(cells, foodCell, ColorProvider.BackgroundColorEven);
                else
                    ColorOnlyCell(cells, foodCell, ColorProvider.BackgroundColorOdd);
            }

            // coloring current food
            foreach (var foodCell in currentState.Food)
            {
                ColorOnlyCell(cells, foodCell, ColorProvider.FoodColor);
            }

            // iterating though live players
            foreach (var player in currentState.Players)
            {
                // if current player is not in the game
                if (player.Snake == null || player.Snake.Count == 0)
                    continue;

                // searching the same player in former state
                var formerPlayer = formerState.Players.First(p => p.Name.Equals(player.Name));

                // redrawing old snake to background color
                if (formerPlayer.Snake != null && formerPlayer.Snake.Count != 0)
                {
                    foreach (var cell in formerPlayer.Snake)
                    {
                        if ((cell.X + cell.Y) % 2 == 0)
                            ColorOnlyCell(cells, cell, ColorProvider.BackgroundColorEven);
                        else
                            ColorOnlyCell(cells, cell, ColorProvider.BackgroundColorOdd);
                    }
                }

                if (player.Name == myUserName)
                {
                    // redrawing MY snake
                    foreach (var cell in currentState.Snake)
                    {
                        ColorOnlyCell(cells, cell, ColorProvider.UserSnakeColor);
                    }
                }
                else
                {
                    // redrawing new snake
                    foreach (var cell in player.Snake)
                    {
                        ColorOnlyCell(cells, cell, ColorProvider.SnakeColor);
                    }
                }
            }

            return cells;
        }
    }
}
