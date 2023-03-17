using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class GameManager
    {
        private ScoreMenager scoreMenager;
        private TetrisGame tetrisGame;
        private TetrisConsoleDrawer tetrisDrawer;
        private TetrisConsoleInputManager inputManager;


        public GameManager(int tetrisRows, int tetrisCols)
        {
            this.scoreMenager = new ScoreMenager("score.txt");
            this.tetrisGame = new TetrisGame(tetrisRows, tetrisCols);
            this.tetrisDrawer = new TetrisConsoleDrawer(tetrisRows, tetrisCols);
            this.inputManager = new TetrisConsoleInputManager();
        }

        public void MainLoop()
        {
            while (true)
            {
                tetrisGame.Frame++;

                var input = inputManager.GetInput();

                switch (input)
                {
                    case TetrisInput.None:
                        break;
                    case TetrisInput.Left:
                        if (tetrisGame.IsSave(tetrisGame.CurrentFigure, tetrisGame.CurrentFigureRow, tetrisGame.CurrentFigureCol - 1))
                        {
                            tetrisGame.CurrentFigureCol--;
                        }
                        break;
                    case TetrisInput.Right:
                        if (tetrisGame.IsSave(tetrisGame.CurrentFigure, tetrisGame.CurrentFigureRow, tetrisGame.CurrentFigureCol + 1))
                        {
                            tetrisGame.CurrentFigureCol++;
                        }
                        break;
                    case TetrisInput.Down:
                        if (tetrisGame.CurrentFigureRow + tetrisGame.CurrentFigure.Height < tetrisGame.TetrisRows - 1)
                        {
                            scoreMenager.AddScore(0);
                            tetrisGame.Frame = 1;
                            tetrisGame.CurrentFigureRow++;
                        }
                        break;
                    case TetrisInput.Rotate:
                        var rotatedFigure = tetrisGame.CurrentFigure.RotateFigure();
                        if (tetrisGame.IsRotateSave(rotatedFigure))
                        {
                            tetrisGame.CurrentFigure = rotatedFigure;
                        }
                        break;
                    case TetrisInput.Exit:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
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
