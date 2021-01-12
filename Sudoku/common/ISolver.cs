using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.common
{
    public interface ISolver
    {
        IState Solve(IState puzzle);
        IState Solve(ISearchState puzzle);
    }
}
