using ScrabbleEngine;

namespace ScrabbleTests
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Word word = new Word("TestWord");
            Assert.AreEqual(word.Value, "testword");
        }

        [TestMethod]
        public void TestMethod3()
        {
            Letter letter = new Letter('a');
            Assert.AreEqual(letter.Value, 'a');
        }
    }
}
