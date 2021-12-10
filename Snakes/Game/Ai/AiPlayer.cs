using System;
using System.Linq;
using Snakes.Game.Field;
using Snakes.Tools;

namespace Snakes.Game.Ai
{
    public class AiPlayer : Player.Player
    {
        public int Difficulty;
        private FieldNode _head;
        
        public AiPlayer(GameField field, int x, int y, int number, int difficulty = 5) : base(field, x, y, number)
        {
            Difficulty = difficulty;
        }

        public override OnMoveEventArgs Move()
        {
            /*Random random = new Random();
            Direction direction = Direction.None;
            while (!Move(direction))
            {
                direction = (Direction)random.Next(1, 5);
            }*/
            
            
            _head = new FieldNode(_field.Copy(), Number, null);
            var bestSolution = FieldNode.Minimax(_head, Difficulty, Int32.MinValue, Int32.MaxValue);
            while (bestSolution.Parent?.Parent != null)
            {
                bestSolution = bestSolution.Parent;
            }
            var result = new OnMoveEventArgs()
            {
                OriginX = X,
                OriginY = Y,
                Direction = bestSolution.Delta,
                PlayerNumber = Number
            };
            Move(bestSolution.Delta);
            return result;
        }
        


        public override Player.Player Copy(GameField field)
        {
            return new AiPlayer(field, X, Y, Number);
        }
    }
}