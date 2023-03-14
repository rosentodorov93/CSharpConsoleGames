using System.Text.RegularExpressions;

namespace Tetris
{
    internal class Program
    {

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
        static int LinesToLevel = 3;
        static int[] ScorePerLine = { 0, 40, 100, 300, 1200 };



        static int Frame = 0;
        static int FramesPerSecond = 16;
        static int Score = 0;
        static int HighScore;
        static int LinesCleared = 0;
        static int Level = 1;
        static bool[,] CurrentFigure = null;
        static int CurrentFigureRow = 0;
        static int CurrentFigureCol = 0;
        static Random Random = new Random();


        static void Main(string[] args)
        {
            var musicPlayer = new MusicPlayer();
            musicPlayer.PlayMusic();
            Console.Title = "Tetris-v1.0";
            Console.WindowHeight = GameRows + 1;
            Console.WindowWidth = GameCols + 1;
            Console.BufferHeight = GameRows + 1;
            Console.BufferWidth = GameCols + 1;
            Console.CursorVisible = false;
            CurrentFigure = TetrisFigures[Random.Next(0, TetrisFigures.Count - 1)];
            HighScore = GetHighScore();

            while (true)
            {
                Frame++;
                if (Score > HighScore)
                {
                    HighScore = Score;
                }

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey();

                    if (key.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }
                    if (key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow)
                    {
                        if (IsSave(CurrentFigure, CurrentFigureRow, CurrentFigureCol + 1))
                        {
                            CurrentFigureCol++;
                        }

                    }
                    if (key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow)
                    {
                        if (IsSave(CurrentFigure, CurrentFigureRow, CurrentFigureCol - 1))
                        {
                            CurrentFigureCol--;
                        }

                    }
                    if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow)
                    {
                        if (CurrentFigureRow + CurrentFigure.GetLength(0) < TetrisRows - 1)
                        {
                            Score += Level;
                            Frame = 1;
                            CurrentFigureRow++;
                        }
                    }
                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        CurrentFigure = RotateFigure(CurrentFigure);
                    }
                }

                if (Frame % (FramesPerSecond - Level) == 0)
                {
                    Frame = 1;
                    CurrentFigureRow++;
                }
                if (Collision())
                {
                    AddCurrentFigureToTetrisFiels();
                    int lines = CheckForFullLines();
                    Score += ScorePerLine[lines];
                    LinesCleared += lines;
                    if (LinesCleared >= LinesToLevel)
                    {
                        Level++;
                        LinesCleared = LinesCleared - LinesToLevel;

                    }
                    ResetFigure();
                    if (Collision())
                    {
                        GameOver();
                    }

                }


                DrawBorder();
                DrawGameInfo();
                DrawCurrentFigure();
                DrawTerrisField();

                Thread.Sleep(40);

            }

        }

        private static int GetHighScore()
        {
            int score = 0;
            if (File.Exists("score.txt"))
            {
                string[] lines = File.ReadAllLines("score.txt");

                foreach (var line in lines)
                {
                    var currentScore = int.Parse(Regex.Match(line, @"-> ([0-9]+)").Groups[1].Value);
                    score = Math.Max(score, currentScore);
                }
            }

            return score;
        }

        private static bool[,] RotateFigure(bool[,] currentFigure)
        {
            var rotatedFigure = new bool[currentFigure.GetLength(1), currentFigure.GetLength(0)];

            for (int row = 0; row < currentFigure.GetLength(0); row++)
            {
                for (int col = 0; col < CurrentFigure.GetLength(1); col++)
                {
                    rotatedFigure[col, currentFigure.GetLength(0) - row - 1] = currentFigure[row, col];
                }
            }

            int rightBorder = rotatedFigure.GetLength(1) + CurrentFigureCol;
            if (rightBorder > TetrisCols)
            {

                CurrentFigureCol -= rightBorder - TetrisCols;


            }

            if (IsSave(rotatedFigure, CurrentFigureRow, CurrentFigureCol))
            {
                return rotatedFigure;

            }
            return currentFigure;
        }

        private static bool IsSave(bool[,] figure, int row, int col)
        {
            if (col < 0)
            {
                return false;
            }
            if (col + figure.GetLength(1) > TetrisCols)
            {
                return false;
            }

            for (int r = 0; r < figure.GetLength(0); r++)
            {
                for (int c = 0; c < figure.GetLength(1); c++)
                {
                    if (figure[r, c] && TetrisField[row + r, col + c])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static int CheckForFullLines()
        {
            int linesCount = 0;

            for (int row = 0; row < TetrisField.GetLength(0); row++)
            {
                bool isFullLine = true;
                for (int col = 0; col < TetrisField.GetLength(1); col++)
                {
                    if (TetrisField[row, col] == false)
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
            var figure = TetrisFigures[Random.Next(0, TetrisFigures.Count)];
            CurrentFigureRow = 0;
            CurrentFigureCol = 0;
            CurrentFigure = figure;
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
            Draw("Score:", 2, TetrisCols + 3, ConsoleColor.DarkCyan);
            Draw(Score.ToString(), 3, TetrisCols + 3, ConsoleColor.DarkCyan);

            Draw("Best:", 5, TetrisCols + 3, ConsoleColor.DarkCyan);
            Draw(HighScore.ToString(), 6, TetrisCols + 3, ConsoleColor.DarkCyan);

            Draw("Level:", 8, TetrisCols + 3, ConsoleColor.DarkCyan);
            Draw(Level.ToString(), 9, TetrisCols + 3, ConsoleColor.DarkCyan);


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