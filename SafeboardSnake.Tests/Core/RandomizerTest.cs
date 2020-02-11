using System;
using System.Collections.Generic;
using System.Text;
using SafeboardSnake.Core.Models;
using SafeboardSnake.Tests.Models;
using Shouldly;
using Xunit;

namespace SafeboardSnake.Tests.Core
{
    public class RandomizerTest
    {
        [Fact]
        public void GenerateCell_ShouldGenerateCellInsideGameboard()
        {
            var gameboard = ModelGenerator.GetGeneratedGameboard();

            // maybe it is a bad approach
            // to test random generation just by multiple testing
            // so error can still occur.
            // But inner-method-calls are already tested, so at least we know
            // that everything inside works fine.
            // So this test is just an additional check that sometimes can help us.

            for (int i = 0; i < 100; i++)
            {
                // Assert
                gameboard.IsInsideGameboard(
                    Randomizer.GenerateCell(gameboard)
                    ).ShouldBeTrue();
            }
        }
    }
}
