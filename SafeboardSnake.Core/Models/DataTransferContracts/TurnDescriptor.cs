using System.Collections.Generic;
using Newtonsoft.Json;

namespace SafeboardSnake.Core.Models.DataTransferContracts
{
    /// <summary>
    /// Defines data transfer contract for game-state
    /// </summary>
    public class TurnDescriptor
    {
        [JsonProperty("turnNumber")]
        public int TurnNumber { get; set; }

        [JsonProperty("turnUntilNextTurnMilliseconds")]
        public int TurnUntilNextTurnMilliseconds { get; set; }

        [JsonProperty("gameBoardSize")]
        public Gameboard GameboardSize { get; set; }

        [JsonProperty("snake")]
        public IEnumerable<Cell> SnakeCells { get; set; }

        [JsonProperty("food")]
        public IEnumerable<Cell> FoodCells { get; set; }
    }
}
