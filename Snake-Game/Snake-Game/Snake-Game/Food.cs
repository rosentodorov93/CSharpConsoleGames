using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    public class Food : Drawable
    {
        private Random random;
        private Position? position;


        public Food()
        {
            random = new Random();
            this.Position = new Position(5, 5);
        }
        public Position? Position { get => position; private set => position = value; }

        public void GenerateFood(Queue<Position> snake)
        {
            do
            {
                position.col = random.Next(0, Console.WindowHeight);
                position.row = random.Next(0, Console.WindowWidth);

            } while (snake.Contains(position));

        }

        public void DrawFood()
        {
            Draw(position, '@', ConsoleColor.White);
        }
    }
}
