
using System.Collections.Concurrent;

namespace ScrabbleEngine
{
    public class Board
    {
        public Square[,] grid;
        private const int gridDimension = 15;
        Dictionary dict;

        public Square[,] Grid
        {            
            get { return grid; }
        }

        public int GridDimension
        {
            get { return gridDimension; }
        }

        public Board()
        {
            dict = new Dictionary();
            grid = new Square[gridDimension, gridDimension];

            for (int r = 0; r < gridDimension; r++)
            {
                for (int c = 0; c < gridDimension; c++)
                {
                    grid[r, c] = new Square();
                }
            }

            SquareReflectBonus(Square.BonusType.tripleWord, 0, 0);
            SquareReflectBonus(Square.BonusType.doubleWord, 1, 1);
            SquareReflectBonus(Square.BonusType.doubleWord, 2, 2);
            SquareReflectBonus(Square.BonusType.doubleWord, 3, 3);
            SquareReflectBonus(Square.BonusType.doubleWord, 4, 4);
            SquareReflectBonus(Square.BonusType.tripleLetter, 5, 5);
            SquareReflectBonus(Square.BonusType.doubleLetter, 6, 6);
            SquareReflectBonus(Square.BonusType.tripleLetter, 5, 1);
            SquareReflectBonus(Square.BonusType.doubleLetter, 3, 0);
            SquareReflectBonus(Square.BonusType.doubleLetter, 6, 2);
            SquareReflectBonus(Square.BonusType.tripleLetter, 1, 5);
            SquareReflectBonus(Square.BonusType.doubleLetter, 0, 3);
            SquareReflectBonus(Square.BonusType.doubleLetter, 2, 6);

            DiamondReflectBonus(Square.BonusType.tripleWord, 7, 0);
            DiamondReflectBonus(Square.BonusType.doubleLetter, 7, 3);
        }

        public Board(char[,] pCharBoard)
        {
            dict = new Dictionary();
            grid = new Square[gridDimension, gridDimension];

            for (int r = 0; r < gridDimension; r++)
            {
                for (int c = 0; c < gridDimension; c++)
                {
                    grid[r, c] = new Square(Square.BonusType.nothing, pCharBoard[r, c]);
                }
            }

            SquareReflectBonus(Square.BonusType.tripleWord, 0, 0);
            SquareReflectBonus(Square.BonusType.doubleWord, 1, 1);
            SquareReflectBonus(Square.BonusType.doubleWord, 2, 2);
            SquareReflectBonus(Square.BonusType.doubleWord, 3, 3);
            SquareReflectBonus(Square.BonusType.doubleWord, 4, 4);
            SquareReflectBonus(Square.BonusType.tripleLetter, 5, 5);
            SquareReflectBonus(Square.BonusType.doubleLetter, 6, 6);
            SquareReflectBonus(Square.BonusType.tripleLetter, 5, 1);
            SquareReflectBonus(Square.BonusType.doubleLetter, 3, 0);
            SquareReflectBonus(Square.BonusType.doubleLetter, 6, 2);
            SquareReflectBonus(Square.BonusType.tripleLetter, 1, 5);
            SquareReflectBonus(Square.BonusType.doubleLetter, 0, 3);
            SquareReflectBonus(Square.BonusType.doubleLetter, 2, 6);

            DiamondReflectBonus(Square.BonusType.tripleWord, 7, 0);
            DiamondReflectBonus(Square.BonusType.doubleLetter, 7, 3);
        }        

        public Square this[int row, int column]
        {
            get { return this.grid[row, column]; }
        }

