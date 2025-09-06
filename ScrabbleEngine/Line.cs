
using System.Collections.Concurrent;
using System.Text;

namespace ScrabbleEngine
{
    public class Line
    {
        private List<Square> validSquares;

        public int Length
        {
            get { return validSquares.Count; }
        }

        public Line(string pstrMask = "")
        {
            validSquares = new List<Square>();
            foreach (char c in pstrMask)
            {
                validSquares.Add(new Square(Square.BonusType.nothing, c));
            } 
        }

        /// <summary>
        /// Gets and sets char value in the square at the indexed spot
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public char this[int index]
        {
            get { return this.validSquares[index].Value; }
            set { this.validSquares[index].Value = value; }
        }

        /// <summary>
        /// Returns the square at the specified index
        /// </summary>
        /// <param name="pIndex"></param>
        /// <returns></returns>
        public Square GetSquare(int pIndex)
        {
            return validSquares[pIndex];
        }

        /// <summary>
        /// Adds a square to our line
        /// </summary>
        /// <param name="pSquare"></param>
        public void AddSquare(Square pSquare)
        {
            validSquares.Add(pSquare);
        }

        /// <summary>
        /// return the string value of all the squares in the line
        /// </summary>
        /// <returns></returns>
        public string PrintLine()
        {
            StringBuilder retString = new StringBuilder();
            foreach(Square square in validSquares)
            {
                retString.Append(square.Value);
            }
            return retString.ToString();
        }

        /// <summary>
        /// Returns true if the line has no squares in it
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            foreach( Square square in validSquares)
            {
                if (square.Value != Letter.NoLetter)
                    return false;
            }  
            return true;
        }

        public void LineCheck(string pstrLetters, out List<Word> pLstStrWords, ProgressBar pB)
        {
            int intProgressValue = 0;
            Dictionary dictWords = new Dictionary();

            if (pB != null)
            {
                pB.Value = 0;
                pB.Maximum = this.Length-1;
            }

            pLstStrWords = new List<Word>();

            for (int i = 0; i < this.Length; i++)
            {
                ConcurrentBag<Word> bag = new ConcurrentBag<Word>();

                Parallel.For(0, dictWords.Length, j =>
                {
                    Word scrabbleWord = dictWords.GetWord(j);
                    if (scrabbleWord.WordMatchMask(i, this, pstrLetters, true, false) == true)
                    {
                        scrabbleWord.ColumnIndex = i;
                        bag.Add(scrabbleWord);
                    }                    
                });
                pLstStrWords.AddRange(bag.ToList());
                if (pB != null)
                    pB.Value = intProgressValue++;

                //for (int j = 0; j < dictWords.Length; j++)
                //{
                //    Word scrabbleWord = dictWords.GetWord(j);
                //    if (scrabbleWord.WordMatchMask(i, this, pstrLetters, true, false) == true)
                //    {
                //        scrabbleWord.ColumnIndex = i;
                //        scrabbleWord.AddToList(ref pLstStrWords);
                //    }

                //    if (pB != null)
                //    {
                //        pB.Value = intProgressValue++;
                //    }                    
                //}
            }
        }

        public void SimpleLineCheck(string pstrLetters, out List<Word> pLstStrWords, ProgressBar pB)
        {
            int intProgressValue = 0;
            Dictionary dictWords = new Dictionary();

            if (pB != null)
            {
                pB.Value = 0;
                pB.Maximum = dictWords.Length;
            }

            pLstStrWords = new List<Word>();
            
            for (int j = 0; j < dictWords.Length; j++)
            {
                Word scrabbleWord = dictWords.GetWord(j);
                if (scrabbleWord.WordMatchMask(0, this, pstrLetters, false, false) == true)
                {
                    scrabbleWord.AddToList(ref pLstStrWords);
                }

                if (pB != null)
                {
                    pB.Value = intProgressValue++;
                }
            }
        }
    }
}
