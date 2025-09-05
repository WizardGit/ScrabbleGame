using ScrabbleEngine;
using System;

namespace ScrabbleTests
{
    [TestClass]
    public sealed class UltWordListTests
    {
        [TestMethod]
        public void TestTestExplorer()
        {
            int testInt = 1;
            Assert.AreEqual(1, testInt);
        }

        [TestMethod]
        public void TestUltWordListCreation()
        {
            Word word1 = new Word("nice");
            Word word2 = new Word("test");
            Word word31 = new Word("Word");
            Word word32 = new Word("garbage");
            Word word4 = new Word("father");

            List<Word> words1 = new List<Word>();
            words1.Add(word1);
            List<Word> words2 = new List<Word>();
            words2.Add(word2);
            List<Word> words3 = new List<Word>();
            words3.Add(word31);
            words3.Add(word32);
            List<Word> words4 = new List<Word>();
            words4.Add(word4);

            List<List<Word>> listsWords = new List<List<Word>>();
            listsWords.Add(words1);
            listsWords.Add(words2);
            listsWords.Add(words3);
            listsWords.Add(words4);

            UltWordList thelist = new UltWordList(listsWords, false);
            Assert.AreEqual("nice", thelist.PrintWordListAt(0, false, false));
            Assert.AreEqual("word & garbage", thelist.PrintWordListAt(2, false, false));
            Assert.AreEqual(4, thelist.Length);

            thelist = new UltWordList(listsWords, true);
            Assert.AreEqual("nice", thelist.PrintWordListAt(0, false, false));
            Assert.AreEqual(1, thelist.Length);

            thelist = new UltWordList(words3, false);
            Assert.AreEqual("word & garbage", thelist.PrintWordListAt(0, false, false));
            Assert.AreEqual(1, thelist.Length);

            thelist = new UltWordList(words3, true);
            Assert.AreEqual("word", thelist.PrintWordListAt(0, false, false));
            Assert.AreEqual("garbage", thelist.PrintWordListAt(1, false, false));
            Assert.AreEqual(2, thelist.Length);

            thelist = new UltWordList();
            Assert.AreEqual(0, thelist.Length);
        }

        [TestMethod]
        public void TestPointsAtIndexMethods()
        {
            Word word1 = new Word("nice");
            Word word2 = new Word("test");
            Word word31 = new Word("Word");
            Word word32 = new Word("garbage");
            Word word4 = new Word("father");

            List<Word> words1 = new List<Word>();
            words1.Add(word1);
            List<Word> words2 = new List<Word>();
            words2.Add(word2);
            List<Word> words3 = new List<Word>();
            words3.Add(word31);
            words3.Add(word32);
            List<Word> words4 = new List<Word>();
            words4.Add(word4);

            List<List<Word>> listsWords = new List<List<Word>>();
            listsWords.Add(words1);
            listsWords.Add(words2);
            listsWords.Add(words3);
            listsWords.Add(words4);

            UltWordList thelist = new UltWordList(listsWords, false);
            Assert.AreEqual(6, thelist.PointsAt(0));
            Assert.AreEqual(4, thelist.PointsAt(1));
            Assert.AreEqual(19, thelist.PointsAt(2));
            Assert.AreEqual(12, thelist.PointsAt(3));

            thelist.SortPoints(true);
            Assert.AreEqual("test", thelist.PrintWordListAt(0, false, false));
            Assert.AreEqual("father", thelist.PrintWordListAt(2, false, false));
            thelist.SortPoints(false);
            Assert.AreEqual("father", thelist.PrintWordListAt(1, false, false));
            Assert.AreEqual("test", thelist.PrintWordListAt(3, false, false));
        }

        [TestMethod]
        public void TestPointsAtBoardMethods()
        {
            Word word1 = new Word("bead");
            word1.SetAsRow();
            word1.ColumnIndex = 6;
            word1.RowIndex = 7;
            Word word2 = new Word("beaded");
            word2.SetAsRow();
            word2.ColumnIndex = 6;
            word2.RowIndex = 7;
            Word word3 = new Word("card");
            word3.SetAsColumn();
            word3.ColumnIndex = 9;
            word3.RowIndex = 4;
            Word word4 = new Word("carded");
            word4.SetAsColumn();
            word4.ColumnIndex = 10;
            word4.RowIndex = 3;

            List<Word> words1 = new List<Word>();
            words1.Add(word1);
            List<Word> words2 = new List<Word>();
            words2.Add(word2);
            List<Word> words3 = new List<Word>();
            words3.Add(word3);
            words3.Add(word4);

            List<List<Word>> listsWords = new List<List<Word>>();
            listsWords.Add(words1);
            listsWords.Add(words2);
            listsWords.Add(words3);

            UltWordList thelist = new UltWordList(listsWords, false);

            Board board = new Board();
            board[7, 6].Value = 'b';
            board[7, 7].Value = 'e';
            board[7, 8].Value = 'a';
            board[7, 9].Value = 'd';

            board[4, 9].Value = 'c';
            board[5, 9].Value = 'a';
            board[6, 9].Value = 'r';

            board[3, 10].Value = 'c';
            board[4, 10].Value = 'a';
            board[5, 10].Value = 'r';
            board[6, 10].Value = 'd';
            board[7, 10].Value = 'e';
            board[7, 11].Value = 'd';
            board[8, 10].Value = 'd';

            Assert.AreEqual(7, thelist.PointsAt(0, board));
            //letter a falls on triple letter
            Assert.AreEqual(12, thelist.PointsAt(1, board));
            //letter a falls on double word
            Assert.AreEqual(9 + 20, thelist.PointsAt(2, board));

            thelist.SortPoints(true);
            Assert.AreEqual("bead", thelist.PrintWordListAt(0, false, false));
            Assert.AreEqual("beaded", thelist.PrintWordListAt(1, false, false));
            thelist.SortPoints(false);
            Assert.AreEqual("bead", thelist.PrintWordListAt(2, false, false));
            Assert.AreEqual("beaded", thelist.PrintWordListAt(1, false, false));
        }
    }
}
