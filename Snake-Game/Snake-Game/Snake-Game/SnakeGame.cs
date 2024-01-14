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

        public SnakeGame(Snake snake, InputManager inputManager)
        {
            this.snake = snake;
            this.inputManager = inputManager;
        }

        public void Start()
        {
            while (true)
            {
                Console.CursorVisible = false;
                Console.BufferHeight = Console.WindowHeight;

                var direction = inputManager.GetDirection();
                snake.Move(direction);
                snake.DrawSnake();
                Thread.Sleep(100);
            }
        }
    }
}
