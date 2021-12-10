using System;

namespace Snakes.Tools
{
    public static class Utilities
    {
        public static int FitInRange(int value, int min, int max)
        {
            return Math.Min(Math.Max(value, min), max);
        }

        public static int Opposite(int i)
        {
            return i == 1 ? 2 : 1;
        }

        public static bool SequenceEquals(this int[,] firstArray,int[,] secondArray)
        {
            if (firstArray.Length != secondArray.Length)
            {
                return false;
            }

            for (int i = 0; i < firstArray.GetLength(0); i++)
            {
                for (int j = 0; j < firstArray.GetLength(1); j++)
                {
                    if (firstArray[i,j] != secondArray[i,j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}