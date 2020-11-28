using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku
{
    public class SearchState
    {
        private int[,] _data;

        public SearchState()
        {
            _data = new int[9, 9];

            foreach (var i in Enumerable.Range(0, 9))
            {
                foreach (var j in Enumerable.Range(0, 9))
                {
                    _data[i, j] = (int)Numbers.Any;
                }
            }
        }

        public bool IsSolution  => 
            Enumerable.Range(0, 9).All(i => Enumerable.Range(0, 9).All(j => this[i, j].HasValue));

        public int? this[int x, int y]
        {
            get 
            {
                var v = _data[x, y];
                if (NumberOfSetBits(v) == 1)
                {
                    return (int)Math.Log2(v);
                }

                return null;
            }

            set
            {
                if (value.HasValue)
                {
                    _data[x, y] = (int)Math.Pow(2, value.Value);
                }
            }
        }

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

        public bool CanBe(int x, int y, int val)
        {
            var bitVal = (int)Math.Pow(2, val);
            return (bitVal & _data[x, y]) == bitVal;
        }

        public void SetNot(int x, int y, int val)
        {
            var bitVal = (int)Math.Pow(2, val);
            var bitNotVal = (int)Numbers.Any - bitVal;
            _data[x, y] = _data[x, y] & bitNotVal;
        }

        private int NumberOfSetBits(int i)
        {
            i = i - ((i >> 1) & 0x55555555);
            i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
            return (((i + (i >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
        }
    }
}
