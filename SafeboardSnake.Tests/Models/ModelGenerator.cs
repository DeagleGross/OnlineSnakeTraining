using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using SafeboardSnake.Core.Engine;
using SafeboardSnake.Core.Models;
using SafeboardSnake.Tests.Consts;

namespace SafeboardSnake.Tests.Models
{
    internal class ModelGenerator
    {
        internal static Gameboard GetGeneratedGameboard(int width = Constants.GameboardWidth, int height = Constants.GameboardHeight)
        {
            return new Gameboard(width, height);
        }
    }
}
