using System.Text.RegularExpressions;

namespace Tetris
{
    internal class Program
    {


        static void Main(string[] args)
        {
            int tetrisRows = 20;
            int tetrisCols = 10;

            var musicPlayer = new MusicPlayer();
            musicPlayer.PlayMusic();

            var tetrisGame = new GameManager(tetrisRows, tetrisCols);
            tetrisGame.MainLoop();

        }
    }
}