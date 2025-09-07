using ScrabbleEngine;
using System;

namespace ScrabbleTests
{
    [TestClass]
    public sealed class Linetests
    {
        [TestMethod]
        public void TestTestExplorer()
        {
            int testInt = 1;
            Assert.AreEqual(1, testInt);
        }

        [TestMethod]
        public void TestLine()
        {
            Line line = new Line();
            Assert.AreEqual(0, line.Length);
            Assert.AreEqual("", line.PrintLine());
            Assert.AreEqual(true, line.IsEmpty());

            line = new Line("abcde--");
            Assert.AreEqual(7, line.Length);
            Assert.AreEqual("abcde--", line.PrintLine());
            Assert.AreEqual(false, line.IsEmpty());

            Square square = new Square(Square.BonusType.nothing, 'c');
            line.AddSquare(square);
            Assert.AreEqual(8, line.Length);
            Assert.AreEqual("abcde--c", line.PrintLine());
            Assert.AreEqual(false, line.IsEmpty());
            Assert.AreEqual(square, line.GetSquare(7));
            Assert.AreEqual('b', line[1]);
            Assert.AreEqual('-', line[5]);
        }

        [TestMethod]
        public void TestLineChecks()
        {
            Line line = new Line("-------");
            
            line.SimpleLineCheck("abcdefg", out List<Word> lineCheckWords2, null);
            Assert.AreEqual(64, lineCheckWords2.Count);
            line.LineCheck("abcdefg", out List<Word> lineCheckWords1, null);
            Assert.AreEqual(0, lineCheckWords1.Count);

            line = new Line("a------");
            line.SimpleLineCheck("bcdefg", out lineCheckWords2, null);
            Assert.AreEqual(9, lineCheckWords2.Count);
            line.LineCheck("bcdefg", out lineCheckWords1, null);
            Assert.AreEqual(9, lineCheckWords1.Count);

            line = new Line("--c----");
            line.SimpleLineCheck("badefg", out lineCheckWords2, null);
            Assert.AreEqual(5, lineCheckWords2.Count);
            line.LineCheck("abdefg", out lineCheckWords1, null);
            Assert.AreEqual(17, lineCheckWords1.Count);

            line = new Line("------g");
            line.LineCheck("abcdef", out lineCheckWords1, null);
            Assert.AreEqual(9, lineCheckWords1.Count);
        }
    }
}
