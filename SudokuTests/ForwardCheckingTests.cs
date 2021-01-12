using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;
using Sudoku.common;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SudokuTests
{
    [TestClass()]
    public class ForwardCheckingTests
    {
        [TestMethod()]
        public void EmptyStateDoesNothingTest()
        {
            var s = new SearchState();
            var fc = new ForwardChecking();

            var result = fc.Solve(s.Copy());

            foreach (var (x, y) in Sets.All)
            {
                Assert.AreEqual(s.BitDomain(x, y), result.BitDomain(x, y));
            }
        }

        [TestMethod()]
        public void SingleAssignmentTest()
        {
            var s = new SearchState();

            var idx = (3, 3);
            s[idx] = 4;

            var fc = new ForwardChecking();

            var result = fc.Solve(s.Copy());
            var neighbours = Sets.ContainingSets(idx).SelectMany(x => x).Distinct().ToHashSet();

            foreach (var x in Sets.All)
            {
                if (x == idx)
                {
                    Assert.AreEqual(4, result[x]);
                }
                else if (neighbours.Contains(x))
                {
                    Assert.AreNotEqual(s.BitDomain(x), result.BitDomain(x));
                    Assert.IsFalse(result.CanBe(x, 4));
                }
                else
                {
                    Assert.AreEqual(s.BitDomain(x), result.BitDomain(x));
                }
            }
        }

        [TestMethod()]
        public void TwoAssignmentTest()
        {
            var s = new SearchState();

            var idx1 = (3, 3);
            var idx2 = (2, 2);
            s[idx1] = 4;
            s[idx2] = 7;

            var fc = new ForwardChecking();

            var result = fc.Solve(s.Copy());
            var neighbours1 = Sets.ContainingSets(idx1).SelectMany(x => x).Distinct().ToHashSet();
            var neighbours2 = Sets.ContainingSets(idx2).SelectMany(x => x).Distinct().ToHashSet();
            var intersect = neighbours1.Where(neighbours2.Contains).ToHashSet();

            foreach (var x in Sets.All)
            {
                if (x == idx1)
                {
                    Assert.AreEqual(4, result[x]);
                }
                else if (x == idx2)
                {
                    Assert.AreEqual(7, result[x]);
                }
                else if (intersect.Contains(x))
                {
                    Assert.AreNotEqual(s.BitDomain(x), result.BitDomain(x));
                    Assert.IsFalse(result.CanBe(x, 4));
                    Assert.IsFalse(result.CanBe(x, 7));
                }
                else if (neighbours1.Contains(x))
                {
                    Assert.AreNotEqual(s.BitDomain(x), result.BitDomain(x));
                    Assert.IsFalse(result.CanBe(x, 4));
                }
                else if (neighbours2.Contains(x))
                {
                    Assert.AreNotEqual(s.BitDomain(x), result.BitDomain(x));
                    Assert.IsFalse(result.CanBe(x, 7));
                }
                else
                {
                    Assert.AreEqual(s.BitDomain(x), result.BitDomain(x));
                }
            }
        }
    }
}
