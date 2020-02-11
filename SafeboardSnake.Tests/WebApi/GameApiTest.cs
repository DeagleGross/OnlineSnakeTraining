using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SafeboardSnake.Core.Engine;
using SafeboardSnake.Core.Models.DataTransferContracts;
using SafeboardSnake.WebApi.Controllers;
using SafeboardSnake.WebApi.ExceptionFilters;
using SafeboardSnake.WebApi.Models;
using Shouldly;
using Xunit;

namespace SafeboardSnake.Tests.WebApi
{
    public class GameApiTest
    {
        private readonly GameBoardController _gameBoardController;

        public GameApiTest()
        {
            _gameBoardController = new GameBoardController(new GameService());
        }

        [Fact]
        public void CheckGetApi_ShouldReturnOk()
        {
            var apiResult = _gameBoardController.Get();

            // Assert
            apiResult.ShouldBeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void CheckGetApi_ShouldHaveNonNullProperties()
        {
            // Arrange
            var apiResult = _gameBoardController.Get();

            // Act + asserts on not null
            var okResult = apiResult as OkObjectResult;
            okResult.ShouldNotBeNull();

            var turnDescriptor = okResult.Value as TurnDescriptor;
            turnDescriptor.ShouldNotBeNull();

            // Asserts - checking all properties not to be equal to zero or null
            // and properties-collection should contain some data
            turnDescriptor.FoodCells.ShouldNotBeEmpty();
            turnDescriptor.GameboardSize.Width.ShouldNotBe(0);
            turnDescriptor.GameboardSize.Height.ShouldNotBe(0);
            turnDescriptor.SnakeCells.Count().ShouldBeGreaterThan(1);
            turnDescriptor.TurnNumber.ShouldBeGreaterThanOrEqualTo(0);
            turnDescriptor.TurnUntilNextTurnMilliseconds.ShouldBeGreaterThan(0);
        }

        [Fact]
        public void ChangeDirectionPassNull_ShouldGetBadRequestResult()
        {
            // Arrange
            DirectionDescriptor directionDescriptor = null;

            // Act
            var result = _gameBoardController.ChangeDirection(directionDescriptor);

            // Assert
            // no need to check direction not be changed
            // - it is obvious in method direction wouldn't be changed
            result.ShouldBeOfType(typeof(BadRequestResult));
        }

        [Fact]
        public void ChangeDirectionPassNullString_ShouldGetBadRequestResult()
        {
            // Arrange
            var directionDescriptor = new DirectionDescriptor { Direction = null };

            // Act
            var result = _gameBoardController.ChangeDirection(directionDescriptor);

            // Assert
            result.ShouldBeOfType(typeof(BadRequestResult));
        }

        [Fact]
        public void ChangeDirectionPassNonExistingDirection_ShouldThrowArgumentException()
        {
            // I'm trying to exception filters as a relatively new method of
            // handling exceptions in ASP.NET applications.
            //
            // If you check through postman
            // result of changing direction to non existing direction
            // will be ok and server will not stop.
            //
            // However if you launch web-api project using debug mode,
            // visual studio will stop you in ChangeDirection method
            // of GameService class.
            //
            // It is just an exception that is caught by visual studio debugger before
            // exception-filter is applied (= OnExceptionAsync() is called)
            //
            // If I try to write this code in test:
            /*
                var directionDescriptor = new DirectionDescriptor
                {
                    Direction = "NonExistingDirection - I don't really know what this direction is"
                };

                var result = _gameBoardController.ChangeDirection(directionDescriptor);

                result.ShouldBeOfType<BadRequestResult>();
            */
            // the same will work in unit-testing project: exception will be caught by vs
            // before exception filter is applied.
            // So test will not succeed in running.
            //
            // I've been studying this problem, but didn't find any great solution.
            // So I'm not testing api call as it will work on server. I'm testing
            // only the fact that exception of desired type (ArgumentException) will be thrown.
            //
            // So in this test-class I decided to test
            // that filter of type is applied to Post method 'ChangeDirection'
            //      Test Case: CheckChangeDirectionHasExceptionFilterApplied()

            // Arrange
            var directionDescriptor = new DirectionDescriptor
            {
                Direction = "NonExistingDirection - I don't really know what this direction is"
            };

            Action postRequestChangeDirection = () =>
            {
                _gameBoardController.ChangeDirection(directionDescriptor);
            };
            
            // Assert
            postRequestChangeDirection.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void ChangeDirectionPassValidDirection_ShouldReturnOk()
        {
            // Arrange
            var directionDescriptor = new DirectionDescriptor
            {
                Direction = "Top"
            };

            // Act
            var result = _gameBoardController.ChangeDirection(directionDescriptor);

            // Assert
            result.ShouldBeOfType<OkResult>();
        }

        [Fact]
        public void CheckChangeDirectionHasExceptionFilterApplied()
        {
            // Using reflection to find out
            // does POST method contain filter for argumentException

            var controllerType = typeof(GameBoardController);
            var changeDirectionInfo = controllerType.GetMethod("ChangeDirection");
            var match = changeDirectionInfo.GetCustomAttributes(
                typeof(ArgumentExceptionFilterAttribute), false);
            
            match.Length.ShouldBeGreaterThan(0);
        }
    }
}
