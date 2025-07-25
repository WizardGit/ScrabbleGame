
namespace ScrabbleEngine
{
    public class Word
    {
        private string value;
        private List<Letter> letterList;
        private int points;
        private int rowIndex;
        private int columnIndex;
        private bool isRow;
        private bool isColumn;
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public int Points
        {
            get { return points; }
            set { this.points = value; }
        }
        public int Length
        {
            get { return value.Length; }
        }

        public int RowIndex
        {
            get { return rowIndex; }
            set { this.rowIndex = value; }
        }

        public int ColumnIndex
        {
            get { return columnIndex; }
            set { this.columnIndex = value; }
        }

        public Word(string pStrWord)
        {
            this.value = pStrWord.ToLower().Trim();
            this.points = 0;
            this.letterList = new List<Letter>();
            rowIndex = -1;
            columnIndex = -1;
            isRow = false;
            isColumn = false;

            foreach (char c in this.value)
            {
                letterList.Add(new Letter(c));
            }
            CalculatePoints();
        }

        public void SetAsRow()
        {
            isRow = true;
            isColumn = false;
        }

        public void SetAsColumn()
        {
            isRow = false;
            isColumn = true;
        }

        public void CalculatePoints()
        {
            int intTempPoints = 0;
            foreach(Letter c  in letterList)
            {
                intTempPoints += c.Points;
            }
            this.points = intTempPoints;
        }

        public string PrintWord()
        {
            return this.value;
        }

        public string PrintWordPoints()
        {
            return this.value + " (" + this.points + ")";
        }

        public string PrintWordIndex()
        {
            return this.value + " [" + this.rowIndex + ", " + this.columnIndex + "]";
        }

        public char this[int index]
        {
            get { return this.letterList[index].Value; }
            set { this.letterList[index].Value = value; }
        }
        public void AddToList(ref List<Word> pLstStrWords)
        {
            foreach (Word word in pLstStrWords)
            {
                if (this.Value == word.Value)
                {
                    return;
                }
            }
            pLstStrWords.Add(this);
        }

        public bool RemoveLetter(char pcharLetter, ref string pstrLetters)
        {
            char[] charListLetters = pstrLetters.ToCharArray();

            for (int i = 0; i < pstrLetters.Length; i++)
            {
                if (charListLetters[i] == pcharLetter)
                {
                    charListLetters[i] = Letter.NoLetter;
                    pstrLetters = new string(charListLetters);
                    return true;
                }
            }
            //We have no corresponding letters, but check for the wildcard
            for (int i = 0; i < pstrLetters.Length; i++)
            {
                if (charListLetters[i] == Letter.AnyLetter)
                {
                    charListLetters[i] = Letter.NoLetter;
                    pstrLetters = new string(charListLetters);
                    return true;
                }
            }
            return false;
        }

        public bool WordMatchMask(int pintStartIndex, string pstrMask, string pstrLetters, bool pblnMustHitMask)
        {
            bool blnHitMask;
            if (pblnMustHitMask == true)
                blnHitMask = false;
            else
                blnHitMask = true;

            if ((pstrMask.Length < this.Length) || ((pstrMask.Length - pintStartIndex) < this.Length))
                return false;

            Word wrdMask = new Word(pstrMask);

            //We can't count words if there is a letter before it for same reason as letters after word (see below comment)
            if ((pintStartIndex - 1 >= 0) && (wrdMask[pintStartIndex - 1] != Letter.NoLetter) )
            {
                return false;
            }

            int c = pintStartIndex;
            for (int i = pintStartIndex, j = 0; (i < pstrMask.Length) && (j < this.Length); i++, j++, c++)
            {
                if (wrdMask[i] == Letter.NoLetter)
                {
                    if (RemoveLetter(this[j], ref pstrLetters) == false)
                        return false;
                }
                else
                {
                    if (wrdMask[i] != this[j])
                        return false;
                    else
                        blnHitMask = true;
                }
            }
            //We need to check to make sure our word doesn't end right next to another letter - otherwise the word can't be played
            //If that letter can still be combined to make a different word, we'll catch that case letter as we move through the dictionary
            if ((c != pstrMask.Length) && (wrdMask[c] != Letter.NoLetter))
                return false;
            else
                return blnHitMask;
        }

        public bool WordMatchMask(int pintStartIndex, Line pLine, string pstrLetters, bool pblnMustHitMask = true, bool pblnMustMatchLength = false)
        {
            if ((pblnMustMatchLength == true) && (this.Length != pLine.Length))
                return false;

            bool blnHitMask;
            if (pblnMustHitMask == true)
                blnHitMask = false;
            else
                blnHitMask = true;

            if ((pLine.Length < this.Length) || ((pLine.Length - pintStartIndex) < this.Length))
                return false;            

            //We can't count words if there is a letter before it for same reason as letters after word (see below comment)
            if ((pintStartIndex - 1 >= 0) && (pLine[pintStartIndex - 1] != Letter.NoLetter))
            {
                return false;
            }

            int c = pintStartIndex;
            for (int i = pintStartIndex, j = 0; (i < pLine.Length) && (j < this.Length); i++, j++, c++)
            {
                if (pLine[i] == Letter.NoLetter)
                {
                    if ((pLine.GetSquare(i).IsValid(this[j]) == false) || (RemoveLetter(this[j], ref pstrLetters) == false))
                        return false;
                }
                else
                {
                    if (pLine[i] != this[j])
                        return false;
                    else
                        blnHitMask = true;
                }
            }
            //We need to check to make sure our word doesn't end right next to another letter - otherwise the word can't be played
            //If that letter can still be combined to make a different word, we'll catch that case letter as we move through the dictionary
            if ((c != pLine.Length) && (pLine[c] != Letter.NoLetter))
                return false;
            else
                return blnHitMask;
        }
    }
}
