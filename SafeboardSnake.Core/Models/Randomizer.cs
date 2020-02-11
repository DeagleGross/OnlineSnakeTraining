using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafeboardSnake.Core.Models;

namespace SafeboardSnake.Core.Models
{
    /// <summary>
    /// Used for generating some instances of models
    /// </summary>
    public static class Randomizer
    {
        private static readonly Random Random = new Random();

        /// <summary>
        /// Generates a cell inside gameboard (just generates integer coordinates and forms a cell from it)
        /// </summary>
        /// <param name="gameboard"></param>
        /// <returns></returns>
        public static Cell GenerateCell(Gameboard gameboard)
        {
            var x = Random.Next(0, gameboard.Width);
            var y = Random.Next(0, gameboard.Height);

            return new Cell() { X = x, Y = y };
        }

        /// <summary>
        /// Returns integer value in interval [min;max)
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GetIntegerValue(int min, int max)
        {
            return Random.Next(min, max);
        }
    }
}
