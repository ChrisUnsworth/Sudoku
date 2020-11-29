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
        public void ArrayConstructorTest()
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

            var sb = new StringBuilder();

            sb.AppendLine("3|8|2║9| | ║ | |1");
            sb.AppendLine("─┼─┼─╬─┼─┼─╬─┼─┼─");
            sb.AppendLine(" | | ║ | | ║ |5|2");
            sb.AppendLine("─┼─┼─╬─┼─┼─╬─┼─┼─");
            sb.AppendLine(" |1| ║ |2|7║3| | ");
            sb.AppendLine("═╬═╬═╬═╬═╬═╬═╬═╬═");
            sb.AppendLine(" | | ║ |4| ║ |2|7");
            sb.AppendLine("─┼─┼─╬─┼─┼─╬─┼─┼─");
            sb.AppendLine("8| | ║2| |9║ | |5");
            sb.AppendLine("─┼─┼─╬─┼─┼─╬─┼─┼─");
            sb.AppendLine("2|4| ║ |6| ║ | | ");
            sb.AppendLine("═╬═╬═╬═╬═╬═╬═╬═╬═");
            sb.AppendLine(" | |8║4|7| ║ |1| ");
            sb.AppendLine("─┼─┼─╬─┼─┼─╬─┼─┼─");
            sb.AppendLine("5|2| ║ | | ║ | | ");
            sb.AppendLine("─┼─┼─╬─┼─┼─╬─┼─┼─");
            sb.AppendLine("7| | ║ | |8║2|9|4");

            var pretty = s.Pretty();

            Assert.AreEqual(sb.ToString(), pretty);
        }

        [TestMethod]
        public void AsArrayTest()
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

            var array = s.ToArray();

            foreach (var i in Enumerable.Range(0, 9))
            {
                foreach (var j in Enumerable.Range(0, 9))
                {
                    Assert.AreEqual(data[i, j], array[i, j]);
                }
            }
        }
    }
}