
namespace ScrabbleEngine
{
    public class Line
    {
        private List<Square> validSquares;

        public int Length
        {
            get { return validSquares.Count; }
        }

        public Line(string pstrMask = "---------------")
        {
            validSquares = new List<Square>();
            foreach (char c in pstrMask)
            {
                validSquares.Add(new Square(Square.BonusType.nothing, c));
            }           
           
        }

        public char this[int index]
        {
            get { return this.validSquares[index].Value; }
            set { this.validSquares[index].Value = value; }
        }
        public Square GetSquare(int pIndex)
        {
            return validSquares[pIndex];
        }

        public void AddSquare(Square pSquare)
        {
            validSquares.Add(pSquare);
        }

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
                pB.Maximum = dictWords.Length * this.Length;
            }

            pLstStrWords = new List<Word>();

            for (int i = 0; i < this.Length; i++)
            {
                //Word debugWord = new Word("Words start at Index " + i.ToString());
                //debugWord.AddToList(ref pLstStrWords);

                for (int j = 0; j < dictWords.Length; j++)
                {
                    Word scrabbleWord = dictWords.GetWord(j);
                    if (scrabbleWord.WordMatchMask(i, this, pstrLetters, true, false) == true)
                    {
                        scrabbleWord.ColumnIndex = i;
                        scrabbleWord.AddToList(ref pLstStrWords);
                    }

                    if (pB != null)
                    {
                        pB.Value = intProgressValue++;
                    }                    
                }
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
                if (scrabbleWord.WordMatchMask(0, this, pstrLetters, false, true) == true)
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
