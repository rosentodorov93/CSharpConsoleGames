using System.Text.RegularExpressions;

namespace Tetris
{
    public class ScoreMenager
    {
        private readonly string highScoreFileName;
        private readonly int[] ScorePerLine = { 1, 40, 100, 300, 1200 };
        public ScoreMenager(string fileName)
        {
            this.highScoreFileName = fileName;
            this.HighScore = GetHighScore();
        }

        public int Score { get; private set; }
        public int HighScore { get; private set; }

        public int GetHighScore()
        {
            int score = 0;
            if (File.Exists(this.highScoreFileName))
            {
                string[] lines = File.ReadAllLines(this.highScoreFileName);

                foreach (var line in lines)
                {
                    var currentScore = int.Parse(Regex.Match(line, @"-> ([0-9]+)").Groups[1].Value);
                    score = Math.Max(score, currentScore);
                }
            }
            
            return score;
        }
        public void AddHighScore()
        {
            string line = $"[{DateTime.UtcNow.ToString()}] {Environment.UserName} -> {this.Score.ToString()}";
            File.AppendAllLines(this.highScoreFileName, new List<string> { line });
        }
        public void AddScore(int linesCount)
        {
            this.Score += ScorePerLine[linesCount];
            if (this.Score > this.HighScore)
            {
                this.HighScore = this.Score;
            }

        }
    }
}
