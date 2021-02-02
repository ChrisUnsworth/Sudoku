using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.common
{
    public interface ISearchState : IState
    {
        ISet<int> Domain(int x, int y);
        ISet<int> Domain((int, int) i);

        int BitDomain(int x, int y);
        int BitDomain((int, int) i);

        bool CanBe(int x, int y, int val);
        bool CanBe((int, int) i, int val);

        bool SetNot(int x, int y, int val);
        bool SetNot((int, int) i, int val);

        bool DomainMinus(int x, int y, int domain);
        bool DomainMinus((int, int) i, int domain);

        void Reset(int x, int y);
        void Reset((int, int) i);

        ISearchState Copy();

        bool IsSolution { get; }
    }
}
