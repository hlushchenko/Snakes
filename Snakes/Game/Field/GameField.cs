using System;
using System.Threading;
using Snakes.Game.Ai;
using Snakes.Game.Player;
using Snakes.Tools;
using static Snakes.Tools.Utilities;

namespace Snakes.Game.Field
{
    public class GameField
    {
        public Player.Player Player1;
        public Player.Player Player2;

        public delegate void OnMoveDelegate(object sender, OnMoveEventArgs e);

        public delegate Direction OnInputRequiredDelegate(object sender, OnInputRequiredEventArgs e);

        public event OnInputRequiredDelegate OnInputRequired;

        public event OnMoveDelegate OnMove;
        public int[,] Field;
        public int Size;
        public bool GameOver => !(Player1.CanMove() && Player2.CanMove());

        public GameField(int size)
        {
            Size = size;
            Field = new int[size, size];
        }

        public GameField(GameConfig config)
        {
            Size = config.Size;
            Field = new int[Size, Size];
            var xStart = FitInRange(config.StartX, 0, Size - 1);
            var yStart = FitInRange(config.StartY, 0, Size - 1);
            Field[xStart, yStart] = 1;
            Field[Size - xStart - 1, Size - yStart - 1] = 2;
            if (config.Player1Bot)
            {
                Player1 = new AiPlayer(this, xStart, yStart, 1, (int)config.Bot1Difficulty);
            }
            else
            {
                var player = new HumanPlayer(this, xStart, yStart, 1);
                player.OnInputRequired += InputRequired;
                Player1 = player;
            }
            if (config.Player2Bot)
            {
                Player2 = new AiPlayer(this, Size - xStart - 1, Size - yStart - 1, 2, (int)config.Bot2Difficulty);
            }
            else Player2 = new HumanConsolePlayer(this,Size - xStart - 1,Size - yStart - 1, 2);
            OnCreate();
        }

        private Direction InputRequired(object sender, OnInputRequiredEventArgs e)
        {
            if (OnInputRequired != null) return OnInputRequired.Invoke(sender, e);
            else
            {
                return Direction.None;
            }
        }

        private void OnCreate()
        {
            for (int i = 1; i <= 2; i++)
            {
                var eventArgs = new OnMoveEventArgs()
                {
                    OriginX = GetPlayer(i).X,
                    OriginY = GetPlayer(i).Y,
                    PlayerNumber = i,
                    Direction = Direction.None
                };
                OnMove?.Invoke(this, eventArgs);
            }
        }

        public void Game()
        {
            while (!GameOver)
            {
                OnMove?.Invoke(this, Player1.Move());
                Thread.Sleep(10);
                OnMove?.Invoke(this, Player2.Move());
                Thread.Sleep(10);
                
            }
            Console.WriteLine($"Game Over! {(Player1.CanMove()?"Blue Wins!":"Red Wins!")}");
        }

        public void Render()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Console.ForegroundColor = Field[j, i] switch
                    {
                        1 => ConsoleColor.Blue,
                        2 => ConsoleColor.Red,
                        _ => ConsoleColor.White
                    };
                    Console.Write("██");
                }

                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        //TODO: переписать в массив (мб)

        public Player.Player GetPlayer(int number)
        {
            return number switch
            {
                1 => Player1,
                2 => Player2,
                _ => null
            };
        }

        public GameField Copy()
        {
            var result = new GameField(Size);
            Array.Copy(Field, result.Field, Size*Size);
            result.Player1 = Player1.Copy(result);
            result.Player2 = Player2.Copy(result);
            return result;
        }
    }
}