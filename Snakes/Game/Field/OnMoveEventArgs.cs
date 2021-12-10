using Snakes.Tools;

namespace Snakes.Game.Field
{
    public class OnMoveEventArgs
    {
        public Direction Direction = Direction.None;
        public int OriginX = 1;
        public int OriginY = 1;
        public int PlayerNumber = 1;
    }
}