using System.Text.RegularExpressions;

namespace Tetris
{
    internal class Program
    {

        static int TetrisRows = 20;
        static int TetrisCols = 10;
        static void Main(string[] args)
        {
            var musicPlayer = new MusicPlayer();
            var scoreMenager = new ScoreMenager("score.txt");
            var tetrisGame = new TetrisGame(TetrisRows, TetrisCols);
            var tetrisDrawer = new TetrisConsoleDrawer(TetrisRows, TetrisCols);
            //musicPlayer.PlayMusic();

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
                        if (tetrisGame.CurrentFigureRow + tetrisGame.CurrentFigure.Height < TetrisRows - 1)
                        {
                            scoreMenager.AddScore(0);
                            tetrisGame.Frame = 1;
                            tetrisGame.CurrentFigureRow++;
                        }
                    }
                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        var rotatedFigure = tetrisGame.CurrentFigure.RotateFigure();
                        if (tetrisGame.IsRotateSave(rotatedFigure))
                        {
                            tetrisGame.CurrentFigure = rotatedFigure;
                        }
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
                        scoreMenager.AddHighScore();
                        tetrisDrawer.DrawGameOver(scoreMenager);
                        Thread.Sleep(10000);
                        Environment.Exit(0);
                    }

                }

                tetrisDrawer.DrawUi(tetrisGame, scoreMenager);

                Thread.Sleep(40);

            }
        }
    }
}