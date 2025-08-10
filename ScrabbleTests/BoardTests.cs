using ScrabbleEngine;
using System.Diagnostics.Metrics;

namespace ScrabbleTests
{
    [TestClass]
    public sealed class BoardTests
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
        public void TestColumnMatchCheck()
        {
            Board board = new Board();
            board[6, 7].Value = 'b';
            board[7, 7].Value = 'e';
            board[8, 7].Value = 'a';
            board[9, 7].Value = 'd';
            board.WordMatchColumn("bead", "abcdefg", 7, 7, out List<Word> wordsMade);
            Assert.AreEqual(0, wordsMade.Count);
            board.WordMatchColumn("bead", "abcdefg", 6, 7, out wordsMade);
            Assert.AreEqual(0, wordsMade.Count);
            board.WordMatchColumn("beaded", "abcdefg", 6, 7, out wordsMade);
            Assert.AreEqual(1, wordsMade.Count);

            board[11, 4].Value = 'c';
            board[11, 5].Value = 'a';
            board[11, 6].Value = 'r';
            board.WordMatchColumn("beaded", "abcdefg", 6, 7, out wordsMade);
            Assert.AreEqual(2, wordsMade.Count);
            Assert.AreEqual("card", wordsMade[0].Value);
            Assert.AreEqual("beaded", wordsMade[1].Value);

            board[11, 8].Value = 'e';
            board[11, 9].Value = 'd';
            board.WordMatchColumn("beaded", "abcdefg", 6, 7, out wordsMade);
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
        public void TestColumnLineCheck()
        {
            Board board = new Board();
            board[6, 7].Value = 'b';
            board[7, 7].Value = 'e';
            board[8, 7].Value = 'a';
            board[9, 7].Value = 'd';
            board.ColumnLineCheck("abcdefg", out List<List<Word>> wordsMade, null, 7);
            Assert.AreEqual(1, wordsMade.Count);
            Assert.AreEqual(1, wordsMade[0].Count);

            board[11, 4].Value = 'c';
            board[11, 5].Value = 'a';
            board[11, 6].Value = 'r';
            board.ColumnLineCheck("abcdefg", out wordsMade, null, 7);
            Assert.AreEqual(25, wordsMade.Count);

            board.ColumnLineCheck("abcdefg", out wordsMade, null, 8);
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
            board.GetWord(7, 6, out Word pColWord, out Word pRowWord);
            Assert.AreEqual("b", pColWord.Value);
            Assert.AreEqual("bead", pRowWord.Value);

            board[4, 9].Value = 'c';
            board[5, 9].Value = 'a';
            board[6, 9].Value = 'r';
            board.GetWord(7, 9, out pColWord, out pRowWord);
            Assert.AreEqual("card", pColWord.Value);
            Assert.AreEqual("bead", pRowWord.Value);

            board[3, 10].Value = 'c';
            board[4, 10].Value = 'a';
            board[5, 10].Value = 'r';
            board[6, 10].Value = 'd';
            board[7, 10].Value = 'e';
            board[7, 11].Value = 'd';
            board[8, 10].Value = 'd';
            board.GetWord(7, 10, out pColWord, out pRowWord);
            Assert.AreEqual("carded", pColWord.Value);
            Assert.AreEqual("beaded", pRowWord.Value);
        }

        [TestMethod]
        public void TestEmpty()
        {
            Board board1 = new Board();

            Assert.AreEqual(true, board1.IsEmpty());

            board1[7, 6].Value = 'b';
            Assert.AreEqual(false, board1.IsEmpty());

            board1 = new Board();
            board1[14, 14].Value = 'b';
            Assert.AreEqual(false, board1.IsEmpty());
        }

        [TestMethod]
        public void TestReflectBonus()
        {
            Board board1 = new Board();
            //Board already runs the Reflect Methods, so let's verify that
            board1.SquareReflectBonus(Square.BonusType.tripleWord, 1,1);
            Assert.AreEqual(Square.BonusType.tripleWord, board1[1, 1].Bonus);
            Assert.AreEqual(Square.BonusType.tripleWord, board1[13, 1].Bonus);
            Assert.AreEqual(Square.BonusType.tripleWord, board1[1, 13].Bonus);
            Assert.AreEqual(Square.BonusType.tripleWord, board1[13, 13].Bonus);
            //Test out of quadrant for row
            Assert.ThrowsException<Exception>(() =>
            {
                board1.DiamondReflectBonus(Square.BonusType.tripleWord, 8, 2);
            });
            //Test out of column range
            Assert.ThrowsException<Exception>(() =>
            {
                board1.DiamondReflectBonus(Square.BonusType.tripleWord, 5, 20);
            });

            board1.DiamondReflectBonus(Square.BonusType.doubleLetter, 7, 6);
            Assert.AreEqual(Square.BonusType.doubleLetter, board1[7, 6].Bonus);
            Assert.AreEqual(Square.BonusType.doubleLetter, board1[6, 7].Bonus);
            Assert.AreEqual(Square.BonusType.doubleLetter, board1[8, 7].Bonus);
            Assert.AreEqual(Square.BonusType.doubleLetter, board1[7, 8].Bonus);
            //Test not on row 7
            Assert.ThrowsException<Exception>(() =>
            {
                board1.DiamondReflectBonus(Square.BonusType.doubleLetter, 6, 6);
            });
            //Test out of column range
            Assert.ThrowsException<Exception>(() =>
            {
                board1.DiamondReflectBonus(Square.BonusType.doubleLetter, 7, 15);
            });
        }

        [TestMethod]
        public void TestGetWordColumn()
        {
            Board board1 = new Board();

            board1.GetRow(4,5, out Word pRowLeft, out Word pRowRight);
            board1.GetColumn(4, 5, out Word pColumnAbove, out Word pColumnBelow);
            Assert.AreEqual("", pRowLeft.Value);
            Assert.AreEqual("", pRowRight.Value);
            Assert.AreEqual("", pColumnAbove.Value);
            Assert.AreEqual("", pColumnBelow.Value);

            board1[7, 6].Value = 'b';
            board1[7, 7].Value = 'e';
            board1[7, 8].Value = 'a';
            board1[7, 9].Value = 'd';
            board1.GetRow(7, 6, out pRowLeft, out pRowRight);
            board1.GetColumn(7, 6, out pColumnAbove, out pColumnBelow);
            Assert.AreEqual("", pRowLeft.Value);
            //Not a valid word, but that's not the intention of our method anyway
            Assert.AreEqual("ead", pRowRight.Value);
            Assert.AreEqual("", pColumnAbove.Value);
            Assert.AreEqual("", pColumnBelow.Value);
            board1.GetRow(7, 5, out pRowLeft, out pRowRight);
            Assert.AreEqual("", pRowLeft.Value);
            Assert.AreEqual("bead", pRowRight.Value);

            board1[4, 9].Value = 'c';
            board1[5, 9].Value = 'a';
            board1[6, 9].Value = 'r';
            board1.GetRow(4, 9, out pRowLeft, out pRowRight);
            board1.GetColumn(4, 9, out pColumnAbove, out pColumnBelow);
            Assert.AreEqual("", pRowLeft.Value);
            //Not a valid word, but that's not the intention of our method anyway
            Assert.AreEqual("", pRowRight.Value);
            Assert.AreEqual("", pColumnAbove.Value);
            Assert.AreEqual("ard", pColumnBelow.Value);
            board1.GetColumn(3, 9, out pColumnAbove, out pColumnBelow);
            Assert.AreEqual("", pColumnAbove.Value);
            Assert.AreEqual("card", pColumnBelow.Value);
        }

        [TestMethod]
        public void TestSmallBoardMethods()
        {
            Board board1 = new Board();
            string letters = "-------";

            Assert.AreEqual(true, board1.OneLetterUsed(letters));
            Assert.AreEqual(false, board1.RemoveLetter('a', ref letters));
            Assert.AreEqual("-------", letters);
            letters = "abcdefg";
            Assert.AreEqual(false, board1.OneLetterUsed(letters));
            Assert.AreEqual(true, board1.RemoveLetter('a', ref letters));
            Assert.AreEqual("-bcdefg", letters);
            letters = "a------";
            Assert.AreEqual(true, board1.OneLetterUsed(letters));
            Assert.AreEqual(true, board1.RemoveLetter('a', ref letters));
            Assert.AreEqual("-------", letters);
            letters = "------z";
            Assert.AreEqual(true, board1.OneLetterUsed(letters));
            letters = "abczte-";
            Assert.AreEqual(true, board1.OneLetterUsed(letters));
            Assert.AreEqual(false, board1.RemoveLetter('-', ref letters));
            Assert.AreEqual("abczte-", letters);

            letters = "*bczte-";
            Assert.AreEqual(true, board1.RemoveLetter('q', ref letters));
            Assert.AreEqual("-bczte-", letters);

            letters = "*bczte*";
            Assert.AreEqual(true, board1.RemoveLetter('q', ref letters));
            Assert.AreEqual("-bczte*", letters);
        }

        [TestMethod]
        public void TestCheckPlayedWords()
        {
            Board board1 = new Board();
            string wrongWord;

            Assert.AreEqual(true, board1.CheckPlayedWords(out wrongWord));

            board1[7, 6].Value = 'b';
            board1[7, 7].Value = 'e';
            board1[7, 8].Value = 'a';
            board1[7, 9].Value = 'd';
            board1[4, 9].Value = 'c';
            board1[5, 9].Value = 'a';
            board1[6, 9].Value = 'r';
            Assert.AreEqual(true, board1.CheckPlayedWords(out wrongWord));
            board1[7, 10].Value = 'e';
            board1[7, 11].Value = 'd';
            Assert.AreEqual(true, board1.CheckPlayedWords(out wrongWord));
            board1[13, 12].Value = 'z';
            Assert.AreEqual(false, board1.CheckPlayedWords(out wrongWord));
            Assert.AreEqual("z", wrongWord);
            board1[8, 9].Value = 't';
            Assert.AreEqual(false, board1.CheckPlayedWords(out wrongWord));
            Assert.AreEqual("cardt", wrongWord);

        }
    }
}
