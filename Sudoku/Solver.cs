using Decider.Csp.BaseTypes;
using Decider.Csp.Global;
using Decider.Csp.Integer;
using Sudoku.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku
{
    public class Solver
    {
        private List<VariableInteger> _variables;
        private List<IConstraint> _constraints;
        private ISearchState _start;
        private IState _solution;
        private bool _solutionIsUnique;
        private bool _solved = false;

        public Solver(ISearchState start)
        {
            _start = start;
            _variables = Enumerable.Repeat((VariableInteger)null, 9 * 9).ToList();
            SetVariables();
            _constraints = new List<IConstraint>();
            AddAllDiffConstraints();
            AddState();
        }

        public ISearchState Start => _start;

        public IState Solution
        {
            get
            {
                if (!_solved) Solve();
                return _solution;
            }
        }

        public bool HasSolution => Solution != null;

        public bool HasUniqueSolution => HasSolution && _solutionIsUnique;

        private void Solve()
        {
            if (_solved) return;
            var it = GetSolutions().GetEnumerator();
            if (it.MoveNext())
            {
                _solution = it.Current;
                _solutionIsUnique = !it.MoveNext();
            }

            _solved = true;
        }

        private VariableInteger this[int x, int y]
        {
            get => _variables[x + y * 9];
            set => _variables[x + y * 9] = value;
        }

        private void SetVariables()
        {
            foreach (var x in Enumerable.Range(0, 9))
            {
                foreach (var y in Enumerable.Range(0, 9))
                {
                    this[x, y] = new VariableInteger($"{x}_{y}", 1, 9);
                }
            }
        }

        private void AddAllDiffConstraints()
        {
            // Rows
            foreach (var y in Enumerable.Range(0, 9))
            {
                var vars = Enumerable.Range(0, 9)
                                     .Select(x => this[x, y])
                                     .ToList();
                _constraints.Add(new AllDifferentInteger(vars));
            }

            // Columns
            foreach (var x in Enumerable.Range(0, 9))
            {
                var vars = Enumerable.Range(0, 9)
                                     .Select(y => this[x, y])
                                     .ToList();
                _constraints.Add(new AllDifferentInteger(vars));
            }

            // squares
            
            foreach (var s_x in Enumerable.Range(0, 3))
            {
                foreach (var s_y in Enumerable.Range(0, 3))
                {
                    var vars = Enumerable.Range(0, 3)
                                         .SelectMany(x => Enumerable.Range(0, 3).Select(y => (x, y)))
                                         .Select(p => this[p.x + s_x * 3, p.y + s_y * 3])
                                         .ToList();
                    _constraints.Add(new AllDifferentInteger(vars));
                }
            }
        }

        private void AddState()
        {
            foreach (var x in Enumerable.Range(0, 9))
            {
                foreach (var y in Enumerable.Range(0, 9))
                {
                    var val = _start[x, y];

                    if (val.HasValue)
                    {
                        _constraints.Add(new ConstraintInteger(val.Value == this[x, y]));
                    }
                }
            }
        }

        private IEnumerable<SearchState> GetSolutions()
        {
            IState<int> state = new StateInteger(_variables, _constraints);

            state.StartSearch(out StateOperationResult searchResult, out IList<IDictionary<string, IVariable<int>>> solutions);

            if (searchResult != StateOperationResult.Solved) yield break;

            foreach (var result in solutions)
            {
                var solution = new SearchState();

                foreach (var x in Enumerable.Range(0, 9))
                {
                    foreach (var y in Enumerable.Range(0, 9))
                    {
                        solution[x, y] = result[$"{x}_{y}"].InstantiatedValue;
                    }
                }

                yield return solution;
            }
        }
    }
}
