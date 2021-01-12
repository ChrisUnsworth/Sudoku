using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sudoku.common;
using Sudoku.utils;

namespace Sudoku
{
    public class SearchState : ISearchState
    {
        private int[,] _data;

        public SearchState()
        {
            _data = new int[9, 9];

            foreach (var i in Enumerable.Range(0, 9))
            {
                foreach (var j in Enumerable.Range(0, 9))
                {
                    _data[i, j] = 1022;
                }
            }
        }

        public SearchState(int[,] data)
        {
            _data = new int[9, 9];

            foreach (var i in Enumerable.Range(0, 9))
            {
                foreach (var j in Enumerable.Range(0, 9))
                {
                    _data[i, j] = data[i, j] > 0 ? (int)data[i, j].AsNumbers() : 1022;
                }
            }
        }

        public SearchState(SearchState state)
        {
            _data = new int[9, 9];

            foreach (var i in Enumerable.Range(0, 9))
            {
                foreach (var j in Enumerable.Range(0, 9))
                {

                    _data[i, j] = state[i, j].HasValue ? state.BitDomain(i, j) : 1022;
                }
            }
        }

        public SearchState(IState state) : this()
        {
            foreach (var (i, j) in Enumerable.Range(0, 9).SelectMany(i => Enumerable.Range(0, 9).Select(j => (i, j))))
            {
                this[i, j] = state[i, j];
            }
        }

        ISearchState ISearchState.Copy() => Copy();

        public SearchState Copy() => new SearchState(this);

        public bool IsSolution  => 
            Enumerable.Range(0, 9).All(i => Enumerable.Range(0, 9).All(j => this[i, j].HasValue));

        public int? this[int x, int y]
        {
            get
            {
                var v = _data[x, y];
                return OneBitSet(v)
                        ? (int)Math.Log2(v)
                        : (int?)null;
            }

            set
            {
                if (value.HasValue)
                {
                    _data[x, y] = (int)Math.Pow(2, value.Value);
                }
            }
        }

        public int? this[(int x, int y) p]
        {
            get => this[p.x, p.y];
            set => this[p.x, p.y]  = value;
        }

        public ISet<int> Domain((int, int) i) => Domain(i.Item1, i.Item2);
        public ISet<int> Domain(int x, int y) => Enumerable.Range(1, 9).Where(i => CanBe(x, y, i)).ToHashSet();

        public int BitDomain((int, int) i) => BitDomain(i.Item1, i.Item2);
        public int BitDomain(int x, int y) => _data[x, y];


        public string Pretty()
        {
            var sb = new StringBuilder();

            foreach (var j in Enumerable.Range(0, 9))
            {
                foreach (var i in Enumerable.Range(0, 9))
                {
                    if (i > 0 && i % 3 == 0) sb.Append("║");
                    else if (i > 0) sb.Append("|");
                    var val = this[i, j];
                    sb.Append(val.HasValue ? val.Value.ToString() : " ");
                }

                sb.AppendLine();

                if (j > 0 && (j + 1) % 3 == 0 && j < 8) sb.AppendLine(string.Join('╬', Enumerable.Repeat("═╬═╬═", 3)));
                else if (j < 8) sb.AppendLine(string.Join('╬', Enumerable.Repeat("─┼─┼─", 3)));
            }

            return sb.ToString();
        }

        public bool CanBe((int, int) i, int val) => CanBe(i.Item1, i.Item2, val);
        public bool CanBe(int x, int y, int val)
        {
            var bitVal = (int)Math.Pow(2, val);
            return (bitVal & _data[x, y]) == bitVal;
        }

        public bool SetNot((int, int) i, int val) => DomainMinus(i.Item1, i.Item2, (int)Math.Pow(2, val));
        public bool SetNot(int x, int y, int val) => DomainMinus(x, y, (int)Math.Pow(2, val));

        public bool DomainMinus((int, int) i, int domain) => DomainMinus(i.Item1, i.Item2, domain);
        public bool DomainMinus(int x, int y, int domain)
        {
            if ((_data[x, y] & domain) == 0) return false;

            _data[x, y] = _data[x, y] & ~domain;
            return true;
        }

        private bool OneBitSet(int i) => (i & (i - 1)) == 0;

        public int[,] ToArray()
        {
            var result = new int[9, 9];
            foreach (var i in Sets.All) result[i.x, i.y] = this[i] ?? 0;
            return result;
        }
    }
}
