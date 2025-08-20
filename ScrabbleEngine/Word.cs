
namespace ScrabbleEngine
{
    public class Word
    {
        private string value;
        private List<Letter> letterList;
        private int points;
        private int rowIndex;
        private int columnIndex;
        public bool isRow;
        public bool isColumn;
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

        /// <summary>
        /// Creates a Word object using the provided string
        /// Word can be empty string
        /// </summary>
        /// <param name="pStrWord">string parameter</param>
        public Word(string pStrWord)
        {
            this.value = "";
            this.points = 0;
            this.letterList = new List<Letter>();
            rowIndex = -1;
            columnIndex = -1;
            isRow = false;
            isColumn = false;

            if (pStrWord.Length > 0)
            {
                this.value = pStrWord.ToLower().Trim();
                foreach (char c in this.value)
                {
                    letterList.Add(new Letter(c));
                }
                CalculatePoints();
            }            
        }
        /// <summary>
        /// Indexes through the word's char values
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public char this[int index]
        {
            get { return this.letterList[index].Value; }
            set { this.letterList[index].Value = value; }
        }

        /// <summary>
        /// Sets word as a row word
        /// </summary>
        public void SetAsRow()
        {
            isRow = true;
            isColumn = false;
        }

        /// <summary>
        /// sets word as a column word
        /// </summary>
        public void SetAsColumn()
        {
            isRow = false;
            isColumn = true;
        }

        /// <summary>
        /// Calculates the points that the word is worth
        /// </summary>
        public int CalculatePoints()
        {
            int intTempPoints = 0;
            foreach(Letter c  in letterList)
            {
                intTempPoints += c.Points;
            }
            this.points = intTempPoints;
            return this.points;
        }

        /// <summary>
        /// Checks if word is nothing ""
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if (this.value == "")
            { 
                return true; 
            }
            return false;
        }

        /// <summary>
        /// Returns a string with the word formatted nicely with points/index if requested
        /// if both indices are -1, will return [], else it will return one or both if one or both are not -1
        /// </summary>
        /// <param name="pblnPoints"></param>
        /// <param name="pblnIndex"></param>
        /// <returns></returns>
        public string PrintWord(bool pblnPoints, bool pblnIndex)
        {
            string strResults = this.value;            

            if (pblnIndex == true)
            {
                strResults += " [";
                if (this.rowIndex > -1)
                {
                    if (this.columnIndex > -1)
                        strResults += this.rowIndex + ", ";
                    else
                        strResults += this.rowIndex;
                }   
                if (this.columnIndex > -1)
                    strResults += this.columnIndex;
                strResults += "]";
            }            

            if (pblnPoints == true)
            {
                strResults += " (" + this.points + ")";
            }

            return strResults;
        }

        
        public void AddToList(ref List<Word> pLstWords, bool pCheckIndex = false)
        {
            foreach (Word word in pLstWords)
            {
                if (pCheckIndex == true)
                {
                    if ((this.rowIndex == word.rowIndex) && (this.columnIndex == word.columnIndex))
                    {
                        return;
                    }
                }
                else if (this.Value == word.Value)
                {
                    return;
                }
            }
            pLstWords.Add(this);
        }

        public bool InList(List<Word> pLstWords)
        {
            foreach(Word word in pLstWords)
            {
                if ((this.rowIndex == word.rowIndex) && 
                    (this.columnIndex == word.columnIndex) && 
                    (this.isRow == word.isRow) && 
                    (this.isColumn == word.isColumn) &&
                    (this.value == word.value))
                {
                    return true;
                }
            }
            return false;
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

        public bool OneLetterUsed(string pstrLetters)
        {
            for (int i = 0; i < pstrLetters.Length; i++)
            {
                if (pstrLetters[i] == Letter.NoLetter)
                    return true;
            }
            return false;
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
            {
                if (OneLetterUsed(pstrLetters) == true)
                {
                    return blnHitMask;
                }
                return false;
            }                
        }
    }
}
