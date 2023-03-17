using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class TetrisGame
    {
        private readonly List<Tetronino> TetrisFigures = new List<Tetronino>()
        {
            new Tetronino(new bool[,]
            {
                {true, true,true,true}
            }),
            new Tetronino(new bool[,]
            {
                {true, true,},
                {true, true,}
            }),
            new Tetronino(new bool[,]
            {
                {false, true,false},
                {true, true,true}
            }),
            new Tetronino(new bool[,]
            {
                {true, true,false},
                {false, true,true}
            }),
            new Tetronino(new bool[,]
            {
                {false, true,true},
                {true, true,false}
            }),
            new Tetronino(new bool[,]
            {
                {true, false,false},
                {true, true,true}
            }),
            new Tetronino(new bool[,]
            {
                {false, false,true},
                {true, true,true}
            })
        };
        private Random random;

        public TetrisGame(int tetrisRows, int tetrisCols)
        {
            this.TetrisField = new bool[tetrisRows, tetrisCols];
            this.TetrisRows = tetrisRows;
            this.TetrisCols = tetrisCols;
            this.LinesToLevel = 3;
            this.Frame = 0;
            this.FramesPerSecond = 15;
            this.LinesCleared = 0;
            this.Level = 1;
            this.CurrentFigureRow = 0;
            this.CurrentFigureCol = 0;
            this.CurrentFigure = null;
            this.random = new Random();
            this.GenerateRandomFigure();
        }
        public bool[,] TetrisField { get; private set; }
        public int TetrisRows { get; }
        public int TetrisCols { get; }
        public int LinesToLevel { get; private set; }
        public int Frame { get; set; }
        public int FramesPerSecond { get; private set; }
        public int LinesCleared { get; private set; }
        public int Level { get; private set; }
        public Tetronino CurrentFigure { get; set; }
        public int CurrentFigureRow { get; set; }
        public int CurrentFigureCol { get; set; }

        public void GenerateRandomFigure()
        {
            this.CurrentFigure = TetrisFigures[random.Next(0, TetrisFigures.Count)];
            this.CurrentFigureRow = 0;
            this.CurrentFigureCol = 0;
        }

        public void UpdateLevel(int linesCount)
        {
            this.LinesCleared += linesCount;
            if (this.LinesCleared >= this.LinesToLevel)
            {
                this.Level++;
                this.LinesCleared = this.LinesCleared - this.LinesToLevel;
            }
        }

        public void UpdateState()
        {
            if (this.Frame % (this.FramesPerSecond - this.Level) == 0)
            {
                this.Frame = 1;
                this.CurrentFigureRow++;
            }
        }

        public void AddCurrentFigureToTetrisFiels()
        {
            for (int row = 0; row < this.CurrentFigure.Height; row++)
            {
                for (int col = 0; col < this.CurrentFigure.Width; col++)
                {
                    if (this.CurrentFigure.Body[row, col])
                    {
                        this.TetrisField[row + this.CurrentFigureRow, col + this.CurrentFigureCol] = true;
                    }
                }
            }
        }

        public  int CheckForFullLines()
        {
            int linesCount = 0;

            for (int row = 0; row < this.TetrisField.GetLength(0); row++)
            {
                bool isFullLine = true;
                for (int col = 0; col < this.TetrisField.GetLength(1); col++)
                {
                    if (this.TetrisField[row, col] == false)
                    {
                        isFullLine = false;
                    }
                }

                if (isFullLine)
                {
                    linesCount++;

                    for (int r = row; r >= 1; r--)
                    {
                        for (int c = 0; c < this.TetrisField.GetLength(1); c++)
                        {
                            TetrisField[r, c] = TetrisField[r - 1, c];
                        }
                    }
                }
            }

            return linesCount;
        }

        public  bool Collision()
        {
            if (this.CurrentFigureRow + this.CurrentFigure.Height >= TetrisRows)
            {
                return true;
            }
            for (int row = 0; row < this.CurrentFigure.Height; row++)
            {
                bool hasCollision = false;
                for (int col = 0; col < this.CurrentFigure.Width; col++)
                {
                    if (this.CurrentFigure.Body[row, col])
                    {
                        hasCollision = this.TetrisField[this.CurrentFigureRow + row + 1, this.CurrentFigureCol + col];
                    }
                    if (hasCollision)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public  bool IsSave(Tetronino figure, int row, int col)
        {
            if (col < 0)
            {
                return false;
            }
            if (col + figure.Width > this.TetrisCols)
            {
                return false;
            }

            for (int r = 0; r < figure.Height; r++)
            {
                for (int c = 0; c < figure.Width; c++)
                {
                    if (figure.Body[r, c] && this.TetrisField[row + r, col + c])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool IsRotateSave(Tetronino rotatedFigure)
        {
            int rightBorder = rotatedFigure.Width + this.CurrentFigureCol;
            if (rightBorder > TetrisCols)
            {

                this.CurrentFigureCol -= rightBorder - this.TetrisCols;


            }

            if (this.IsSave(rotatedFigure, this.CurrentFigureRow, this.CurrentFigureCol))
            {
                return true;

            }

            return false;
        }
    }
}
