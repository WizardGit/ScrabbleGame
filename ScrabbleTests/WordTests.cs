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
            word = new Word("");
            Assert.AreEqual("", word.Value);
            Assert.AreEqual(0, word.Points);
        }
    }
}
