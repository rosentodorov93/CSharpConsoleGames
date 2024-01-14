namespace Snake_Game
{
    public class InputManager
    {
        private Position defaultDirection;
        private Position? currentDirection;
        public InputManager()
        {
            this.defaultDirection = new Position(0, 1);
        }
        public Position GetDirection()
        {
            if (Console.KeyAvailable)
            {
                var input = Console.ReadKey();

                if (input.Key == ConsoleKey.UpArrow)
                {
                    currentDirection = new Position(0, -1);
                }
                if (input.Key == ConsoleKey.DownArrow)
                {
                    currentDirection = new Position(0, 1);
                }
                if (input.Key == ConsoleKey.RightArrow)
                {
                    currentDirection = new Position(1, 0);
                }
                if (input.Key == ConsoleKey.LeftArrow)
                {
                    currentDirection = new Position(-1, 0);
                }
            }

            if (currentDirection == null)
            {
                return defaultDirection;
            }
            else
            {
                return currentDirection;
            }
        }
    }
}
