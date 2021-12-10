using Snakes.Game.Field;
using Snakes.Tools;

namespace Snakes.Game.Player
{
    public abstract class Player
    {
        public int X;
        public int Y;
        public int Number;
        protected GameField _field;
        public Direction LastMove = Direction.None;
        

        public Player(GameField field, int x, int y, int number)
        {
            X = x;
            Y = y;
            Number = number;
            _field = field;
        }
        public int PossibleMoves
        {
            get
            {
                
                return RecursiveCheck(new bool[_field.Size, _field.Size], X, Y);
            }
        }

        private int RecursiveCheck(bool[,] checked_, int x, int y)
        {
            int result = 1;
            checked_[x, y] = true;
            if (x!=0 && !checked_[x-1, y] && _field.Field[x-1, y] == 0)
            {
                result += RecursiveCheck(checked_, x - 1, y);
            }
            if (x<_field.Size-1 && !checked_[x+1, y] && _field.Field[x+1, y] == 0)
            {
                result += RecursiveCheck(checked_, x + 1, y);
            }
            if (y!=0 && !checked_[x, y-1] && _field.Field[x, y-1] == 0)
            {
                result += RecursiveCheck(checked_, x, y-1);
            }
            if (y<_field.Size-1 && !checked_[x, y+1] && _field.Field[x, y+1] == 0)
            {
                result += RecursiveCheck(checked_, x , y+1);
            }

            return result;
        }

        public bool CanMove()
        {
            bool result = false;
            if (X != 0) result |= _field.Field[X - 1, Y] == 0;
            if (X != _field.Size - 1) result |= _field.Field[X + 1, Y] == 0;
            if (Y != 0) result |= _field.Field[X, Y - 1] == 0;
            if (Y != _field.Size - 1) result |= _field.Field[X, Y + 1] == 0;
            return result;
        }

        public bool CanMove(Direction direction)
        {
            return direction switch
            {
                 Direction.Up => Y!=0 && _field.Field[X, Y-1] == 0,
                 Direction.Down => Y != _field.Size - 1 && _field.Field[X, Y + 1] == 0,
                 Direction.Left => X != 0 && _field.Field[X - 1, Y] == 0,
                 Direction.Right => X != _field.Size - 1 && _field.Field[X + 1, Y] == 0,
                 _ => false
            };
        }

        public abstract OnMoveEventArgs Move();
        public abstract Player Copy(GameField field);
        

        public bool Move(Direction direction)
        {
            if (!CanMove(direction))  return false;
            switch (direction)
            {
                case Direction.Up:
                    Y--;
                    break;
                case Direction.Down:
                    Y++;
                    break;
                case Direction.Left:
                    X--;
                    break;
                case Direction.Right:
                    X++;
                    break;
            }
            _field.Field[X, Y] = Number;
            return true;
        }
        
    }
}