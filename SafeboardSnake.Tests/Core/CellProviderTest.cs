using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafeboardSnake.Core.Const;
using SafeboardSnake.Core.Engine;
using SafeboardSnake.Core.Models;
using SafeboardSnake.Tests.Models;
using Shouldly;
using Xunit;

namespace SafeboardSnake.Tests.Core
{
    public class CellProviderTest
    {
        private readonly CellProvider _cellProvider;
        private readonly Snake _snake;
        private readonly Gameboard _gameboard;

        public CellProviderTest()
        {
            _gameboard = ModelGenerator.GetGeneratedGameboard();
            _snake = new Snake();
            _cellProvider = new CellProvider(_gameboard, _snake);
        }

        [Fact]
        public void GetNonUsedCells_ShouldReturn2UsedCells()
        {
            // Arrange
            _snake.LocateSnakeAtStart(_cellProvider.GetSnakeStartingCells());

            // Act
            var nonUsedCells = _cellProvider.GetNonUsedCells();

            // Assert
            nonUsedCells.Count().ShouldBe(
                _gameboard.Width * _gameboard.Height - _snake.SnakePieces.Count);
        }

        [Fact]
        public void LocateSnakeAtStart_ShouldSetSnakeCellsToTwoPieces()
        {
            // Arrange
            _snake.LocateSnakeAtStart(_cellProvider.GetSnakeStartingCells());

            // Act
            _snake.LocateSnakeAtStart(_cellProvider.GetSnakeStartingCells());

            // Assert
            _snake.SnakePieces.Count().ShouldBe(Constants.InitialLengthOfSnake);
        }
    }
}
