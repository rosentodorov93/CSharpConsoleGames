using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    public class SnakeGame
    {
        private Snake snake;
        private InputManager inputManager;
        private Food food;

        public SnakeGame(Snake snake, InputManager inputManager, Food food)
        {
            this.snake = snake;
            this.inputManager = inputManager;
            this.food = food;
        }

        public void Start()
        {
            Console.CursorVisible = false;
            Console.BufferHeight = Console.WindowHeight;

            while (true)
            {
                var direction = inputManager.GetDirection();

                snake.Move(direction);
                var isOnFood = snake.FeedCheck(food.Position.row, food.Position.col);
                if (isOnFood)
                {
                    food.GenerateFood(snake.SnakeElelemnts);
                }
                else
                {
                    snake.RemoveElement();
                }
                food.DrawFood();
                snake.DrawSnake();
                Thread.Sleep(120);
            }
        }
    }
}
