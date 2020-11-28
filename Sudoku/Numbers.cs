using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    public enum Numbers
    {
        One = 2,
        Two = 4,
        Three = 8,
        Four = 16,
        Five = 32,
        Six = 64,
        Seven = 128,
        Eight = 256,
        Nine = 512,
        Any = 1022        
    }

    public static class NumberExtensions
    {
        public static int AsInt(this Numbers num) => 
            num != Numbers.Any 
                ? (int)Math.Log2((int)num)
                : 0;

        public static Numbers? AsNumbers(this int i) =>
            i > 0 && i <= 9
                ? (Numbers)Math.Pow(2, i)
                : (Numbers?)null;
    }
}