using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SafeboardSnake.Core.Engine;
using SafeboardSnake.Core.Models;
using SafeboardSnake.Tests.Consts;
using Shouldly;
using Xunit;

namespace SafeboardSnake.Tests.Core
{
    public class GameServiceTest
    {
        private readonly GameService _gameService;

        public GameServiceTest()
        {
            _gameService = new GameService();
        }

        /// <summary>
        /// Returns direction that is stored in field of gameService
        /// </summary>
        /// <returns></returns>
        private Direction GetInnerGameServiceDirection()
        {
            var bindFlags = BindingFlags.Instance | 
                                     BindingFlags.Public | 
                                     BindingFlags.NonPublic | 
                                     BindingFlags.Static;

            var fieldInfo = _gameService.GetType().GetField(Constants.CurrentDirectionFieldName, bindFlags);

            if (fieldInfo == null)
            {
                throw new FieldAccessException($"{Constants.CurrentDirectionFieldName} couldn't be found in gameService object");
            }

            return (Direction)fieldInfo.GetValue(_gameService);
        }

        [Fact]
        public void ChangeDirection_ShouldThrowException()
        {
            // Arrange
            var directionName = "Kyda-to tyda!";

            // Act
            Action changeDirection = () =>
            {
                _gameService.ChangeDirection(directionName);
            };

            // Assert
            changeDirection.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void ChangeDirection_ShouldChangeCorrectly()
        {
            // Act
            _gameService.ChangeDirection("Top");

            // there is no other way to get direction -> it is private
            // so using reflection is only way to reach it.
            // Let me explain my (maybe) strange choice below:

            // I think reflection is a super narrowly applied method of doing something,
            // but to use it in unit testing is ok in this case.

            // As for me, i see no reasons in testing other methods of gameService
            // (i.e. 'ReloadEvent' and 'SetupPropertiesForStartOfGame')
            // because they ensure shared game logic. It could be checked
            // in other type of testing

            // and we really need to understand we are using valid method
            // for redefining direction. So it is reasonable to test changingOfDirection!
            // I've could create a controller that converts string
            // value to direction and returns it, so i can easily access it.
            // But do we really need to create another class with static method if our goal
            // is just to parse string to enum ???

            // another approach is to create public property but if in task
            // we are not letting user know about direction -> why do we need to
            // make direction field extraordinary and the only one that has
            // public property
            var direction = GetInnerGameServiceDirection();

            // assert
            direction.ShouldBe(Direction.Top);
        }

        [Fact]
        public void ChangeDirectionToOpposite_ShouldNotChange()
        {
            // Top - Down testing
            _gameService.ChangeDirection("Down");
            var direction = GetInnerGameServiceDirection();
            direction.ShouldBe(Direction.Top);

            // Left - Right testing
            _gameService.ChangeDirection("Left");
            direction = GetInnerGameServiceDirection();
            direction.ShouldBe(Direction.Left);

            _gameService.ChangeDirection("Right");
            direction = GetInnerGameServiceDirection();
            direction.ShouldBe(Direction.Left);
        }
    }
}
