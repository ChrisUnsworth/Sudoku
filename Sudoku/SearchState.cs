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

            for (int i; i < 9; i++)
            {
                for (int j; j < 9; j++)
                {
                    _data[i, j] = (int)Numbers.Any;
                }
            }
        }

        public bool IsSolution => throw new NotImplementedException();

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
