using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;
using Sudoku.common;
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

            var (difficulty, puzzle) = gen.Next();

            Assert.AreEqual(2, difficulty);

            Assert.IsNotNull(puzzle);
        }



        [TestMethod()]
        public void Trybuild0()
        {
            var gen = new Generator(new Random(756756));

            var result = gen.TryBuild(0, out IState puzzle);

            Assert.IsTrue(result);

            Assert.IsNotNull(puzzle);
        }



        [TestMethod()]
        public void Trybuild1()
        {
            var gen = new Generator(new Random(756756));

            var result = gen.TryBuild(1, out IState puzzle);

            Assert.IsTrue(result);

            Assert.IsNotNull(puzzle);
        }



        [TestMethod()]
        public void Trybuild3()
        {
            var gen = new Generator(new Random(756756));

            var result = gen.TryBuild(3, out IState puzzle);

            Assert.IsTrue(result);

            Assert.IsNotNull(puzzle);
        }
    }
}
