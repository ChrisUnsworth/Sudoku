using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.common
{
    public interface IState
    {
        int? this[int x, int y] { get; set; }
        int? this[(int x, int y) p] { get; set; }

        int[,] ToArray();
    }
}
