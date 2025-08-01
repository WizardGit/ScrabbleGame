using ScrabbleEngine;
using System.Diagnostics.Metrics;

namespace ScrabbleTests
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestTestExplorer()
        {
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void TestWord()
        {
            Word word = new Word("TestWord");
            Assert.AreEqual(word.Value, "testword");
        }

        [TestMethod]
        public void TestLetter()
        {
            Letter letter = new Letter('a');
            Assert.AreEqual(letter.Value, 'a');
        }
        [TestMethod]
        public void TestRowMatchCheck()
        {
            Board board = new Board();
            board[7, 6].Value = 'b';
            board[7, 7].Value = 'e';
            board[7, 8].Value = 'a';
            board[7, 9].Value = 'd';
            board.WordMatchRow("bead", "abcdefg", 7, 7, out List<Word> wordsMade);
            Assert.AreEqual(0, wordsMade.Count);
            board.WordMatchRow("bead", "abcdefg", 7, 6, out wordsMade);
            Assert.AreEqual(0, wordsMade.Count);
            board.WordMatchRow("beaded", "abcdefg", 7, 6, out wordsMade);
            Assert.AreEqual(1, wordsMade.Count);

            board[4, 11].Value = 'c';
            board[5, 11].Value = 'a';
            board[6, 11].Value = 'r';
            board.WordMatchRow("beaded", "abcdefg", 7, 6, out wordsMade);
            Assert.AreEqual(2, wordsMade.Count);
            Assert.AreEqual("card", wordsMade[0].Value);
            Assert.AreEqual("beaded", wordsMade[1].Value);

            board[8, 11].Value = 'e';
            board[9, 11].Value = 'd';
            board.WordMatchRow("beaded", "abcdefg", 7, 6, out wordsMade);
            Assert.AreEqual(2, wordsMade.Count);
            Assert.AreEqual("carded", wordsMade[0].Value);
            Assert.AreEqual("beaded", wordsMade[1].Value);

        }

        [TestMethod]
        public void TestRowLineCheck()
        {
            Board board = new Board();
            board[7, 6].Value = 'b';
            board[7, 7].Value = 'e';
            board[7, 8].Value = 'a';
            board[7, 9].Value = 'd';
            board.RowLineCheck("abcdefg", out List<List<Word>> wordsMade, null, 7);
            Assert.AreEqual(1, wordsMade.Count);
            Assert.AreEqual(1, wordsMade[0].Count);

            board[4, 11].Value = 'c';
            board[5, 11].Value = 'a';
            board[6, 11].Value = 'r';
            board.RowLineCheck("abcdefg", out wordsMade, null, 7);
            Assert.AreEqual(25, wordsMade.Count);
        }
    }
}
