using ScrabbleEngine;
using System.Diagnostics.Metrics;

namespace ScrabbleTests
{
    [TestClass]
    public sealed class MainFormTests
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
        public void TestGetNewWord()
        {
            Board newboard = new Board();
            Board oldBoard = new Board();
            MainForm form = new MainForm();
            List<Word> lstNewWords;

            lstNewWords = form.GetNewWord(oldBoard, newboard);
            Assert.AreEqual(0, lstNewWords.Count);

            newboard[7, 6].Value = 'b';
            newboard[7, 7].Value = 'e';
            newboard[7, 8].Value = 'a';
            newboard[7, 9].Value = 'd';
            lstNewWords = form.GetNewWord(oldBoard, newboard);
            Assert.AreEqual(1, lstNewWords.Count);
            Assert.AreEqual("bead", lstNewWords[0].Value);

            newboard[4, 9].Value = 'c';
            newboard[5, 9].Value = 'a';
            newboard[6, 9].Value = 'r';
            lstNewWords = form.GetNewWord(oldBoard, newboard);
            Assert.AreEqual(2, lstNewWords.Count);
            Assert.AreEqual("card", lstNewWords[0].Value);
            Assert.AreEqual("bead", lstNewWords[1].Value);
        }
    }
}