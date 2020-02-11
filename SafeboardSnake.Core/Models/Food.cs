using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafeboardSnake.Core.Models
{
    public class Food
    {
        /// <summary>
        /// Collection of cells where food is located
        /// </summary>
        public List<Cell> FoodCells { get; set; }
            = new List<Cell>();

        /// <summary>
        /// Adds new food in food-cells list. Food is inside a gameboard
        /// and food is not generated inside a snake.
        /// </summary>
        public void GenerateNewFood(IEnumerable<Cell> cells, int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                FoodCells.Add(
                    cells.ElementAt(
                        Randomizer.GetIntegerValue(0, cells.Count()))
                    );
            }
        }

        /// <summary>
        /// Removes all cells from foodCells collection
        /// </summary>
        public void DeleteAllFood()
        {
            FoodCells.Clear();
        }

        /// <summary>
        /// Removes a food from a cell
        /// </summary>
        /// <param name="cell"></param>
        public void DeleteFoodByCell(Cell cell)
        {
            FoodCells.Remove(cell);
        }

        /// <summary>
        /// Returns true if cell contains food
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public bool IsCellWithFood(Cell cell)
        {
            foreach (var foodCell in FoodCells)
            {
                if (cell.Equals(foodCell))
                {
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder("Food: ");

            foreach (var cell in FoodCells)
            {
                strBuilder.Append(cell + " ");
            }

            return strBuilder.ToString();
        }
    }
}
