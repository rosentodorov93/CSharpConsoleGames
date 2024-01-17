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
        private int score;
        private double speedLevel;

        public SnakeGame(Snake snake, InputManager inputManager, Food food)
        {
            this.snake = snake;
            this.inputManager = inputManager;
            this.food = food;
            this.score = 0;
            this.speedLevel = 110;
        }

        public void Start()
        {
            Console.CursorVisible = false;
            Console.BufferHeight = Console.WindowHeight;

            while (true)
            {
                var direction = inputManager.GetDirection();

                var currentHeadPosition = snake.GetHead(direction);

                var isPositionSafe = CheckBoudries(currentHeadPosition);
                if (!isPositionSafe || snake.SnakeElelemnts.Contains(currentHeadPosition))
                {
                    GameOver();
                    return;
                }
                else
                {
                    snake.Move(currentHeadPosition);
                }

                var isOnFood = snake.FeedCheck(food.Position.row, food.Position.col);
                if (isOnFood)
                {
                    food.GenerateFood(snake.SnakeElelemnts);
                    score++;
                    speedLevel -= 1;
                }
                else
                {
                    snake.RemoveElement();
                    speedLevel -= 0.1;
                }
                food.DrawFood();
                snake.DrawSnake();
                Thread.Sleep((int)this.speedLevel);
            }
        }

        private bool CheckBoudries(Position head)
        {
            if (head.row < 0 || head.col < 0 || head.col >= Console.WindowHeight || head.row >= Console.WindowWidth)
            {
                return false;
            }

            return true;
        }

        private void GameOver()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Game Over!");
            Console.WriteLine($"Your score is {this.score * 100}");
        }
    }
}
