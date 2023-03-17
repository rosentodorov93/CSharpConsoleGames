namespace Tetris
{
    public class GameManager
    {
        private ScoreMenager scoreMenager;
        private ITetrisGame tetrisGame;
        private TetrisConsoleDrawer tetrisDrawer;
        private IInputManager inputManager;


        public GameManager(ITetrisGame tetrisGame, IInputManager inputManager, ScoreMenager scoreMenager, TetrisConsoleDrawer tetrisDrawer)
        {
            this.scoreMenager = scoreMenager;
            this.inputManager = inputManager;
            this.tetrisGame = tetrisGame;
            this.tetrisDrawer = tetrisDrawer;
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
