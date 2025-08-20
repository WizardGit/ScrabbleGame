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
    }
}
