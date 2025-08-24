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
    }
}
