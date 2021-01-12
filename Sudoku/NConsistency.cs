using Sudoku.common;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Sudoku.utils;

namespace Sudoku
{
    public class NConsistency : ISolver
    {
        private int _n;

        public NConsistency(int n)
        {
            _n = n;
        }

        public ISearchState Solve(IState puzzle) => Solve(new SearchState(puzzle));

        public ISearchState Solve(ISearchState puzzle)
        {
            var frontier = Sets.All.Where(p => puzzle[p].HasValue).ToList();

            while (frontier.Any())
            {
                var newFrontier = new List<(int x, int y)>();

                foreach (var p1 in frontier)
                {
                    foreach (var set in Sets.ContainingSets(p1))
                    {
                        foreach (var subSet in BitCounter.PowerSet(set, 1, _n - 1))
                        {
                            var intersect = puzzle.BitDomain(subSet.First());
                            foreach (var i in subSet.Skip(1)) intersect |= puzzle.BitDomain(i);

                            if (BitCounter.Count(intersect) <= subSet.Count)
                            {
                                foreach (var p2 in set.Where(p => !subSet.Contains(p)))
                                {
                                    if (puzzle.DomainMinus(p2, intersect))
                                    {
                                        newFrontier.Add(p2);
                                    }
                                }
                            }
                        }
                    }
                }

                frontier = newFrontier.Distinct().ToList();
            }

            return puzzle;
        }
    }
}
