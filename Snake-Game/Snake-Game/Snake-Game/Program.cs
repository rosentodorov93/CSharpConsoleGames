﻿namespace Snake_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var game = new SnakeGame(new Snake(), new InputManager(), new Food());
            game.Start();
        }
    }
}