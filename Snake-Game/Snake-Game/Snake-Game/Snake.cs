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

        public void Move(Position direction)
        {
            var oldHead = snakeElements.Last();
            var newHead = new Position(oldHead.row + direction.row, oldHead.col + direction.col);
            snakeElements.Enqueue(newHead);
            RemoveElement();

        }

        public void DrawSnake()
        {
            foreach (var snakeElelemnt in snakeElements)
            {
                Draw(snakeElelemnt, '*');
            }
        }

        private void RemoveElement()
        {
            var elemntToRemove = snakeElements.Dequeue();
            Draw(elemntToRemove, ' ');
        }
    }
}
