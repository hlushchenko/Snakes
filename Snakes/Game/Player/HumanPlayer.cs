using Snakes.Game.Field;
using Snakes.Tools;

namespace Snakes.Game.Player
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(GameField field, int x, int y, int number) : base(field, x, y, number)
        {
        }
        
        public event GameField.OnInputRequiredDelegate OnInputRequired;
        
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
                if (OnInputRequired != null)
                    direction = OnInputRequired.Invoke(this, new OnInputRequiredEventArgs()
                    {
                        OriginX = X,
                        OriginY = Y,
                        PlayerNumber = Number
                    });
                else
                    direction = Direction.None;
            }

            result.Direction = direction;
            return result;
        }

        public override Player Copy(GameField field)
        {
            return new HumanPlayer(field, X, Y, Number);
        }

    }
}