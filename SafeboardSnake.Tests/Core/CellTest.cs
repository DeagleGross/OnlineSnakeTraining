using System;
using System.Collections.Generic;
using System.Text;
using SafeboardSnake.Core.Models;
using Shouldly;
using Xunit;

namespace SafeboardSnake.Tests.Core
{
    public class CellTest
    {
        private readonly Cell _zeroCell;

        public CellTest()
        {
            _zeroCell = new Cell();
        }

        [Fact]
        public void CheckCellEquals()
        {
            // Arrange
            var cell = new Cell() { X = 10, Y = 10 };
            var otherCell = new Cell() { X = 10, Y = 10 };

            // Act + Assert
            cell.Equals(otherCell).ShouldBeTrue();
        }

        [Fact]
        public void CheckCellPlusOperator()
        {
            // Arrange
            var cell = new Cell() { X = 5, Y = 15 };
            var otherCell = new Cell() { X = 10, Y = 28 };

            // Act
            var sumCell = cell + otherCell;

            // Assert
            sumCell.X.ShouldBe(cell.X + otherCell.X);
            sumCell.Y.ShouldBe(cell.Y + otherCell.Y);
        }

        [Fact]
        public void CheckCellMinusOperator()
        {
            // Arrange
            var cell = new Cell() { X = 5, Y = 15 };
            var otherCell = new Cell() { X = 10, Y = 28 };

            // Act
            var sumCell = cell - otherCell;

            // Assert
            sumCell.X.ShouldBe(cell.X - otherCell.X);
            sumCell.Y.ShouldBe(cell.Y - otherCell.Y);
        }

        [Fact]
        public void CheckGetTopCell()
        {
            // Arrange
            var cell = new Cell() { Y = -1 };

            // Act
            var topCell = Cell.GetTopCell(_zeroCell);

            // Assert
            topCell.Equals(cell).ShouldBeTrue();
        }

        [Fact]
        public void CheckGetRightCell()
        {
            // Arrange
            var cell = new Cell() { X = 1 };

            // Act
            var rightCell = Cell.GetRightCell(_zeroCell);

            // Assert
            rightCell.Equals(cell).ShouldBeTrue();
        }

        [Fact]
        public void CheckGetDownCell()
        {
            // Arrange
            var cell = new Cell() { Y = 1 };

            // Act
            var downCell = Cell.GetDownCell(_zeroCell);

            // Assert
            downCell.Equals(cell).ShouldBeTrue();
        }

        [Fact]
        public void CheckGetLeftCell()
        {
            // Arrange
            var cell = new Cell() { X = -1 };

            // Act
            var leftCell = Cell.GetLeftCell(_zeroCell);

            // Assert
            leftCell.Equals(cell).ShouldBeTrue();
        }
    }
}
