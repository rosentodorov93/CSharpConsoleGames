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
        


        static void Main(string[] args)
        {
            var musicPlayer = new MusicPlayer();
            var scoreMenager = new ScoreMenager("score.txt");
            var tetrisGame = new TetrisGame(TetrisRows, TetrisCols);
            //musicPlayer.PlayMusic();
            Console.Title = "Tetris-v1.0";
            Console.WindowHeight = GameRows + 1;
            Console.WindowWidth = GameCols + 1;
            Console.BufferHeight = GameRows + 1;
            Console.BufferWidth = GameCols + 1;
            Console.CursorVisible = false;

            while (true)
            {
                tetrisGame.Frame++;

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey();

                    if (key.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }
                    if (key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow)
                    {
                        if (tetrisGame.IsSave(tetrisGame.CurrentFigure, tetrisGame.CurrentFigureRow, tetrisGame.CurrentFigureCol + 1))
                        {
                            tetrisGame.CurrentFigureCol++;
                        }

                    }
                    if (key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow)
                    {
                        if (tetrisGame.IsSave(tetrisGame.CurrentFigure, tetrisGame.CurrentFigureRow, tetrisGame.CurrentFigureCol - 1))
                        {
                            tetrisGame.CurrentFigureCol--;
                        }

                    }
                    if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow)
                    {
                        if (tetrisGame.CurrentFigureRow + tetrisGame.CurrentFigure.GetLength(0) < TetrisRows - 1)
                        {
                            scoreMenager.AddScore(0);
                            tetrisGame.Frame = 1;
                            tetrisGame.CurrentFigureRow++;
                        }
                    }
                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        tetrisGame.CurrentFigure = RotateFigure(tetrisGame);
                    }
                }

                tetrisGame.UpdateState();

                if (tetrisGame.Collision())
                {
                    tetrisGame.AddCurrentFigureToTetrisFiels();
                    int lines = tetrisGame.CheckForFullLines();
                    scoreMenager.AddScore(lines);
                    tetrisGame.UpdateLevel(lines);
                    tetrisGame.GenerateRandomFigure();

                    if (tetrisGame.Collision())
                    {
                        GameOver(scoreMenager);
                    }

                }


                DrawBorder();
                DrawGameInfo(scoreMenager.Score, scoreMenager.HighScore, tetrisGame.Level);
                DrawCurrentFigure(tetrisGame.CurrentFigure, tetrisGame.CurrentFigureRow, tetrisGame.CurrentFigureCol);
                DrawTerrisField(tetrisGame.TetrisField);

                Thread.Sleep(40);

            }

        }
        private static bool[,] RotateFigure(TetrisGame game)
        {
            var rotatedFigure = new bool[game.CurrentFigure.GetLength(1), game.CurrentFigure.GetLength(0)];

            for (int row = 0; row < game.CurrentFigure.GetLength(0); row++)
            {
                for (int col = 0; col < game.CurrentFigure.GetLength(1); col++)
                {
                    rotatedFigure[col, game.CurrentFigure.GetLength(0) - row - 1] = game.CurrentFigure[row, col];
                }
            }

            int rightBorder = rotatedFigure.GetLength(1) + game.CurrentFigureCol;
            if (rightBorder > TetrisCols)
            {

                game.CurrentFigureCol -= rightBorder - TetrisCols;


            }

            if (game.IsSave(rotatedFigure, game.CurrentFigureRow, game.CurrentFigureCol))
            {
                return rotatedFigure;

            }
            return game.CurrentFigure;
        }

        private static void GameOver(ScoreMenager scoreMenager)
        {

            scoreMenager.AddHighScore();
            string scoreLine = $"    {scoreMenager.Score.ToString()}";
            scoreLine += new string(' ', 15 - scoreLine.Length);

            Draw("________________", 7, 3);
            Draw("|  Game Over!   |", 8, 3);
            Draw("|     Score:    |", 9, 3);
            Draw($"|{scoreLine}|", 10, 3);
            Draw("________________", 11, 3);

            Thread.Sleep(10000);
            Environment.Exit(0);
        }

        private static void DrawTerrisField(bool[,] tetrisField)
        {
            for (int row = 0; row < tetrisField.GetLongLength(0); row++)
            {
                for (int col = 0; col < tetrisField.GetLongLength(1); col++)
                {
                    if (tetrisField[row, col])
                    {
                        Draw("*", row + 1, col + 1);
                    }
                }
            }
        }

        private static void DrawCurrentFigure(bool[,] currentFigure, int currentFigureRow, int currentFigureCol)
        {
            for (int r = 0; r < currentFigure.GetLength(0); r++)
            {
                for (int c = 0; c < currentFigure.GetLength(1); c++)
                {
                    if (currentFigure[r, c])
                    {
                        Draw("*", r + currentFigureRow + 1, c + currentFigureCol + 1, ConsoleColor.Yellow);
                    }
                }
            }
        }

        private static void DrawGameInfo(int score, int highScore, int level)
        {
            Draw("Score:", 2, TetrisCols + 3, ConsoleColor.DarkCyan);
            Draw(score.ToString(), 3, TetrisCols + 3, ConsoleColor.DarkCyan);

            Draw("Best:", 5, TetrisCols + 3, ConsoleColor.DarkCyan);
            Draw(highScore.ToString(), 6, TetrisCols + 3, ConsoleColor.DarkCyan);

            Draw("Level:", 8, TetrisCols + 3, ConsoleColor.DarkCyan);
            Draw(level.ToString(), 9, TetrisCols + 3, ConsoleColor.DarkCyan);


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