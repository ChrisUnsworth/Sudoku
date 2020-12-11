using Sudoku.common;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    public class ForwardChecking : ISolver
    {
        public IState Solve(IState puzzle) => Solve(new SearchState(puzzle));

        public IState Solve(ISearchState puzzle)
        {
            var frontier = Sets.All.Where(p => puzzle[p].HasValue).ToList();

            while (frontier.Any())
            {
                var newFrontier = new List<(int x, int y)>();

                foreach (var p1 in frontier)
                {
                    var val = puzzle[p1].Value;
                    foreach (var p2 in Sets.ContainingSets(p1).SelectMany(x => x).Distinct().Where(x => x != p1))
                    {
                        if (!puzzle[p2].HasValue)
                        {
                            puzzle.SetNot(p2.x, p2.y, val);
                            if (puzzle[p2].HasValue) newFrontier.Add(p2);
                        }
                    }
                }

                frontier = newFrontier;
            }

            return puzzle;
        }
    }
}
