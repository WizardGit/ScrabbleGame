using ScrabbleEngine;

namespace ScrabbleTests
{
    [TestClass]
    public sealed class WordTests
    {
        [TestMethod]
        public void TestTestExplorer()
        {
            int testInt = 1;
            Assert.AreEqual(1, testInt);
        }

        [TestMethod]
        public void TestWordCreation()
        {
            Word word = new Word("testWord");
            Assert.AreEqual("testword", word.Value);
            Assert.AreEqual(12, word.Points);
            Assert.AreEqual('t', word[0]);
            Assert.AreEqual('w', word[4]);
            Assert.AreEqual('d', word[7]);
            Assert.AreEqual(false, word.IsEmpty());
            Assert.AreEqual(8, word.Length);

            word = new Word("");
            Assert.AreEqual("", word.Value);
            Assert.AreEqual(0, word.Points);
            Assert.AreEqual(true, word.IsEmpty());

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                char letter = word[0];
            });
        }

        [TestMethod]
        public void TestBasicWordMethods()
        {
            Word word = new Word("testWord");

            Assert.AreEqual(false, word.isRow);
            Assert.AreEqual(false, word.isColumn);
            word.SetAsColumn();
            Assert.AreEqual(false, word.isRow);
            Assert.AreEqual(true, word.isColumn);
            word.SetAsRow();
            Assert.AreEqual(true, word.isRow);
            Assert.AreEqual(false, word.isColumn);
        }

        [TestMethod]
        public void TestPrintWord()
        {
            Word word = new Word("testWord");

            Assert.AreEqual("testword", word.PrintWord(false, false));
            Assert.AreEqual("testword (12)", word.PrintWord(true, false));
            Assert.AreEqual("testword []", word.PrintWord(false, true));
            Assert.AreEqual("testword [] (12)", word.PrintWord(true, true));
                        
            Assert.AreEqual(12, word.CalculatePoints());

            word = new Word("");

            Assert.AreEqual("", word.PrintWord(false, false));
            Assert.AreEqual(" (0)", word.PrintWord(true, false));
            Assert.AreEqual(" []", word.PrintWord(false, true));
            Assert.AreEqual(" [] (0)", word.PrintWord(true, true));

            Assert.AreEqual(0, word.CalculatePoints());

            word = new Word("quOtIent");
            word.RowIndex = 2;

            Assert.AreEqual("quotient", word.PrintWord(false, false));
            Assert.AreEqual("quotient (17)", word.PrintWord(true, false));
            Assert.AreEqual("quotient [2]", word.PrintWord(false, true));
            word.ColumnIndex = 9;
            Assert.AreEqual("quotient [2, 9]", word.PrintWord(false, true));
            Assert.AreEqual("quotient [2, 9] (17)", word.PrintWord(true, true));

            Assert.AreEqual(17, word.CalculatePoints());
        }

        [TestMethod]
        public void TestWordsInList()
        {
            Word word = new Word("testWord");
            Word word1 = new Word("quotient");
            Word word2 = new Word("uncle");
            Word word3 = new Word("fellow");

            List<Word> testWordList = new List<Word>();
            testWordList.Add(word1);
            testWordList.Add(word2);
            testWordList.Add(word3);  

            Assert.AreEqual(false, word.InList(testWordList));
            Assert.AreEqual(true, word3.InList(testWordList));
            word.AddToList(ref testWordList, false);
            Assert.AreEqual(true, word.InList(testWordList));

            Assert.AreEqual(4, testWordList.Count);
            word.AddToList(ref testWordList, true);
            Assert.AreEqual(4, testWordList.Count);
        }

        [TestMethod]
        public void TestLetterMethods()
        {
            Word word = new Word("");

            string testLetters = "-------";
            Assert.AreEqual(true, word.OneLetterUsed(testLetters));
            testLetters = "gsdfgoc";            
            Assert.AreEqual(false, word.OneLetterUsed(testLetters));
            testLetters = "asdfgo-";
            Assert.AreEqual(true, word.OneLetterUsed(testLetters));
            testLetters = "-sdfgoc";
            Assert.AreEqual(true, word.OneLetterUsed(testLetters));

            Assert.AreEqual(true, word.RemoveLetter('c', ref testLetters));
            Assert.AreEqual("-sdfgo-", testLetters);
            Assert.AreEqual(false, word.RemoveLetter('a', ref testLetters));
            Assert.AreEqual("-sdfgo-", testLetters);
            testLetters = "*sdf*o-";
            Assert.AreEqual(true, word.RemoveLetter('a', ref testLetters));
            Assert.AreEqual("-sdf*o-", testLetters);
            Assert.AreEqual(true, word.RemoveLetter('a', ref testLetters));
            Assert.AreEqual("-sdf-o-", testLetters);

            Assert.ThrowsException<Exception>(() =>
            {
                word.RemoveLetter('&', ref testLetters);
            });
        }
    }
}
