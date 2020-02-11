using SafeboardSnake.Core.Models;

// ReSharper disable once CheckNamespace
namespace SafeboardSnake.Core.Const
{
    public class Constants
    {
        /// <summary>
        /// Milliseconds amount that last between two reloads of timer on server-side
        /// </summary>
        internal const int TurnUntilNextTurnMilliseconds = 250;

        /// <summary>
        /// Default width of gameboard
        /// </summary>
        internal const int DefaultGameboardWidth = 20;

        /// <summary>
        /// Default height of gameboard
        /// </summary>
        internal const int DefaultGameboardHeight = 20;

        /// <summary>
        /// Default direction of snake at start of game
        /// </summary>
        internal const Direction DefaultDirection = Direction.Top;

        /// <summary>
        /// Number of turn from which game is starting (or how many movements snake has already done)
        /// </summary>
        internal const int StartingTurnNumber = 0;

        /// <summary>
        /// Absolute difference in values for direction-enum-constants that are opposite (down-top and right-left)
        /// </summary>
        internal const int OppositeDirectionsDifference = 2;

        /// <summary>
        /// Amount of cells that snake contains at start of game
        /// </summary>
        public const int InitialLengthOfSnake = 2;
    }
}
