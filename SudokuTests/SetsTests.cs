using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku.common;

namespace SudokuTests
{
    [TestClass()]
    public class SetsTests
    {
        [TestMethod()]
        public void AllCountTest()
        {
            var all = Sets.All.ToList();

            all.ForEach(p => {
                Assert.IsTrue(p.x >= 0);
                Assert.IsTrue(p.x <= 8);
                Assert.IsTrue(p.y >= 0);
                Assert.IsTrue(p.y <= 8);
            });

            Assert.AreEqual(all.Distinct().Count(), 9 * 9);
        }


        [TestMethod()]
        public void RowContainsTest()
        {
            foreach (var p1 in Sets.All)
            {
                Assert.IsNotNull(Sets.RowContaining(p1).SingleOrDefault(p2 => p1.x == p2.x && p1.y == p2.y));
            }
        }


        [TestMethod()]
        public void ColumnContainsTest()
        {
            foreach (var p1 in Sets.All)
            {
                Assert.IsNotNull(Sets.ColumnContaining(p1).SingleOrDefault(p2 => p1.x == p2.x && p1.y == p2.y));
            }
        }


        [TestMethod()]
        public void SquareContainsTest()
        {
            foreach (var p1 in Sets.All)
            {
                Assert.IsNotNull(Sets.SquareContaining(p1).SingleOrDefault(p2 => p1.x == p2.x && p1.y == p2.y));
            }
        }


        [TestMethod()]
        public void AllSetsContainTest()
        {
            foreach (var p1 in Sets.All)
            {
                foreach (var set in Sets.ContainingSets(p1))
                {
                    Assert.IsNotNull(set.SingleOrDefault(p2 => p1.x == p2.x && p1.y == p2.y));
                }
            }
        }
    }
}
