using System;
using SafeboardSnake.Core.Engine;
using SafeboardSnake.Core.Models;
using SafeboardSnake.Tests.Models;
using Shouldly;
using Xunit;

namespace SafeboardSnake.Tests.Core
{
    public class SnakeTest
    {
        private readonly Snake _snake;
        private readonly CellProvider _cellProvider;

        // called before every test
        public SnakeTest()
        {
            var gameboard = ModelGenerator.GetGeneratedGameboard();
            _snake = new Snake();

            _cellProvider = new CellProvider(gameboard, _snake);
        }

        [Fact]
        public void InitSnake_ShouldContainNoCells()
        {
            // Assert
            _snake.SnakePieces.ShouldNotBeNull();
            _snake.SnakePieces.Count.ShouldBe(0);
        }

        [Fact]
        public void SetSnakeAtStart_ShouldMakeSnakeCells()
        {
            // Act
            _snake.LocateSnakeAtStart(_cellProvider.GetSnakeStartingCells());

            // Assert
            _snake.SnakePieces.ShouldNotBeNull();
            _snake.SnakePieces.Count.ShouldBe(2);
        }

        [Fact]
        public void MoveSnakeToDefinedDirection_ShouldChangeCoordinates()
        {
            // Arrange
            var direction = Direction.Top;
            var formerSnake = new Snake();

            _snake.LocateSnakeAtStart(_cellProvider.GetSnakeStartingCells());
            formerSnake.LocateSnakeAtStart(_cellProvider.GetSnakeStartingCells());

            // Act 
            _snake.MoveInDirection(direction);

            // Assert
                // snake amount of pieces should be the same
            _snake.SnakePieces.Count.ShouldBe(formerSnake.SnakePieces.Count);

                // snake is small so comparing head and tail movement
            Math.Abs(_snake.GetHead().Y - formerSnake.GetHead().Y).ShouldBe(1);
            _snake.SnakePieces.Last.Value.Equals(formerSnake.GetHead()).ShouldBeTrue();
        }

        [Fact]
        public void MoveSnakeInUndefinedDirection_ShoutThrowArgumentException()
        {
            // Arrange
            _snake.LocateSnakeAtStart(_cellProvider.GetSnakeStartingCells());
            var direction = Direction.Left;
            direction++;

            // Act
            Action moveSnakeInUndefinedDirection = () =>
            {
                _snake.MoveInDirection(direction);
            };

            // Assert
            moveSnakeInUndefinedDirection.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void AddNewCellToSnake_ShouldLengthenTail()
        {
            // test is done according to rule
            // that snake is at first pointing to the top
            // and consists of 2 cells initially

            // Arrange
            _snake.LocateSnakeAtStart(_cellProvider.GetSnakeStartingCells());
            var cellCount = _snake.SnakePieces.Count;
            var tailCell = _snake.SnakePieces.Last.Value;

            var expectedAddedCell = tailCell + new Cell() { X = 0, Y = 1 };

            // Act
            _snake.AddNewPiece();

            // Assert
            _snake.SnakePieces.Count.ShouldBe(cellCount + 1);
            _snake.SnakePieces.Last.Value.Equals(expectedAddedCell).ShouldBeTrue();
        }

        [Fact]
        public void IsSnakeEaten_ShouldReturnTrue()
        {
            // Assert init - at initial state snake shouldn't be eaten
            _snake.LocateSnakeAtStart(_cellProvider.GetSnakeStartingCells());
            _snake.IsEatenByYourself().ShouldBeFalse();

            // Arrange
                // breaking snake and adding cell in the end
                // that is located in the head of snake
            _snake.SnakePieces.AddLast(_snake.GetHead());

            // Act
            var isEatenResult = _snake.IsEatenByYourself();

            // Assert
            isEatenResult.ShouldBeTrue();
        }
    }
}
