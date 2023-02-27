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
        static bool[,] TetrisField = new bool[TetrisRows, TetrisCols];
        static List<bool[,]> TetrisFigures = new List<bool[,]>()
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
        static bool[,] CurrentFigure = null;
        static int CurrentFigureRow = 0;
        static int CurrentFigureCol = 0;
        static Random Random = new Random();


        static void Main(string[] args)
        {
            Console.Title = "Tetris-v1.0";
            Console.WindowHeight = GameRows + 1;
            Console.WindowWidth = GameCols + 1;
            Console.BufferHeight = GameRows + 1;
            Console.BufferWidth = GameCols + 1;
            Console.CursorVisible = false;
            CurrentFigure = TetrisFigures[Random.Next(0, TetrisFigures.Count - 1)];

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
                        if (CurrentFigureCol + CurrentFigure.GetLength(1) < TetrisCols)
                        {
                            CurrentFigureCol++;
                        }
                    }
                    if (key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow)
                    {
                        if (CurrentFigureCol > 0)
                        {
                            CurrentFigureCol--;
                        }
                    }
                    if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow)
                    {
                        Score++;
                        Frame = 1;
                        CurrentFigureRow++;
                    }
                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        var rotatedFigure = new bool[CurrentFigure.GetLength(1), CurrentFigure.GetLength(0)];

                        for (int row = 0; row < CurrentFigure.GetLength(0); row++)
                        {
                            for (int col = 0; col < CurrentFigure.GetLength(1); col++)
                            {
                                rotatedFigure[col,CurrentFigure.GetLength(0) - row - 1] = CurrentFigure[row , col]; 
                            }
                        }

                        int rightBorder = rotatedFigure.GetLength(1) + CurrentFigureCol;
                        if (rightBorder >= TetrisCols)
                        {
                            CurrentFigureCol -= rightBorder - TetrisCols;
                        }
                        CurrentFigure = rotatedFigure;
                    }
                }
                //change game state
                if (Frame % FramesPerSecond == 0)
                {
                    Frame = 1;
                    CurrentFigureRow++;
                }
                if (Collision())
                {
                    AddCurrentFigureToTetrisFiels();
                    int lines = CheckForFullLines();
                    ResetFigure();
                    if (Collision())
                    {
                        GameOver();
                    }

                }

                //redraw UI
                DrawBorder();
                DrawGameInfo();
                DrawCurrentFigure();
                DrawTerrisField();

                Thread.Sleep(40);

            }

        }

        private static int CheckForFullLines()
        {
            int linesCount = 0;

            for (int row = 0; row < TetrisField.GetLength(0); row++)
            {
                bool isFullLine = true;
                for (int col = 0; col < TetrisField.GetLength(1); col++)
                {
                    if (TetrisField[row,col] == false)
                    {
                        isFullLine = false;
                    }
                }

                if (isFullLine)
                {
                    linesCount++;

                    for (int r = row; r >= 1; r--)
                    {
                        for (int c = 0; c < TetrisField.GetLength(1); c++)
                        {
                            TetrisField[r, c] = TetrisField[r - 1, c];
                        }
                    }
                }
            }

            return linesCount;
        }

        private static void GameOver()
        {
            string line = $"[{DateTime.UtcNow.ToString()}] {Environment.UserName} -> {Score.ToString()}";
            File.AppendAllLines("score.txt", new List<string> { line });
            string scoreLine = $"    {Score.ToString()}";
            scoreLine += new string(' ', 15 - scoreLine.Length);

            Draw("________________", 7, 3);
            Draw("|  Game Over!   |", 8, 3);
            Draw("|     Score:    |", 9, 3);
            Draw($"|{scoreLine}|", 10, 3);
            Draw("________________", 11, 3);

            Thread.Sleep(10000);
            Environment.Exit(0);
        }

        private static void ResetFigure()
        {
            var figure = TetrisFigures[Random.Next(0, TetrisFigures.Count - 1)];
            CurrentFigureRow = 0;
            CurrentFigureCol = 0;
            CurrentFigure = figure;
        }

        private static void DrawTerrisField()
        {
            for (int row = 0; row < TetrisField.GetLongLength(0); row++)
            {
                for (int col = 0; col < TetrisField.GetLongLength(1); col++)
                {
                    if (TetrisField[row, col])
                    {
                        Draw("*", row + 1, col + 1);
                    }
                }
            }
        }

        private static void AddCurrentFigureToTetrisFiels()
        {
            for (int row = 0; row < CurrentFigure.GetLength(0); row++)
            {
                for (int col = 0; col < CurrentFigure.GetLongLength(1); col++)
                {
                    if (CurrentFigure[row, col])
                    {
                        TetrisField[row + CurrentFigureRow, col + CurrentFigureCol] = true;
                    }
                }
            }
        }

        private static bool Collision()
        {
            if (CurrentFigureRow + CurrentFigure.GetLength(0) >= TetrisRows)
            {
                return true;
            }
            for (int row = 0; row < CurrentFigure.GetLength(0); row++)
            {
                bool hasCollision = false;
                for (int col = 0; col < CurrentFigure.GetLength(1); col++)
                {
                    if (CurrentFigure[row, col])
                    {
                        hasCollision = TetrisField[CurrentFigureRow + row + 1, CurrentFigureCol + col];
                    }
                    if (hasCollision)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static void DrawCurrentFigure()
        {
            for (int r = 0; r < CurrentFigure.GetLength(0); r++)
            {
                for (int c = 0; c < CurrentFigure.GetLength(1); c++)
                {
                    if (CurrentFigure[r, c])
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
            Console.SetCursorPosition(0, 0);

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