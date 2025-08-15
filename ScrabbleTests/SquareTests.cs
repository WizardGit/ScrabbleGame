using ScrabbleEngine;
using System.Diagnostics.Metrics;

namespace ScrabbleTests
{
    [TestClass]
    public sealed class SquareTests
    {
        [TestMethod]
        public void TestTestExplorer()
        {
            int testInt = 1;
            Assert.AreEqual(1, testInt);
        }

        [TestMethod]
        public void TestCalculatePoints()
        {
            Square square = new Square();
            int multFactor = 1;

            Assert.ThrowsException<Exception>(() => square.Points(ref multFactor));

            square.Value = 'a';
            Assert.AreEqual('a', square.Value);
            square.Value = 'A';
            Assert.AreEqual('a', square.Value);
            Assert.AreEqual(1, square.Points(ref multFactor));
            Assert.AreEqual(1, multFactor);

            Assert.ThrowsException<Exception>(() => square.Value = '%');

            square.Bonus = Square.BonusType.tripleLetter;
            Assert.AreEqual(3, square.Points(ref multFactor));
            square.Bonus = Square.BonusType.doubleWord;
            Assert.AreEqual(1, square.Points(ref multFactor));
            Assert.AreEqual(2, multFactor);
        }

        [TestMethod]
        public void TestRandomPoints()
        {
            Square square = new Square();

            Letter letter = new Letter('b');
            Assert.AreEqual(true, square.RemoveLetter(letter));
            Assert.AreEqual(false, square.IsValid('b'));
            Assert.AreEqual(true, square.IsValid('a'));
            letter = new Letter('a');
            Assert.AreEqual(true, square.RemoveLetter(letter));
            Assert.AreEqual(false, square.IsValid('a'));
            Assert.AreEqual(true, square.IsValid('*'));
            Assert.AreEqual(false, square.IsValid('-'));
            Assert.AreEqual(false, square.RemoveLetter(letter));
            letter = new Letter('*');
            Assert.AreEqual(false, square.RemoveLetter(letter));

        }
    }
}
