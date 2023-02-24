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
        static List<bool[,]> TerisFigures = new List<bool[,]>()
        {
            new bool[,]
            {
                {true, true,true,true}
            },
            new bool[,]
            {
                {true, true,},
                {true, true,}
            },
            new bool[,]
            {
                {false, true,false},
                {true, true,true}
            },
            new bool[,]
            {
                {true, true,false},
                {false, true,true}
            },
            new bool[,]
            {
                {false, true,true},
                {true, true,false}
            },
            new bool[,]
            {
                {true, false,false},
                {true, true,true}
            },
            new bool[,]
            {
                {false, false,true},
                {true, true,true}
            }
        };


        //game state
        static int Frame = 0;
        static int FramesPerSecond = 15;
        static int Score = 0;
        static int CurrentFigureIndex = 2;
        static int CurrentFigureRow = 0;
        static int CurrentFigureCol = 0;


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
                Frame++;
                //read input
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey();

                    if (key.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }
                    if (key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow)
                    {
                        CurrentFigureCol++;
                    }
                    if (key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow)
                    {
                        CurrentFigureCol--;
                    }
                    if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow)
                    {
                        Score++;
                        CurrentFigureRow++;
                    }
                }
                //change game state
                if (Frame % FramesPerSecond == 0)
                {
                    Frame = 1;
                    CurrentFigureRow++;
                }
                //redraw UI
                DrawBorder();
                DrawGameInfo();
                DrawCurrentFigure();
                Thread.Sleep(40);
                
            }

        }

        private static void DrawCurrentFigure()
        {
            var currentFigure = TerisFigures[CurrentFigureIndex];

            for (int r = 0; r < currentFigure.GetLength(0); r++)
            {
                for (int c = 0; c < currentFigure.GetLength(1); c++)
                {
                    if (currentFigure[r,c])
                    {
                        Draw("*", r + CurrentFigureRow + 1, c + CurrentFigureCol + 1, ConsoleColor.Yellow);
                    }
                }
            }
        }

        private static void DrawGameInfo()
        {
            Draw("Frame:", 2, TetrisCols + 2, ConsoleColor.DarkCyan);
            Draw(Frame.ToString(), 3, TetrisCols + 2, ConsoleColor.DarkCyan);

            Draw("Score:", 4, TetrisCols + 2, ConsoleColor.DarkCyan);
            Draw(Score.ToString(), 5, TetrisCols + 2, ConsoleColor.DarkCyan);

        }

        private static void DrawBorder()
        {
            Console.SetCursorPosition(0,0);

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