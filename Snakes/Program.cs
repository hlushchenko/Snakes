using System;
using System.Linq;
using Snakes.Game.Field;
using Snakes.Game.Player;
using Snakes.Tools;

namespace Snakes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
            Console.Clear();
            GameConfig config = new GameConfig()
            {
                Size = 10,
                Player1Bot = true,
                Player2Bot = true,
                Bot1Difficulty = Difficulty.Hard,
                Bot2Difficulty = Difficulty.Easy
            };
            GameField field = new GameField(config);
            field.Render();
            field.OnMove += FieldOnOnMove;
            field.OnInputRequired += FieldInput;
            field.Game();
            Console.ReadKey();


            /*HumanPlayer player = new HumanPlayer(null, 1, 1, 1);
            player.X = 2;
            var player2 = new HumanPlayer(null, 2, 2, 3);
            player2 = (HumanPlayer)player.Copy(null);
            player2.X = 3;
            Console.WriteLine(player.X);
            Console.WriteLine(player2.X);*/
        }

        private static Direction FieldInput(object sender, OnInputRequiredEventArgs e)
        {
            Console.WriteLine($"Player {e.PlayerNumber}, your turn!");
                return Console.ReadLine() switch
                {
                    "w" => Direction.Up,
                    "s" => Direction.Down,
                    "a" => Direction.Left,
                    "d" => Direction.Right,
                    _ => Direction.None
                };
        }

        private static void FieldOnOnMove(object sender, OnMoveEventArgs e)
        {
            GameField field = (GameField) sender;
            Console.Clear();
            field.Render();
        }
    }
}
