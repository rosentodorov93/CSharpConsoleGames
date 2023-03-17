namespace Tetris
{
    public class TetrisConsoleInputManager : IInputManager
    {
        public TetrisInput GetInput()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.Escape)
                {
                    return TetrisInput.None;
                }
                if (key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow)
                {
                    return TetrisInput.Right;

                }
                if (key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow)
                {
                    return TetrisInput.Left;

                }
                if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow)
                {
                    return TetrisInput.Down;
                }
                if (key.Key == ConsoleKey.Spacebar)
                {
                    return TetrisInput.Rotate;
                }
            }

            return TetrisInput.None;
        }
    }
}
