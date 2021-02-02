using System;
using System.Linq;
using System.Collections.Generic;

using Sudoku.common;

namespace Sudoku
{
    public class Generator
    {
        public int MinClues { get; set; } = 25;

        private List<ISolver> _solvers = new List<ISolver>
        {
            new ForwardChecking(),
            new NConsistency(3),
            new NConsistency(4),
            new NConsistency(5),
            new NConsistency(6),
            new NConsistency(7),
            new NConsistency(8),
            new NConsistency(9)
        };

        private Random _rand;

        public Generator() : this(new Random()) { }

        public Generator(Random rand) => _rand = rand;

        public (int, IState) Next()
        {
            ISearchState s = new SearchState();
            var unset = Sets.All.ToList();
            var hasUniqueSolution = false;
            var count = 0;

            while (count < MinClues || !hasUniqueSolution)
            {
                var i = _rand.Next(unset.Count);
                var domain = s.Domain(unset[i]).ToList();
                s[unset[i]] = domain[_rand.Next(domain.Count)];
                unset.RemoveAt(i);
                s = _solvers.First().Solve(s);

                if (++count >= MinClues)
                {
                    var solver = new Solver(s);
                    if (!solver.HasSolution) return Next();
                    hasUniqueSolution = solver.HasUniqueSolution;
                }
            }

            return (DifficultyOf(s), s.Copy());
        }

        public bool TryBuild(int difficulty, out IState problem)
        {
            if (difficulty < 0 || difficulty >= _solvers.Count) throw new ArgumentOutOfRangeException();

            int i;
            (i, problem) = Next();

            if (i == difficulty) return true;

            return i < difficulty
                ? TryMakeHarder(difficulty, problem as ISearchState, ref problem)
                : TryMakeEasier(difficulty, problem as ISearchState, ref problem);
        }

        private bool TryMakeHarder(int target, ISearchState seed, ref IState problem)
        {
            var setSquares = Sets.All.Where(x => seed[x].HasValue);

            foreach (var x in setSquares)
            {
                var copy = seed.Copy();
                copy.Reset(x);
                var dif = DifficultyOf(copy);
                if (dif == target)
                {
                    problem = copy;
                    return true;
                }

                if (dif < target && TryMakeHarder(target, copy, ref problem)) return true;
            }

            return false;
        }

        private bool TryMakeEasier(int target, ISearchState seed, ref IState problem)
        {
            var unsetSquares = Sets.All.Where(x => !seed[x].HasValue);
            var solution = _solvers.Last().Solve(seed.Copy());

            if (!solution.IsSolution) return false;

            foreach (var x in unsetSquares)
            {
                var copy = seed.Copy();
                copy[x] = solution[x];
                var dif = DifficultyOf(copy);
                if (dif == target)
                {
                    problem = copy;
                    return true;
                }

                if (dif > target && TryMakeEasier(target, copy, ref problem)) return true;
            }

            return false;
        }

        public int DifficultyOf(ISearchState puzzle)
        {
            var s = puzzle.Copy();
            foreach (var idx in Enumerable.Range(0, _solvers.Count))
            {
                if (_solvers[idx].Solve(s).IsSolution)
                {
                    return idx;
                }
            }

            return _solvers.Count;
        }
    }
}
