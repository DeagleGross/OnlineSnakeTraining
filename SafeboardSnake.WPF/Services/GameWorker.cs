using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Media;
using System.Windows.Threading;
using SafeboardSnake.Core.Helpers;
using SafeboardSnake.Core.Models;
using SafeboardSnake.Core.Models.DataTransferContracts;
using SafeboardSnake.WPF.Constants;
using SafeboardSnake.WPF.Models;

namespace SafeboardSnake.WPF.Services
{
    class GameWorker
    {
        private readonly CellRecounter _cellRecounter = new CellRecounter();
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        
        private GameState _gameState;
        private GameState _formerGameState;

        private int _clientRoundNumber;
        private int _clientTurnNumber;

        private string _userName;

        public GameViewModel GameViewModel { get; set; }

        public GameWorker()
        {
            _timer.Tick += ReloadEvent;
        }

        /// <summary>
        /// Inner-app timer that requests server state in periodic time
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private async void ReloadEvent(object source, EventArgs e)
        {
            try
            {
                _timer.Stop();

                // getting response from server
                // and trying to understand - do we need really to redraw GUI ???
                _gameState = await App.RestService.GetGameState();

                // doing nothing if next turn is not reached
                if (_gameState.TurnNumber <= _clientTurnNumber)
                {
                    return;
                }

                // new round has started. We need to redraw background with walls
                if (_gameState.RoundNumber > _clientRoundNumber)
                {
                    _clientRoundNumber = _gameState.RoundNumber;
                    _clientTurnNumber = _gameState.TurnNumber;

                    ReformDataToViewModel();

                    _formerGameState = _gameState;
                    return;
                }

                // synchronization with server
                _clientRoundNumber = _gameState.RoundNumber;
                _clientTurnNumber = _gameState.TurnNumber;

                ReformDataToViewModel(false);

                _formerGameState = _gameState;
            }
            finally
            {
                _timer.Start();
            }
        }

        /// <summary>
        /// Fills view-model with data that will be shown
        /// </summary>
        private void ReformDataToViewModel(bool fullRedraw = true)
        {
            int boardWidth = _gameState.GameBoardSize.Width;
            int boardHeight = _gameState.GameBoardSize.Height;

            var playersWithExistingSnake = _gameState.Players.Where(p => !p.Snake.IsNullOrEmpty()).ToList();

            var playerScoreboardInfo = new StringBuilder();
            foreach (var player in playersWithExistingSnake)
            {
                playerScoreboardInfo.AppendLine($"{player.Name}: {player.Snake.Count}");
            }

            if (fullRedraw)
            {
                GameViewModel.Cells = _cellRecounter.DrawBoardCells(
                    boardWidth: boardWidth,
                    boardHeight: boardHeight,
                    playerSnakes: playersWithExistingSnake,
                    foodCells: _gameState.Food,
                    walls: _gameState.Walls,
                    userSnake: _gameState.Snake);
            }
            else
            {
                GameViewModel.Cells = _cellRecounter.RedrawBoardCells(
                    formerState: _formerGameState,
                    currentState: _gameState,
                    GameViewModel.Cells,
                    myUserName: _userName);

                GameViewModel.CallCellsChanged();
            }


            GameViewModel.Rows = boardWidth;
            GameViewModel.Columns = boardHeight;
            GameViewModel.PlayerScoreboard = playerScoreboardInfo.ToString();

            GameViewModel.TurnNumber = _gameState.TurnNumber;
            GameViewModel.RoundNumber = _gameState.RoundNumber;
            GameViewModel.TimeTillNextTurn = _gameState.TimeUntilNextTurnMilliseconds;
        }

        /// <summary>
        /// Prepares all information about game and starts inner-timer of game
        /// </summary>
        public async Task LaunchGame()
        {
            var userResponse = await App.RestService.GetUserName();
            _gameState = await App.RestService.GetGameState();

            if (userResponse == null || _gameState == null)
            {
                GameViewModel.AdditionalInfo = Consts.ErrorMessage;
                return;
            }

            _formerGameState = _gameState;

            // setting information that is super important at the first request
            _userName = userResponse.Name;
            GameViewModel.UserName = _userName;
            _clientRoundNumber = _gameState.RoundNumber;
            _clientTurnNumber = _gameState.TurnNumber;

            // loading GUI
            ReformDataToViewModel();

            // in the end of preparation launching timer
            _timer.Interval = new TimeSpan(0, 0, 0, 0, (int)(_gameState.TimeUntilNextTurnMilliseconds * 0.17));
            _timer.Start();
        }
    }
}
