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
            int testInt = 1;
            Assert.AreEqual(1, testInt);
        }

        [TestMethod]
        public void TestWord()
        {
            Assert.AreEqual("testword", new Word("TestWord").Value);
        }

        [TestMethod]
        public void TestLetter()
        {
            Assert.AreEqual('a', new Letter('a').Value);
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

            board.RowLineCheck("abcdefg", out wordsMade, null, 8);
            Assert.AreEqual(125, wordsMade.Count);
            Assert.AreEqual(2, wordsMade[2].Count);
            Assert.AreEqual("be", wordsMade[2][0].Value);
            Assert.AreEqual("badge", wordsMade[2][1].Value);
        }

        [TestMethod]
        public void TestGetWord()
        {
            Board board = new Board();
            board[7, 6].Value = 'b';
            board[7, 7].Value = 'e';
            board[7, 8].Value = 'a';
            board[7, 9].Value = 'd';
            board.GetWord(board, 7, 6, out Word pColWord, out Word pRowWord);
            Assert.AreEqual("", pColWord.Value);
            Assert.AreEqual("bead", pRowWord.Value);

            board[4, 9].Value = 'c';
            board[5, 9].Value = 'a';
            board[6, 9].Value = 'r';
            board.GetWord(board, 7, 9, out pColWord, out pRowWord);
            Assert.AreEqual("card", pColWord.Value);
            Assert.AreEqual("bead", pRowWord.Value);

            board[3, 10].Value = 'c';
            board[4, 10].Value = 'a';
            board[5, 10].Value = 'r';
            board[6, 10].Value = 'd';
            board[7, 10].Value = 'e';
            board[7, 11].Value = 'd';
            board[8, 10].Value = 'd';
            board.GetWord(board, 7, 10, out pColWord, out pRowWord);
            Assert.AreEqual("carded", pColWord.Value);
            Assert.AreEqual("beaded", pRowWord.Value);
        }

        [TestMethod]
        public void TestGetNewWord()
        {
            Board board1 = new Board();
            Board board2 = new Board();
            List<Word> lstNewWords;

            lstNewWords = board1.GetNewWord(board2);
            Assert.AreEqual(0, lstNewWords.Count);

            board1[7, 6].Value = 'b';
            board1[7, 7].Value = 'e';
            board1[7, 8].Value = 'a';
            board1[7, 9].Value = 'd';
            lstNewWords = board2.GetNewWord(board1);
            Assert.AreEqual(1, lstNewWords.Count);
            Assert.AreEqual("bead", lstNewWords[0].Value);

            board1[4, 9].Value = 'c';
            board1[5, 9].Value = 'a';
            board1[6, 9].Value = 'r';
            lstNewWords = board2.GetNewWord(board1);
            Assert.AreEqual(2, lstNewWords.Count);
            Assert.AreEqual("card", lstNewWords[0].Value);
            Assert.AreEqual("bead", lstNewWords[1].Value);
        }
    }
}
