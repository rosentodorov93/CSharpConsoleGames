namespace Snake_Game
{
    abstract class Drawable
    {
        public void Draw(Position position, char symbol, ConsoleColor color = ConsoleColor.Yellow)
        {
            Console.SetCursorPosition(position.row, position.col);
            Console.ForegroundColor = color;
            Console.Write(symbol);
        }
    }
}
