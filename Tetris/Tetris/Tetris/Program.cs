namespace Tetris
{
    internal class Program
    {
        //settings
        static int TetrisRows = 20;
        static int TetrisCols = 10;
        static int InfoCols = 10;
        static int GameRows = TetrisRows + 2;
        static int GameCols = TetrisCols + InfoCols + 3;


        //game state



        static void Main(string[] args)
        {
            Console.Title = "Tetris-v1.0";
            Console.WindowHeight = GameRows + 1;
            Console.WindowWidth = GameCols + 1;
            Console.BufferHeight = GameRows + 1;
            Console.BufferWidth = GameCols + 1;
            Console.CursorVisible = false;
            DrawBorder();
            while (true)
            {

            }
            //while loop
            //read input
            //change game state
            //redraw UI
            ///draw border
            ///draw gameinfo
            //sleep

        }

        private static void DrawBorder()
        {
            string startLine = "╔";
            startLine += new string('═', TetrisCols);
            startLine += "╦";
            startLine += new string('═', InfoCols);
            startLine += "╗";
            Console.WriteLine(startLine);

            for (int i = 0; i < TetrisRows; i++)
            {
                string line = "║";
                line += new string(' ', TetrisCols);
                line += "║";
                line += new string(' ', InfoCols);
                line += "║";
                Console.WriteLine(line);
            }

            string endLine = "╚";
            endLine += new string('═', TetrisCols);
            endLine += "╩";
            endLine += new string('═', InfoCols);
            endLine += "╝";
            Console.WriteLine(endLine);
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