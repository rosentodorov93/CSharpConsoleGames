using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class TetrisConsoleDrawer
    {
        private int tetrisRows;
        private int tetrisCols;
        private int infoCols;
        private int gameRows;
        private int gameCols;
        private string tetrisCharacter;

        public TetrisConsoleDrawer(int tetrisRows, int tetrisCols, int infoCols = 11, string tetrisCharacter = "*")
        {
            this.tetrisRows = tetrisRows;
            this.tetrisCols = tetrisCols;
            this.infoCols = infoCols;
            this.gameRows = tetrisRows + 2;
            this.gameCols = tetrisCols + infoCols + 3;
            this.tetrisCharacter = tetrisCharacter;

            Console.Title = "Tetris-v1.0";
            Console.WindowHeight = this.gameRows + 1;
            Console.WindowWidth = this.gameCols + 1;
            Console.BufferHeight = this.gameRows + 1;
            Console.BufferWidth = this.gameCols + 1;
            Console.CursorVisible = false;
        }

        public void DrawUi(TetrisGame game, ScoreMenager score)
        {
            this.DrawBorder();
            this.DrawGameInfo(score.Score, score.HighScore, game.Level);
            this.DrawCurrentFigure(game.CurrentFigure, game.CurrentFigureRow, game.CurrentFigureCol);
            this.DrawTerrisField(game.TetrisField);
        }
        public void DrawGameOver(ScoreMenager scoreMenager)
        {
            string scoreLine = $"    {scoreMenager.Score.ToString()}";
            scoreLine += new string(' ', 15 - scoreLine.Length);

            Draw("________________", 7, 3);
            Draw("|  Game Over!   |", 8, 3);
            Draw("|     Score:    |", 9, 3);
            Draw($"|{scoreLine}|", 10, 3);
            Draw("________________", 11, 3);

        }
        private void DrawTerrisField(bool[,] tetrisField)
        {
            for (int row = 0; row < tetrisField.GetLongLength(0); row++)
            {
                for (int col = 0; col < tetrisField.GetLongLength(1); col++)
                {
                    if (tetrisField[row, col])
                    {
                        Draw(this.tetrisCharacter, row + 1, col + 1);
                    }
                }
            }
        }
        private void DrawCurrentFigure(Tetronino currentFigure, int currentFigureRow, int currentFigureCol)
        {
            for (int r = 0; r < currentFigure.Height; r++)
            {
                for (int c = 0; c < currentFigure.Width; c++)
                {
                    if (currentFigure.Body[r, c])
                    {
                        Draw(this.tetrisCharacter, r + currentFigureRow + 1, c + currentFigureCol + 1, ConsoleColor.Yellow);
                    }
                }
            }
        }
        private void DrawGameInfo(int score, int highScore, int level)
        {
            Draw("Score:", 2, this.tetrisCols + 3, ConsoleColor.DarkCyan);
            Draw(score.ToString(), 3, this.tetrisCols + 3, ConsoleColor.DarkCyan);

            Draw("Best:", 5, this.tetrisCols + 3, ConsoleColor.DarkCyan);
            Draw(highScore.ToString(), 6, this.tetrisCols + 3, ConsoleColor.DarkCyan);

            Draw("Level:", 8, this.tetrisCols + 3, ConsoleColor.DarkCyan);
            Draw(level.ToString(), 9, this.tetrisCols + 3, ConsoleColor.DarkCyan);

        }
        private void DrawBorder()
        {
            Console.SetCursorPosition(0, 0);

            string startLine = "╔";
            startLine += new string('═', this.tetrisCols);
            startLine += "╦";
            startLine += new string('═', this.infoCols);
            startLine += "╗";
            Console.WriteLine(startLine);

            for (int i = 0; i < this.tetrisRows; i++)
            {
                string line = "║";
                line += new string(' ', this.tetrisCols);
                line += "║";
                line += new string(' ', this.tetrisCols);
                line += "║";
                Console.WriteLine(line);
            }

            string endLine = "╚";
            endLine += new string('═', this.tetrisCols);
            endLine += "╩";
            endLine += new string('═', this.infoCols);
            endLine += "╝";
            Console.WriteLine(endLine);
        }
        private void Draw(string symbol, int row, int col, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(col, row);
            Console.Write(symbol);
            Console.ResetColor();
        }
    }
}
