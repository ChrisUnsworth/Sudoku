using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

using Sudoku;

namespace SudokuTests
{
    [TestClass()]
    public class NumbersTests
    {
        [TestMethod()]
        public void AsIntTest()
        {
            Assert.AreEqual(1, Numbers.One.AsInt());
            Assert.AreEqual(2, Numbers.Two.AsInt());
            Assert.AreEqual(3, Numbers.Three.AsInt());
            Assert.AreEqual(4, Numbers.Four.AsInt());
            Assert.AreEqual(5, Numbers.Five.AsInt());
            Assert.AreEqual(6, Numbers.Six.AsInt());
            Assert.AreEqual(7, Numbers.Seven.AsInt());
            Assert.AreEqual(8, Numbers.Eight.AsInt());
            Assert.AreEqual(9, Numbers.Nine.AsInt());
            Assert.AreEqual(0, Numbers.Any.AsInt());
        }
        [TestMethod()]
        public void AsNumbersTest()
        {
            Assert.AreEqual(Numbers.One, 1.AsNumbers());
            Assert.AreEqual(Numbers.Two, 2.AsNumbers());
            Assert.AreEqual(Numbers.Three, 3.AsNumbers());
            Assert.AreEqual(Numbers.Four, 4.AsNumbers());
            Assert.AreEqual(Numbers.Five, 5.AsNumbers());
            Assert.AreEqual(Numbers.Six, 6.AsNumbers());
            Assert.AreEqual(Numbers.Seven, 7.AsNumbers());
            Assert.AreEqual(Numbers.Eight, 8.AsNumbers());
            Assert.AreEqual(Numbers.Nine, 9.AsNumbers());
            Assert.AreEqual(null, 0.AsNumbers());
        }
    }
}
