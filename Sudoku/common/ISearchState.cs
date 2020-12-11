using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.common
{
    public interface ISearchState : IState
    {
        ISet<int> Domain(int x, int y);

        int BitDomain(int x, int y);

        bool CanBe(int x, int y, int val);

        void SetNot(int x, int y, int val);

        ISearchState Copy();

        bool IsSolution { get; }
    }
}
