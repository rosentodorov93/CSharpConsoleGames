namespace Tetris
{
    public class Tetronino
    {

        public Tetronino(bool[,] body)
        {
            this.Body = body;
        }

        public int Width => this.Body.GetLength(1);
        public int Height => this.Body.GetLength(0);

        public bool[,] Body { get; private set; }

        public Tetronino RotateFigure()
        {
            var rotatedFigure = new Tetronino(new bool[this.Width, this.Height]);

            for (int row = 0; row < this.Height; row++)
            {
                for (int col = 0; col < this.Width; col++)
                {
                    rotatedFigure.Body[col, this.Height - row - 1] = this.Body[row, col];
                }
            }

            return rotatedFigure;
        }
    }
}
