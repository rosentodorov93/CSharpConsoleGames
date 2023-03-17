namespace Tetris
{
    public interface ITetrisGame
    {
        Tetronino CurrentFigure { get; set; }
        int CurrentFigureCol { get; set; }
        int CurrentFigureRow { get; set; }
        int Frame { get; set; }
        int FramesPerSecond { get; }
        int Level { get; }
        int LinesCleared { get; }
        int LinesToLevel { get; }
        int TetrisCols { get; }
        bool[,] TetrisField { get; }
        int TetrisRows { get; }

        void AddCurrentFigureToTetrisFiels();
        int CheckForFullLines();
        bool Collision();
        void GenerateRandomFigure();
        bool IsRotateSave(Tetronino rotatedFigure);
        bool IsSave(Tetronino figure, int row, int col);
        void UpdateLevel(int linesCount);
        void UpdateState();
    }
}