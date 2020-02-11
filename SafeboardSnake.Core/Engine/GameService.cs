using System;
using System.Timers;
using SafeboardSnake.Core.Const;
using SafeboardSnake.Core.Models;
using SafeboardSnake.Core.Models.DataTransferContracts;

namespace SafeboardSnake.Core.Engine
{
    /// <summary>
    /// Defines game logic
    /// </summary>
    public class GameService
    {
        /// <summary>
        /// Class for accessing different cells of gameboard
        /// </summary>
        private readonly CellProvider _cellProvider;

        /// <summary>
        /// Current direction of snake in the game
        /// </summary>
        private Direction _currentDirection;

        /// <summary>
        /// Snake object with all cells-pieces of snake at current moment
        /// </summary>
        private readonly Snake _snake = new Snake();

        /// <summary>
        /// Turn number of current game
        /// </summary>
        private int _turnNumber;

        /// <summary>
        /// Used for moving and reloading snake every turnUntilNextTurnMilliseconds millis
        /// </summary>
        private readonly Timer _timer;

        /// <summary>
        /// Zone consisting of cells where snake is moving
        /// </summary>
        private readonly Gameboard _gameboard 
            = new Gameboard(Constants.DefaultGameboardWidth, Constants.DefaultGameboardHeight);

        /// <summary>
        /// Food on gameboard for eating
        /// </summary>
        private readonly Food _food = new Food();

        /// <summary>
        /// Constructor defining all inner-properties and starting server timer -> game begins here.
        /// Also initializes snake here by gameboard parameters
        /// </summary>
        public GameService()
        { 
            _cellProvider = new CellProvider(_gameboard, _snake);

            SetupPropertiesForStartOfGame();

            _timer = new Timer(Constants.TurnUntilNextTurnMilliseconds);
            _timer.Elapsed += ReloadEvent;
            _timer.Start();
        }

        /// <summary>
        /// Sets properties to initial conditions. Gameboard is always the same - so it is initialized before.
        /// </summary>
        private void SetupPropertiesForStartOfGame()
        {
            _turnNumber = 0;
            _currentDirection = Direction.Top;

            _snake.LocateSnakeAtStart(_cellProvider.GetSnakeStartingCells());
            
            _food.DeleteAllFood();
            _food.GenerateNewFood(_cellProvider.GetNonUsedCells());
        }

        /// <summary>
        /// Main method where timer-reload event is happening
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void ReloadEvent(Object source, ElapsedEventArgs e)
        {
            try
            {
                _turnNumber++;
                _snake.MoveInDirection(_currentDirection);

                // if game was lost we are offing timer
                // setting all properties back to  
                if (_snake.IsEatenByYourself() || !_gameboard.IsInsideGameboard(_snake.GetHead()))
                {
                    SetupPropertiesForStartOfGame();
                    return;
                }

                // and here checking if food was eaten
                // we need to re-generate it
                if (_food.IsCellWithFood(_snake.GetHead()))
                {
                    // add cell to tail of snake
                    _snake.AddNewPiece();
                    // if found food in head of snake we delete it
                    _food.DeleteFoodByCell(_snake.GetHead());
                    // regenerate new food
                    _food.GenerateNewFood(_cellProvider.GetNonUsedCells());
                }
            }
            finally
            {
                _timer.Start();
            }
        }

        /// <summary>
        /// Receives object with direction value and tries to parse it
        /// and then equal it to CurrentDirection of snake in the game
        /// </summary>
        public void ChangeDirection(string directionString)
        {
            var direction = (Direction) Enum.Parse(typeof(Direction), directionString);

            // not letting direction to be reinitialized as opposite value (left -> right and other combinations)
            if (Math.Abs(_currentDirection - direction) != Constants.OppositeDirectionsDifference)
            {
                _currentDirection = direction;
            }
        }

        /// <summary>
        /// Forms a current moment turn descriptor object, that provides all information about
        /// state of game (snake, food, gameboard, turnNum and timer.interval) in a format
        /// that was provided by safeboard organizers
        /// </summary>
        /// <returns></returns>
        public TurnDescriptor GetCurrentTurn()
        {
            return new TurnDescriptor()
            {
                FoodCells = _food.FoodCells,
                GameboardSize = _gameboard,
                SnakeCells = _snake.SnakePieces,
                TurnNumber = _turnNumber,
                TurnUntilNextTurnMilliseconds = (int)_timer.Interval
            };
        }
    }
}
