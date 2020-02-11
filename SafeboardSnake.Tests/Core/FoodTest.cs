using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafeboardSnake.Core.Engine;
using SafeboardSnake.Core.Models;
using SafeboardSnake.Tests.Models;
using Shouldly;
using Xunit;

namespace SafeboardSnake.Tests.Core
{
    public class FoodTest
    {
        private readonly Food _food;
        private readonly Gameboard _gameboard;
        private readonly Snake _snake;
        private readonly CellProvider _cellProvider;

        public FoodTest()
        {
            _gameboard = ModelGenerator.GetGeneratedGameboard();
            _snake = new Snake();
            _food = new Food();

            _cellProvider = new CellProvider(_gameboard, _snake);
        }

        [Fact]
        public void CheckFoodGeneration()
        {
            // Act
            _food.GenerateNewFood(_cellProvider.GetNonUsedCells());
            var foodCellGenerated = _food.FoodCells.First();

            // Assert
                // food has to be saved in list
            _food.FoodCells.Count.ShouldBe(1);
                // food has to be inside board
            _gameboard.IsInsideGameboard(foodCellGenerated).ShouldBeTrue();

                // no snake cells could be in the same place as food
            foreach (var snakeCell in _snake.SnakePieces)
            {
                snakeCell.Equals(foodCellGenerated).ShouldBeFalse();
            }
        }

        [Fact]
        public void IsCellWithFood_ShouldCheckFoodPlacement()
        {
            // Arrange
            _food.FoodCells.Add(new Cell() { X = 12, Y = 10 });
            var cellWithFood = new Cell() { X = 12, Y = 10 };
            var cellWithoutFood = new Cell() { X = 10, Y = 10 };

            // Act + Assert
            _food.IsCellWithFood(cellWithFood).ShouldBeTrue();
            _food.IsCellWithFood(cellWithoutFood).ShouldBeFalse();
        }

        [Fact]
        public void DeleteFoodByCell_ShouldDeleteCell()
        {
            // Arrange
            var cell = new Cell() { X = 1, Y = 1 };
            _food.FoodCells.Add(cell);
            _food.FoodCells.ShouldContain(cell);

            // Act
            _food.DeleteFoodByCell(cell);

            // Assert
            _food.FoodCells.ShouldNotContain(cell);
        }
    }
}
