using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.utils
{
    public static class BitCounter
    {
        private static readonly int[] _lookup = Enumerable.Range(0, (int)Numbers.Any + 1).Select(CountBits).ToArray();

        public static int Count(int value) => _lookup[value];

        private static int CountBits(int value)
        {
            int count = 0;
            while (value != 0)
            {
                count++;
                value &= value - 1;
            }
            return count;
        }

        public static IEnumerable<List<T>> PowerSet<T>(IList<T> list, int min, int max)
        {
            var count = 1 << list.Count;
            return Enumerable.Range(0, count)
                .Where(mask => Count(mask) >= min && Count(mask) <= max)
                .Select(mask =>
                    Enumerable.Range(0, list.Count)
                        .Where(i => (mask & (1 << i)) > 0)
                        .Select(i => list[i])
                        .ToList());
        }
    }
}
