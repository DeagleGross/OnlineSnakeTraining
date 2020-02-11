using System;
using System.Threading;
using SafeboardSnake.Core.Engine;
using SafeboardSnake.Core.Models;

namespace SafeboardSnake.ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            // GameService gameService = new GameService();

            var gameboard = new Gameboard(20, 20);
            var snake = new Snake();
            var food = new Food();

            var cellProvider = new CellProvider(gameboard, snake);

            food.FoodCells.Add(new Cell()
            {
                X = 10, Y = 9
            });

            var k = 0;
            var direction = Direction.Top;

            while (true)
            {
                Thread.Sleep(1000);

                k++;

                if (k == 5 || k == 10 || k == 15)
                {
                    direction += 1;
                    continue;
                }

                if (k == 50)
                {
                    break;
                }

                snake.MoveInDirection(direction);

                // if game was lost we are offing timer
                // setting all properties back to  
                if (snake.IsEatenByYourself() || !gameboard.IsInsideGameboard(snake.GetHead()))
                {
                    Console.WriteLine("\n\n\n restart of game \n\n\n");

                    snake.LocateSnakeAtStart(cellProvider.GetSnakeStartingCells());

                    food.DeleteAllFood();
                    food.GenerateNewFood(cellProvider.GetNonUsedCells());
                    continue;
                }

                // and here checking if food was eating
                // -> do we need to re-generate it ???
                if (food.IsCellWithFood(snake.GetHead()))
                {
                    // add cell to tail of snake
                    snake.AddNewPiece();
                    // if found food in head of snake we delete it
                    food.DeleteFoodByCell(snake.GetHead());
                    // regenerate new food
                    food.GenerateNewFood(cellProvider.GetNonUsedCells());
                }

                Console.WriteLine($"turn #{k}:");
                Console.WriteLine(snake);
                Console.WriteLine(food);
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
