using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    public class Snake : Drawable
    {
        private Queue<Position> snakeElements;

        public Snake()
        {
            snakeElements = new Queue<Position>();
            GetInitialSnake();
        }

        private void GetInitialSnake()
        {
            snakeElements.Enqueue(new Position(0, 0));
            snakeElements.Enqueue(new Position(1, 0));
            snakeElements.Enqueue(new Position(2, 0));
        }

        public void Move(Position newHead, Position food)
        {
            snakeElements.Enqueue(newHead);

        }

        public void DrawSnake()
        {
            foreach (var snakeElelemnt in snakeElements)
            {
                Draw(snakeElelemnt, '*');
            }
        }
    }
}
