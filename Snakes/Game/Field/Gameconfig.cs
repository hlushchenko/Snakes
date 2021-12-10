using Snakes.Tools;

namespace Snakes.Game.Field
{
    public class GameConfig
    {
        public int Size = 5;
        public bool Player1Bot = false;
        public bool Player2Bot = true;
        public Difficulty Bot1Difficulty = Difficulty.Medium;
        public Difficulty Bot2Difficulty = Difficulty.Medium;
        public int StartX = 1;
        public int StartY = 1;
    }
}