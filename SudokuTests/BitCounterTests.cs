using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku.utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuTests
{
    [TestClass()]
    public class BitCounterTests
    {
        [TestMethod()]
        public void TestCount()
        {
            Assert.AreEqual(BitCounter.Count(0), 0);
            Assert.AreEqual(BitCounter.Count(0b1101), 3);
            Assert.AreEqual(BitCounter.Count(0b1101_0011), 5);
        }
    }
}
