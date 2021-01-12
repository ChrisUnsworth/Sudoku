using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tests
{
    [TestClass()]
    public class SearchStateTests
    {
        [TestMethod()]
        public void SearchStateTest()
        {
            var freahState = new SearchState();
            Assert.IsNotNull(freahState);
        }

        [TestMethod()]
        public void IndexerTest()
        {
            var s = new SearchState();

            Assert.IsNull(s[8, 8]);

            s[8, 8] = 6;

            Assert.AreEqual(6, s[8, 8]);

            Assert.IsNull(s[2, 5]);

            s[2, 5] = 9;

            Assert.AreEqual(9, s[2, 5]);
        }

        [TestMethod()]
        public void CanBeTest()
        {
            var s = new SearchState();

            foreach (var i in Enumerable.Range(1, 9))
            {
                Assert.IsTrue(s.CanBe(8, 8, i), $"Can't be {i} : {Math.Pow(2, i)}");
            }

            s[8, 8] = 6;


            foreach (var i in Enumerable.Range(1, 9).Where(n => n != 6))
            {
                Assert.IsFalse(s.CanBe(8, 8, i), $"Can be {i}");
            }

            Assert.IsTrue(s.CanBe(8, 8, 6), $"Can't be 6");
        }

        [TestMethod()]
        public void SetNotTest()
        {
            var s = new SearchState();

            foreach (var i in Enumerable.Range(1, 9))
            {
                Assert.IsTrue(s.CanBe(8, 8, i), $"Can't be {i} : {Math.Pow(2, i)}");
            }

            s.SetNot(8, 8, 6);


            foreach (var i in Enumerable.Range(1, 9).Where(n => n != 6))
            {
                Assert.IsTrue(s.CanBe(8, 8, i), $"Can be {i}");
            }

            Assert.IsFalse(s.CanBe(8, 8, 6), $"Can't be 6");

            s.SetNot(8, 8, 1);
            s.SetNot(8, 8, 2);
            s.SetNot(8, 8, 3);
            s.SetNot(8, 8, 4);
            s.SetNot(8, 8, 7);
            s.SetNot(8, 8, 8);
            s.SetNot(8, 8, 9);

            Assert.AreEqual(s[8,8], 5);
        }

        [TestMethod]
        public void PrettyTest()
        {
            var s = new SearchState();

            var sb = new StringBuilder();

            sb.AppendLine(" | | ║ | | ║ | | ");
            sb.AppendLine("─┼─┼─╬─┼─┼─╬─┼─┼─");
            sb.AppendLine(" | | ║ | | ║ | | ");
            sb.AppendLine("─┼─┼─╬─┼─┼─╬─┼─┼─");
            sb.AppendLine(" | | ║ | | ║ | | ");
            sb.AppendLine("═╬═╬═╬═╬═╬═╬═╬═╬═");
            sb.AppendLine(" | | ║ | | ║ | | ");
            sb.AppendLine("─┼─┼─╬─┼─┼─╬─┼─┼─");
            sb.AppendLine(" | | ║ | | ║ | | ");
            sb.AppendLine("─┼─┼─╬─┼─┼─╬─┼─┼─");
            sb.AppendLine(" | | ║ | | ║ | | ");
            sb.AppendLine("═╬═╬═╬═╬═╬═╬═╬═╬═");
            sb.AppendLine(" | | ║ | | ║ | | ");
            sb.AppendLine("─┼─┼─╬─┼─┼─╬─┼─┼─");
            sb.AppendLine(" | | ║ | | ║ | | ");
            sb.AppendLine("─┼─┼─╬─┼─┼─╬─┼─┼─");
            sb.AppendLine(" | | ║ | | ║ | | ");

            Assert.AreEqual(sb.ToString(), s.Pretty());

            s[2, 8] = 7;
            s[3, 4] = 2;
            s[8, 4] = 1;
            s[2, 5] = 9;
            s[1, 0] = 8;
            s[2, 5] = 6;
            s[0, 5] = 3;
            s[3, 3] = 7;

            sb = new StringBuilder();

            sb.AppendLine(" |8| ║ | | ║ | | ");
            sb.AppendLine("─┼─┼─╬─┼─┼─╬─┼─┼─");
            sb.AppendLine(" | | ║ | | ║ | | ");
            sb.AppendLine("─┼─┼─╬─┼─┼─╬─┼─┼─");
            sb.AppendLine(" | | ║ | | ║ | | ");
            sb.AppendLine("═╬═╬═╬═╬═╬═╬═╬═╬═");
            sb.AppendLine(" | | ║7| | ║ | | ");
            sb.AppendLine("─┼─┼─╬─┼─┼─╬─┼─┼─");
            sb.AppendLine(" | | ║2| | ║ | |1");
            sb.AppendLine("─┼─┼─╬─┼─┼─╬─┼─┼─");
            sb.AppendLine("3| |6║ | | ║ | | ");
            sb.AppendLine("═╬═╬═╬═╬═╬═╬═╬═╬═");
            sb.AppendLine(" | | ║ | | ║ | | ");
            sb.AppendLine("─┼─┼─╬─┼─┼─╬─┼─┼─");
            sb.AppendLine(" | | ║ | | ║ | | ");
            sb.AppendLine("─┼─┼─╬─┼─┼─╬─┼─┼─");
            sb.AppendLine(" | |7║ | | ║ | | ");

            Assert.AreEqual(sb.ToString(), s.Pretty());
        }

        [TestMethod]
        public void RowIndicesTest()
        {
            var row = SearchState.RowIndices(3).ToList();

            foreach (var (_, y) in row) Assert.AreEqual(3, y);

            foreach (var i in Enumerable.Range(0, 9)) Assert.AreEqual(i, row[i].x);
        }

        [TestMethod]
        public void ColumnIndicesTest()
        {
            var row = SearchState.ColumnIndices(3).ToList();

            foreach (var (x, _) in row) Assert.AreEqual(3, x);

            foreach (var i in Enumerable.Range(0, 9)) Assert.AreEqual(i, row[i].y);
        }
    }
}