        public bool IsEmpty()
        {
            for (int r = 0; r < gridDimension; r++)
            {
                for (int c = 0; c < gridDimension; c++)
                {
                    if (grid[r, c].Value != Letter.NoLetter)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public List<Word> BoardCheck(string pstrLetters, ProgressBar pb)
        {  
            if (IsEmpty() == true)
            {
                Line simpleLine = new Line();
                simpleLine.SimpleLineCheck(pstrLetters, out List<Word> pSimpleWordList, pb);
                return pSimpleWordList;
            }

            List<Word> possibleWordList = new List<Word>();

            for (int r = 0; r < gridDimension; r++)
            {
                RowCheck(pstrLetters, out List<Word> pWordList, pb, r);
                possibleWordList.AddRange(pWordList);
            }


            for (int c = 0; c < gridDimension; c++)
            {
                ColumnCheck(pstrLetters, out List<Word> pWordList, pb, c);
                possibleWordList.AddRange(pWordList);
            }
            return possibleWordList;
        }

        private void SquareReflectBonus(Square.BonusType pBonus, int pRow, int pColumn)
        {
            int indexGridDimension = gridDimension - 1;

            grid[pRow, pColumn].Bonus = pBonus;
            grid[indexGridDimension - pRow, pColumn].Bonus = pBonus;
            grid[pRow, indexGridDimension - pColumn].Bonus = pBonus;
            grid[indexGridDimension - pRow, indexGridDimension - pColumn].Bonus = pBonus;
        }

        private void DiamondReflectBonus(Square.BonusType pBonus, int pRow, int pColumn)
        {
            int indexGridDimension = gridDimension - 1;

            grid[pRow, pColumn].Bonus = pBonus;
            grid[pColumn, pRow].Bonus = pBonus;
            grid[indexGridDimension - pColumn, pRow].Bonus = pBonus;
            grid[pRow, indexGridDimension - pColumn].Bonus = pBonus;
        }        

        private void RowCheck(string pstrLetters, out List<Word> pWordList, ProgressBar pb, int pRow)
        {
            Line line = new Line();

            for (int c = 0; c < gridDimension; c++)
            {
                line.AddSquare(grid[pRow, c]);
            }
            if (line.IsEmpty() == false)
                line.LineCheck(pstrLetters, out pWordList, pb);
            else
            {
                pWordList = new List<Word>();
                return;
            }                

            //All words in "line" are words for a row so set that
            foreach (Word word in pWordList)
            {
                word.SetAsRow();
                word.ColumnIndex = word.ColumnIndex;
                word.RowIndex = pRow;
            }
        }

        private void ColumnCheck(string pstrLetters, out List<Word> pWordList, ProgressBar pb, int pColumn)
        {
            Line line = new Line();

            for (int r = 0; r < gridDimension; r++)
            {
                line.AddSquare(grid[r, pColumn]);
            }
            
            if (line.IsEmpty() == false)
                line.LineCheck(pstrLetters, out pWordList, pb);
            else
            {
                pWordList = new List<Word>();
                return;
            }

            //All words in "line" are words for a column so set that
            foreach (Word word in pWordList)
            {
                word.SetAsColumn();
                //line check is setting each word as the column index, we need to flip that for this function
                word.RowIndex = word.ColumnIndex;
                word.ColumnIndex = pColumn;
            }
        }

        public void RefreshBoard(ProgressBar pB)
        {
            int intProgressValue = 0;
            pB.Value = 0;
            pB.Maximum = gridDimension * gridDimension;

            for (int r = 0;r < gridDimension; r++)
            {
                for (int c = 0;c < gridDimension; c++)
                {
                    if (grid[r,c].Value != Letter.NoLetter)
                    {
                        RefreshSquare(r,c);                        
                    }
                    pB.Value = intProgressValue++;
                }
            }
        }

        public void GetColumn(int pRowIndex, int pColIndex, out Word pColAbove, out Word pColBelow)
        {
            //Go to top 
            int r = pRowIndex-1;
            while ((r >= 0) && (this.grid[r, pColIndex].Value != Letter.NoLetter))
            {
                r--;
            }
            r++;

            List<char> lstChars = new List<char>();
            for (int i = r; (i < pRowIndex); i++)
            {
                lstChars.Add(this.grid[i, pColIndex].Value);
            }
            pColAbove = new Word(new string(lstChars.ToArray()));
            pColAbove.SetAsColumn();
            pColAbove.RowIndex = r;
            pColAbove.ColumnIndex = pColIndex;

            lstChars = new List<char>();
            for (int i = pRowIndex+1; (i < gridDimension) && (this.grid[i, pColIndex].Value != Letter.NoLetter); i++)
            {
                lstChars.Add(this.grid[i, pColIndex].Value);
            }
            pColBelow = new Word(new string(lstChars.ToArray()));
            pColBelow.SetAsColumn();
            pColBelow.RowIndex = pRowIndex + 1;
            pColBelow.ColumnIndex = pColIndex;
        }

        public void GetRow(int pRowIndex, int pColIndex, out Word pRowLeft, out Word pRowRight)
        {
            //Go to left 
            int c = pColIndex-1;
            while ((c >= 0) && (this.grid[pRowIndex, c].Value != Letter.NoLetter))
            {
                c--;
            }
            c++;

            List<char> lstChars = new List<char>();
            for (int i = c; (i < pColIndex); i++)
            {
                lstChars.Add(this.grid[pRowIndex, i].Value);
            }
            pRowLeft = new Word(new string(lstChars.ToArray()));
            pRowLeft.SetAsRow();
            pRowLeft.RowIndex = pRowIndex;
            pRowLeft.ColumnIndex = c;

            lstChars = new List<char>();
            for (int i = pColIndex + 1; (i < gridDimension) && (this.grid[pRowIndex, c].Value != Letter.NoLetter); i++)
            {
                lstChars.Add(this.grid[pRowIndex, i].Value);
            }   
            pRowRight = new Word(new string(lstChars.ToArray()));
            pRowRight.SetAsRow();
            pRowRight.RowIndex = pRowIndex;
            pRowRight.ColumnIndex = pColIndex + 1;
        }

        public void RefreshSquare(int pRowIndex, int pColIndex)
        {
            GetColumn(pRowIndex, pColIndex, out Word pColAbove, out Word pColBelow);
            GetRow(pRowIndex, pColIndex, out Word pRowLeft, out Word pRowRight);
           
            Square square = grid[pRowIndex, pColIndex];
            List<Letter> valchars = square.ValidValues;

            //Check Column
            for(int i = 0; i < valchars.Count; i++)
            {
                string catWord = pColAbove.Value + square.ValidValues[i].Value + pColBelow;
                if ((catWord.Length > 2) && (dict.CheckWord(catWord) == false))
                {
                    square.RemoveLetter(valchars[i]);
                }
            }

            // Check Row
            for (int i = 0; i < valchars.Count; i++)
            {
                string catWord = pRowLeft.Value + square.ValidValues[i].Value + pRowRight;
                if ((catWord.Length > 2) && (dict.CheckWord(catWord) == false))
                {
                    square.RemoveLetter(valchars[i]);
                }
            }
        }
        public bool CheckPlayedWords(out string pWrongWord)
        {
            for (int r = 0; r < gridDimension; r++)
            {
                for (int c = 0; c < gridDimension; c++)
                {
                    if (GetWord(this, r, c, out Word pColWord, out Word pRowWord) == true)
                    {
                        if ((pColWord.Value != "") && (dict.CheckWord(pColWord.Value) == false))
                        {
                            pWrongWord = pColWord.Value;
                            return false;
                        }
                        else if ((pRowWord.Value != "") && (dict.CheckWord(pRowWord.Value) == false))
                        {
                            pWrongWord = pRowWord.Value;
                            return false;
                        }
                    }
                }
            }
            pWrongWord = "";
            return true;
        } 

        /// <summary>
        /// If the specified value at the pRowIndex and pColIndex is part of a column and/or a row word, returns those words
        /// </summary>
        /// <param name="pBoard"></param>
        /// <param name="pRowIndex"></param>
        /// <param name="pColIndex"></param>
        /// <param name="pColWord"></param>
        /// <param name="pRowWord"></param>
        /// <returns>True if it could at least get one column and/or row word</returns>
        public bool GetWord(Board pBoard, int pRowIndex, int pColIndex, out Word pColWord, out Word pRowWord)
        {
            if (pBoard[pRowIndex, pColIndex].Value == Letter.NoLetter)
            {
                pColWord = new Word("");
                pRowWord = new Word("");
                return false;
            }

            int origRow;
            int origCol;

            //Go to top of word 
            int r = pRowIndex;
            while ((r >= 0) && (pBoard[r, pColIndex].Value != Letter.NoLetter))
            {
                r--;
            }
            r++;

            //Variables dictating where the top of the column word begins
            origRow = r;
            origCol = pColIndex;
            List<char> lstChars = new List<char>();

            //Start adding the letters from the complete word until we hit the bottom of the board or a non-letter character
            for (int i = r; ((i < (gridDimension - 1)) && (pBoard[i, pColIndex].Value != Letter.NoLetter)); i++)
            {
                lstChars.Add(pBoard[i, pColIndex].Value);
            }
            // If we actually made a word, record that
            if (lstChars.Count > 1)
            {
                pColWord = new Word(new string(lstChars.ToArray()));
                pColWord.SetAsColumn();
                pColWord.RowIndex = origRow;
                pColWord.ColumnIndex = origCol;
            }
            else
                pColWord = new Word("");

            //Go to left of the word 
            int c = pColIndex;
            while ((c >= 0) && (pBoard[pRowIndex, c].Value != Letter.NoLetter))
            {
                c--;
            }
            c++;

            //These variables hold where the word begins to the left
            origRow = pRowIndex;
            origCol = c;
            lstChars = new List<char>();
            for (int i = c; ((i < (gridDimension - 1)) && (pBoard[pRowIndex, i].Value != Letter.NoLetter)); i++)
            {
                lstChars.Add(pBoard[pRowIndex, i].Value);
            }
            if (lstChars.Count > 1)
            {
                pRowWord = new Word(new string(lstChars.ToArray()));
                pRowWord.SetAsRow();
                pRowWord.RowIndex = origRow;
                pRowWord.ColumnIndex = origCol;
            }
            else
                pRowWord = new Word("");

            if ((pRowWord.Value == "") && (pColWord.Value == ""))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Compares pBoard against this.grid to find new words on pBoard.  
        /// </summary>
        /// <param name="pBoard"></param>
        /// <returns>Returns list of new words on board</returns>
        /// <exception cref="Exception">Square that was different but no new words there...???</exception>
        public List<Word> GetNewWord(Board pBoard)
        {
            List<Word> lstWords = new List<Word>();

            for (int r = 0; r < gridDimension; r++)
            {
                for (int c = 0; c < gridDimension; c++)
                {
                    if (grid[r, c].Value != pBoard[r,c].Value)
                    {
                        if (GetWord(pBoard, r, c, out Word pColWord, out Word pRowWord) == true)
                        {
                            if (pColWord.Value != "")
                                pColWord.AddToList(ref lstWords, true);
                            if (pRowWord.Value != "")
                                pRowWord.AddToList(ref lstWords, true);
                        }
                        else
                        {
                            throw new Exception("There is a wrong word on the board!");
                        }                        
                    }
                }
            }

            List<Word> results = new List<Word>();

            //We got a ton of results, so we need to filter out duplicate results
            foreach (Word word in lstWords)
            {
                if (word.InList(results) == false)
                    results.Add(word);
            }

            return results;
        }

        /// <summary>
        /// Returns pLstStrWords (List of List of words) of all possible words to be played at rowIndex row.
        /// Each list in pLstStrWords contains all words constructed from playing one word.
        /// </summary>
        /// <param name="pstrLetters"></param>
        /// <param name="pLstStrWords"></param>
        /// <param name="pB"></param>
        /// <param name="rowIndex"></param>
        public void RowLineCheck(string pstrLetters, out List<List<Word>> pLstStrWords, ProgressBar pB, int rowIndex)
        {
            int intProgressValue = 0;

            if (pB != null)
            {
                pB.Value = 0;
                pB.Maximum = this.GridDimension - 1;
            }

            pLstStrWords = new List<List<Word>>();

            for (int i = 0; i < this.GridDimension; i++)
            {
                ConcurrentBag<List<Word>> bag = new ConcurrentBag<List<Word>>();

                Parallel.For(0, dict.Length, j =>
                {
                    string scrabbleWord = dict.GetString(j);
                    if (WordMatchRow(scrabbleWord, pstrLetters, rowIndex, i, out List<Word> wordsMade) == true)
                    {
                        bag.Add(wordsMade);
                    }
                });
                pLstStrWords.AddRange(bag.ToList());
                if (pB != null)
                    pB.Value = intProgressValue++;
            }
        }

        /// <summary>
        /// Checks if at least one of the letters in pstrLetters is a Letter.NoLetter
        /// </summary>
        /// <param name="pstrLetters"></param>
        /// <returns></returns>
        public bool OneLetterUsed(string pstrLetters)
        {
            for (int i = 0; i < pstrLetters.Length; i++)
            {
                if (pstrLetters[i] == Letter.NoLetter)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the specified pcharletter out of pstrLetters (will just remove the first if multiple of the same letter
        /// If there is a wildcard letter and it can't find any of pcharletter, it will remove the wildcard letter
        /// </summary>
        /// <param name="pcharLetter"></param>
        /// <param name="pstrLetters"></param>
        /// <returns></returns>
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

        public bool WordMatchRow(string pScrabbleWord, string pstrLetters, int pRowIndex, int pColIndex, out List<Word> wordsMade)
        {
            //are one of the letters for our scrabble word one that is already played that we're playing off
            bool blnHitMask = false;
            wordsMade = new List<Word>();

            // If our scrabble word is longer than spaces we have, return false
            if ((gridDimension - pColIndex) < pScrabbleWord.Length)
                return false;

            //Check if our word is playable in this position
            int c = pColIndex;
            for (int i = pColIndex, j = 0; (i < gridDimension) && (j < pScrabbleWord.Length); i++, j++, c++)
            {
                if (grid[pRowIndex, i].Value == Letter.NoLetter)
                {
                    if ((grid[pRowIndex, i].IsValid(pScrabbleWord[j]) == false) || (RemoveLetter(pScrabbleWord[j], ref pstrLetters) == false))
                        return false;
                }
                else
                {
                    if (grid[pRowIndex, i].Value != pScrabbleWord[j])
                        return false;
                    else
                        blnHitMask = true;
                }
            }

            //Check to make sure we have used at least one of our letters to make the word
            if (OneLetterUsed(pstrLetters) == false)
            {
                return false;
            }
            //At this point we have now verified that our word can be played in this position
            //We now must check words around us

            //Get Word before the first letter of our scrabble word in the grid
            GetRow(pRowIndex, pColIndex, out Word pRowLeft, out _);
            //Get Word after the last letter of our scrabble word in the grid
            GetRow(pRowIndex, pColIndex + pScrabbleWord.Length - 1, out _, out Word pRowRight);
            string catWord = pRowLeft.Value + pScrabbleWord + pRowRight.Value;
            //If we actually now have a concatenated word...
            if (catWord.Length > pScrabbleWord.Length)
            {
                // Our scrabble word is actually part of a bigger word
                // If this is the case, we'll iterate through that bigger word later in our caller method - so just return false now
                return false;
            }

            //Now deal with column words
            //Do we need to worry about going over griddimension?
            for (int i = 0; i < pScrabbleWord.Length; i++)
            {
                GetColumn(pRowIndex, pColIndex+i, out Word pColAbove, out Word pColBelow);
                catWord = pColAbove.Value + pScrabbleWord[i].ToString() + pColBelow.Value;

                //If we actually now have a concatenated word...
                if (catWord.Length > 1)
                {
                    // If it is a valid word, we should add it to our list of words created
                    if (dict.CheckWord(catWord) == true)
                    {
                        Word word = new Word(catWord);
                        word.SetAsColumn();
                        word.RowIndex = pColAbove.RowIndex;
                        word.ColumnIndex = pColIndex+i;
                        wordsMade.Add(word);
                    }
                    // If it is not valid, we simply can't play this word
                    else
                    {
                        return false;
                    }
                }
            }
            
            if ((wordsMade.Count > 0) || (blnHitMask == true))
            {
                Word word = new Word(pScrabbleWord);
                word.SetAsRow();
                word.RowIndex = pRowIndex;
                word.ColumnIndex = pColIndex;
                wordsMade.Add(word);

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Places values in the board onto the tablelayoutpanel
        /// </summary>
        public void SyncBoardToTableLayoutPanel(ref TableLayoutPanel tLB)
        {
            for (int row = 0; row < gridDimension; row++)
            {
                for (int col = 0; col < gridDimension; col++)
                {
                    TextBox? tb = tLB.GetControlFromPosition(col, row) as TextBox;
                    if (tb != null)
                    {
                        if (grid[row, col].Value == Letter.NoLetter)
                            tb.Text = "";
                        else
                            tb.Text = grid[row, col].Value.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Places values on the tablelayoutpanel into the board
        /// </summary>
        /// <param name="tLB"></param>
        public void SyncTableLayoutPanelToBoard(TableLayoutPanel tLB)
        {
            for (int row = 0; row < gridDimension; row++)
            {
                for (int col = 0; col < gridDimension; col++)
                {
                    TextBox? tb = tLB.GetControlFromPosition(col, row) as TextBox;
                    if ((tb != null) && (tb.Text.Length == 1) && (char.IsLetter(tb.Text[0]) == true))
                    {
                        grid[row, col].Value = tb.Text[0];
                    }
                }
            }
        }
    }
}
