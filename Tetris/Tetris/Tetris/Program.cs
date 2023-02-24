namespace Tetris
{
    internal class Program
    {
        //settings
        static int TetrisRows = 20;
        static int TetrisCols = 10;
        static int InfoCols = 10;


        //game state



        static void Main(string[] args)
        {
            //while loop
            //read input
            //change game state
            //redraw UI
            ///draw border
            ///draw gameinfo
            
                  
        }

        private static void Draw(string symbol, int row, int col, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(col, row);
            Console.Write(symbol);
            Console.ResetColor();
        }
    }
}