using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuTests
{
    [TestClass()]
    public class SolverTests
    {
        [TestMethod()]
        public void SimpleSolveTest()
        {
            var data = new int[,]
            {
                { 3, 8, 2, 9, 0, 0, 0, 0, 1 },
                { 0, 0, 0, 0, 0, 0, 0, 5, 2 },
                { 0, 1, 0, 0, 2, 7, 3, 0, 0 },
                { 0, 0, 0, 0, 4, 0, 0, 2, 7 },
                { 8, 0, 0, 2, 0, 9, 0, 0, 5 },
                { 2, 4, 0, 0, 6, 0, 0, 0, 0 },
                { 0, 0, 8, 4, 7, 0, 0, 1, 0 },
                { 5, 2, 0, 0, 0, 0, 0, 0, 0 },
                { 7, 0, 0, 0, 0, 8, 2, 9, 4 }
            };

            var s = new SearchState(data);

            var solver = new Solver(s);

            Assert.IsTrue(solver.HasSolution);
            Assert.IsTrue(solver.HasUniqueSolution);

            var solution = solver.Solution.ToArray();

            var expected = new int[,]
            {
                { 3, 8, 2, 9, 5, 4, 7, 6, 1 },
                { 6, 9, 7, 3, 8, 1, 4, 5, 2 },
                { 4, 1, 5, 6, 2, 7, 3, 8, 9 },
                { 1, 5, 6, 8, 4, 3, 9, 2, 7 },
                { 8, 7, 3, 2, 1, 9, 6, 4, 5 },
                { 2, 4, 9, 7, 6, 5, 1, 3, 8 },
                { 9, 3, 8, 4, 7, 2, 5, 1, 6 },
                { 5, 2, 4, 1, 9, 6, 8, 7, 3 },
                { 7, 6, 1, 5, 3, 8, 2, 9, 4 }
            };

            foreach (var i in Enumerable.Range(0, 9))
            {
                foreach (var j in Enumerable.Range(0, 9))
                {
                    Assert.AreEqual(expected[i, j], solution[i, j]);
                }
            }
        }
    }
}
