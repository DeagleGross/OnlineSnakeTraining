using System;
using System.Collections.Generic;
using System.Text;
using SafeboardSnake.Core.Models;

namespace SafeboardSnake.WPF.Models
{
    public class GameState
    {
        public bool IsStarted { get; set; }
        public bool IsPaused { get; set; }
        public int RoundNumber { get; set; }
        public int TurnNumber { get; set; }
        public int TurnTimeMilliseconds { get; set; }
        public int TimeUntilNextTurnMilliseconds { get; set; }
        public Gameboard GameBoardSize { get; set; }
        public int MaxFood { get; set; }
        public ICollection<Player> Players { get; set; }
        public ICollection<Cell> Snake { get; set; }
        public ICollection<Cell> Food { get; set; }
        public ICollection<Wall> Walls { get; set; }
    }
}
