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

        public SnakeGame(Snake snake)
        {
            this.snake = snake;
        }

        public void Start()
        {
            while (true)
            {
                snake.DrawSnake();
            }
        }
    }
}
