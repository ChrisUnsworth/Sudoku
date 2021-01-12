using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;

namespace SudokuTests
{
    [TestClass()]
    public class PowerSetTests
    {
        [TestMethod()]
        public void AllSets()
        {
            var list = new List<int> { 1, 2, 3 };

            var ps = new PowerSet<int>(list);            

            Assert.AreEqual(8, ps.Count());

            var all = ps.ToList();

            Assert.IsNotNull(all.SingleOrDefault(l => l.Count == 0));
        }
    }
}
