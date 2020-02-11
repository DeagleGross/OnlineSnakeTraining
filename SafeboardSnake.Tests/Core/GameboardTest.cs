using System;
using System.Collections.Generic;
using System.Text;
using SafeboardSnake.Core.Models;
using SafeboardSnake.Tests.Models;
using Shouldly;
using Xunit;

namespace SafeboardSnake.Tests.Core
{
    public class GameboardTest
    {
        private readonly Gameboard _gameboard;

        public GameboardTest()
        {
            _gameboard = ModelGenerator.GetGeneratedGameboard();
        }

        [Fact]
        public void IsInsideGameboard_ShouldReturnTrue()
        {
            // Arrange
            var cellInside = new Cell() { X = 10, Y = 10 };

            // Act
            var isInsideBoardResult = _gameboard.IsInsideGameboard(cellInside);

            // Assert
            isInsideBoardResult.ShouldBeTrue();
        }

        [Fact]
        public void IsOutsideGameboard_ShouldReturnFalse()
        {
            // Arrange
            var cellsOutside = new List<Cell>()
            {
                new Cell() {X = -3, Y = 0},
                new Cell() {X = 0, Y = -1},
                new Cell() {X = _gameboard.Width + 3, Y = 0},
                new Cell() {X = 10, Y = _gameboard.Height + 1}
            };

            foreach (var cell in cellsOutside)
            {
                // Act
                var isInsideResult = _gameboard.IsInsideGameboard(cell);

                // Assert
                isInsideResult.ShouldBeFalse();
            }
        }
    }
}
