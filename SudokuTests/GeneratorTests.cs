using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;
using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuTests
{
    [TestClass()]
    public class GeneratorTests
    {
        [TestMethod()]
        public void GenTest()
        {
            var gen = new Generator(new Random(42));

            var puzzle = gen.Next();

            Assert.IsNotNull(puzzle);
        }

        [TestMethod()]
        public void GenTest2()
        {
            var gen = new Generator(new Random(756756));

            var puzzle = gen.Next();

            Assert.IsNotNull(puzzle);
        }
    }
}
