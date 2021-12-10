using System;
using Snakes.Game.Field;
using Snakes.Tools;

namespace Snakes.Game.Player
{
    public class HumanConsolePlayer : Player
    {
        public HumanConsolePlayer(GameField field, int x, int y, int number) : base(field, x, y, number)
        {
        }

        public override OnMoveEventArgs Move()
        {
            Direction direction = Direction.None;
            var result = new OnMoveEventArgs()
            {
                OriginX = X,
                OriginY = Y,
                PlayerNumber = Number
            };
            while (!Move(direction))
            {
                Console.WriteLine($"Player {Number}, your turn!");
                direction = Console.ReadLine() switch
                {
                    "w" => Direction.Up,
                    "s" => Direction.Down,
                    "a" => Direction.Left,
                    "d" => Direction.Right,
                    _ => Direction.None
                };
            }

            result.Direction = direction;
            return result;
        }

        public override Player Copy(GameField field)
        {
            return new HumanConsolePlayer(field, X, Y, Number);
        }
    }
}