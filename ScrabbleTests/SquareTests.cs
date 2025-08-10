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
        public void TestWord()
        {
            Assert.AreEqual("testword", new Word("TestWord").Value);
        }

        [TestMethod]
        public void TestLetter()
        {
            Assert.AreEqual('a', new Letter('a').Value);
        }
    }
}
