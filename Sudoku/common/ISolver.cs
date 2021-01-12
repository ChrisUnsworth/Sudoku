using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.common
{
    public interface ISolver
    {
        ISearchState Solve(IState puzzle);
        ISearchState Solve(ISearchState puzzle);
    }
}
