namespace Snake_Game
{
    public class InputManager
    {
        private Position currentDirection;
        public InputManager()
        {
            this.currentDirection = new Position(0, 1);
        }
        public Position GetDirection()
        {
            if (Console.KeyAvailable)
            {
                var input = Console.ReadKey();

                if (input.Key == ConsoleKey.UpArrow && currentDirection.col != 1)
                {
                    currentDirection = new Position(0, -1);
                }
                if (input.Key == ConsoleKey.DownArrow && currentDirection.col != -1)
                {
                    currentDirection = new Position(0, 1);
                }
                if (input.Key == ConsoleKey.RightArrow && currentDirection.row != -1)
                {
                    currentDirection = new Position(1, 0);
                }
                if (input.Key == ConsoleKey.LeftArrow && currentDirection.row != 1)
                {
                    currentDirection = new Position(-1, 0);
                }
            }

            return currentDirection;

        }
    }
}
