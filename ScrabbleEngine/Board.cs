
using Microsoft.VisualBasic.Devices;
using System.Collections.Concurrent;
using System.Collections.Generic;

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

        /// <summary>
        /// Checks if Board is Empty
        /// </summary>
        /// <returns></returns>
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

        public List<List<Word>> BoardCheck(string pstrLetters, ProgressBar pb)
        {
            List<List<Word>> possibleWordList = new List<List<Word>>();
            if (IsEmpty() == true)
            {
                Line simpleLine = new Line("-------");
                simpleLine.SimpleLineCheck(pstrLetters, out List<Word> pSimpleWordList, pb);
                possibleWordList.Add(pSimpleWordList);
            }
            else
            {
                for (int r = 0; r < gridDimension; r++)
                {
                    RowLineCheck(pstrLetters, out List<List<Word>> pWordList, pb, r);
                    possibleWordList.AddRange(pWordList);
                }


                for (int c = 0; c < gridDimension; c++)
                {
                    ColumnLineCheck(pstrLetters, out List<List<Word>> pWordList, pb, c);
                    possibleWordList.AddRange(pWordList);
                }
            }
            
            return possibleWordList;
        }

        /// <summary>
        /// Reflects the specified bonus type in a square pattern
        /// pRow and PColumn should be position of top left square to be reflected
        /// </summary>
        /// <param name="pBonus"></param>
        /// <param name="pRow"></param>
        /// <param name="pColumn"></param>
        public void SquareReflectBonus(Square.BonusType pBonus, int pRow, int pColumn)
        {            
            // Since we need to be the top left square of a square reflection, we shoud always be in the top left quadrant of the board!
            if ((pRow >= 7) || (pRow < 0) || (pColumn >= 7) || (pColumn < 0))
                throw new Exception("Specified row or column are outside the top left quadrant of the board!");

            int indexGridDimension = gridDimension - 1;

            grid[pRow, pColumn].Bonus = pBonus;
            grid[indexGridDimension - pRow, pColumn].Bonus = pBonus;
            grid[pRow, indexGridDimension - pColumn].Bonus = pBonus;
            grid[indexGridDimension - pRow, indexGridDimension - pColumn].Bonus = pBonus;
        }

        /// <summary>
        /// Reflects the specified bonus type in a diamond pattern
        /// pRow and Pcolumn should be middle left square of the diamond reflect
        /// </summary>
        /// <param name="pBonus"></param>
        /// <param name="pRow"></param>
        /// <param name="pColumn"></param>
        public void DiamondReflectBonus(Square.BonusType pBonus, int pRow, int pColumn)
        {
            // Diamond reflect should only be used when row is 7 and column is not outside bounds of the board!
            if ((pRow != 7) || (pColumn >= gridDimension) || (pColumn < 0))
                throw new Exception("Specified row or column are outside the allowed bounds when trying to reflect it!");

            int indexGridDimension = gridDimension - 1;

            grid[pRow, pColumn].Bonus = pBonus;
            grid[pColumn, pRow].Bonus = pBonus;
            grid[indexGridDimension - pColumn, pRow].Bonus = pBonus;
            grid[pRow, indexGridDimension - pColumn].Bonus = pBonus;
        }        
                
        /// <summary>
        /// Gets words above/below of specified square on column
        /// If no word, it will return Word with no letter
        /// This method DOES NOT check if the word is a valid word, or that the letter in each square is actually valid to be there!
        /// </summary>
        /// <param name="pRowIndex"></param>
        /// <param name="pColIndex"></param>
        /// <param name="pColAbove"></param>
        /// <param name="pColBelow"></param>
        public void GetColumn(int pRowIndex, int pColIndex, out Word pColAbove, out Word pColBelow)
        {
            List<char> lstChars;
            //Go to top 
            int r = pRowIndex-1;
            while ((r >= 0) && (this.grid[r, pColIndex].Value != Letter.NoLetter))
            {
                r--;
            }
            r++;

            //Get word above
            lstChars = new List<char>();
            for (int i = r; (i < pRowIndex); i++)
            {
                lstChars.Add(this.grid[i, pColIndex].Value);
            }
            pColAbove = new Word(new string(lstChars.ToArray()));
            pColAbove.SetAsColumn();
            pColAbove.RowIndex = r;
            pColAbove.ColumnIndex = pColIndex;

            //Get word below
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

        /// <summary>
        /// Gets words to left/right of specified square on a row
        /// If no word, it will return Word with no letter
        /// This method DOES NOT check if the word is a valid word, or that the letter in each square is actually valid to be there!
        /// </summary>
        /// <param name="pRowIndex"></param>
        /// <param name="pColIndex"></param>
        /// <param name="pRowLeft"></param>
        /// <param name="pRowRight"></param>
        public void GetRow(int pRowIndex, int pColIndex, out Word pRowLeft, out Word pRowRight)
        {
            List<char> lstChars;
            //Go to left 
            int c = pColIndex-1;
            while ((c >= 0) && (this.grid[pRowIndex, c].Value != Letter.NoLetter))
            {
                c--;
            }
            c++;

            //Get word to left
            lstChars = new List<char>();
            for (int i = c; (i < pColIndex); i++)
            {
                lstChars.Add(this.grid[pRowIndex, i].Value);
            }
            pRowLeft = new Word(new string(lstChars.ToArray()));
            pRowLeft.SetAsRow();
            pRowLeft.RowIndex = pRowIndex;
            pRowLeft.ColumnIndex = c;

            //Get word to right
            lstChars = new List<char>();
            for (int i = pColIndex + 1; (i < gridDimension) && (this.grid[pRowIndex, i].Value != Letter.NoLetter); i++)
            {
                lstChars.Add(this.grid[pRowIndex, i].Value);
            }   
            pRowRight = new Word(new string(lstChars.ToArray()));
            pRowRight.SetAsRow();
            pRowRight.RowIndex = pRowIndex;
            pRowRight.ColumnIndex = pColIndex + 1;
        }

        /// <summary>
        /// Removes any letters on empty squares that can't be played on the square due to the resulting word being invalid
        /// </summary>
        /// <param name="pB">OK to be a null letter</param>
        public void RefreshBoard(ProgressBar pB)
        {
            int intProgressValue = 0;
            if (pB != null)
            {                
                pB.Value = 0;
                pB.Maximum = gridDimension * gridDimension;
            }

            for (int r = 0; r < gridDimension; r++)
            {
                for (int c = 0; c < gridDimension; c++)
                {
                    if (grid[r, c].Value == Letter.NoLetter)
                    {
                        RefreshSquare(r, c);
                    }
                    if (pB != null)
                        pB.Value = intProgressValue++;
                }
            }
        }
        
        /// <summary>
        /// Removes any letters that can't be played on the square due to the resulting word being invalid
        /// </summary>
        /// <param name="pRowIndex"></param>
        /// <param name="pColIndex"></param>
        public void RefreshSquare(int pRowIndex, int pColIndex)
        {
            GetColumn(pRowIndex, pColIndex, out Word pColAbove, out Word pColBelow);
            GetRow(pRowIndex, pColIndex, out Word pRowLeft, out Word pRowRight);
           
            Square square = grid[pRowIndex, pColIndex];

            //This is a copy of the valid values of the square
            List<Letter> valchars = square.ValidValues;

            //Check Column
            for(int i = 0; i < valchars.Count; i++)
            {
                string catWord = pColAbove.Value + square.ValidValues[i].Value + pColBelow.Value;
                if ((catWord.Length > 2) && (dict.CheckWordIn(catWord) == false))
                {
                    square.RemoveLetter(valchars[i]);
                }
            }
            //We may have removed some of the letters from the square's valid characters, so now recopy the valid values that weren't eliminated
            valchars = square.ValidValues;

            // Check Row
            for (int i = 0; i < valchars.Count; i++)
            {
                string catWord = pRowLeft.Value + square.ValidValues[i].Value + pRowRight.Value;
                //We only want to remove that letter if no combination can possibly be or fit into a dictionary word
                if ((catWord.Length > 2) && (dict.CheckWordIn(catWord) == false))
                {
                    square.RemoveLetter(valchars[i]);
                }
            }
        }

        /// <summary>
        /// Checks board for words that aren't valid scrabble words!
        /// Returns True if all words are valid - false if not
        /// </summary>
        /// <param name="pWrongWord">First wrong word found</param>
        /// <returns>True if all words are valid - false if not</returns>
        public bool CheckPlayedWords(out string pWrongWord)
        {
            for (int r = 0; r < gridDimension; r++)
            {
                for (int c = 0; c < gridDimension; c++)
                {
                    if (grid[r, c].Value != Letter.NoLetter)
                    {
                        //If false, we have empty square, so just continue on, no need to check anything
                        if (GetWord(r, c, out Word pColWord, out Word pRowWord) == true)
                        {
                            if ((pColWord.Length <= 1) && (pRowWord.Length <= 1))
                            {
                                //If both are 1 or less, that means that they are both just the square letter, in which case, just return one and return false;
                                pWrongWord = pColWord.Value;
                                return false;
                            }
                            else if ((pColWord.Length > 1) && (dict.CheckWord(pColWord.Value) == false))
                            {
                                pWrongWord = pColWord.Value;
                                return false;
                            }
                            else if ((pRowWord.Length > 1) && (dict.CheckWord(pRowWord.Value) == false))
                            {
                                pWrongWord = pRowWord.Value;
                                return false;
                            }
                        }
                    }
                }
            }
            pWrongWord = "";
            return true;
        }

        /// <summary>
        /// Returns true if the square provided has a valid letter in it
        /// Does not guarantee that the word returned actually are valid words...
        /// </summary>
        /// <param name="pRowIndex"></param>
        /// <param name="pColIndex"></param>
        /// <param name="pColWord"></param>
        /// <param name="pRowWord"></param>
        /// <returns></returns>
        public bool GetWord(int pRowIndex, int pColIndex, out Word pColWord, out Word pRowWord)
        {
            GetColumn(pRowIndex, pColIndex, out Word pColAbove, out Word pColBelow);
            GetRow(pRowIndex, pColIndex, out Word pRowLeft, out Word pRowRight);
            char theLetter = grid[pRowIndex, pColIndex].Value;

            if ((theLetter == Letter.NoLetter) || (char.IsLetter(theLetter) == false))
            {
                pColWord = new Word("");
                pRowWord = new Word("");
                return false;
            }

            string colWord = pColAbove.Value + theLetter.ToString() + pColBelow.Value;
            string rowWord = pRowLeft.Value + theLetter.ToString() + pRowRight.Value;

            pColWord = new Word(colWord);
            pColWord.RowIndex = pColAbove.RowIndex;
            pColWord.ColumnIndex = pColAbove.ColumnIndex;
            pColWord.SetAsColumn();
            pRowWord = new Word(rowWord);
            pRowWord.RowIndex = pRowLeft.RowIndex;
            pRowWord.ColumnIndex = pRowLeft.ColumnIndex;
            pRowWord.SetAsRow();
            return true;
        }        

        /// <summary>
        /// Returns pLstStrWords (List of List of words) of all possible words to be played at rowIndex row.
        /// Each list in pLstStrWords contains all words constructed from playing one word.
        /// </summary>
        /// <param name="pstrLetters"></param>
        /// <param name="pLstStrWords"></param>
        /// <param name="pB"></param>
        /// <param name="pRowIndex"></param>
        public void RowLineCheck(string pstrLetters, out List<List<Word>> pLstStrWords, ProgressBar pB, int pRowIndex)
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
                    if (WordMatchRow(scrabbleWord, pstrLetters, pRowIndex, i, out List<Word> wordsMade) == true)
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
        /// Returns pLstStrWords (List of List of words) of all possible words to be played at rowIndex row.
        /// Each list in pLstStrWords contains all words constructed from playing one word.
        /// </summary>
        /// <param name="pstrLetters"></param>
        /// <param name="pLstStrWords"></param>
        /// <param name="pB"></param>
        /// <param name="rowIndex"></param>
        public void ColumnLineCheck(string pstrLetters, out List<List<Word>> pLstStrWords, ProgressBar pB, int pColIndex)
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
                    if (WordMatchColumn(scrabbleWord, pstrLetters, i, pColIndex, out List<Word> wordsMade) == true)
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
            if ((pstrLetters.Length <= 0) || ((char.IsLetter(pcharLetter) == false) && (pcharLetter != Letter.AnyLetter)))
                return false;
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

        /// <summary>
        /// Checks if the supplied scrabble word can be played at the specified row and column indexes.
        /// </summary>
        /// <param name="pScrabbleWord"></param>
        /// <param name="pstrLetters"></param>
        /// <param name="pRowIndex"></param>
        /// <param name="pColIndex"></param>
        /// <param name="wordsMade">List of words of all words completed</param>
        /// <returns>All possible words made, if the scrabble word can be played at the position</returns>
        public bool WordMatchRow(string pScrabbleWord, string pstrLetters, int pRowIndex, int pColIndex, out List<Word> wordsMade, bool pblnMustHitMask = true)
        {
            //are one of the letters for our scrabble word one that is already played that we're playing off
            bool blnHitMask = !pblnMustHitMask;
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
        /// Checks if the supplied scrabble word can be played at the specified row and column indexes.
        /// </summary>
        /// <param name="pScrabbleWord"></param>
        /// <param name="pstrLetters"></param>
        /// <param name="pRowIndex"></param>
        /// <param name="pColIndex"></param>
        /// <param name="wordsMade">List of words of all words completed</param>
        /// <returns>All possible words made, if the scrabble word can be played at the position</returns>
        public bool WordMatchColumn(string pScrabbleWord, string pstrLetters, int pRowIndex, int pColIndex, out List<Word> wordsMade)
        {
            //are one of the letters for our scrabble word one that is already played that we're playing off
            bool blnHitMask = false;
            wordsMade = new List<Word>();

            // If our scrabble word is longer than spaces we have, return false
            if ((gridDimension - pRowIndex) < pScrabbleWord.Length)
                return false;

            //Check if our word is playable in this position
            int r = pRowIndex;
            for (int i = pRowIndex, j = 0; (i < gridDimension) && (j < pScrabbleWord.Length); i++, j++, r++)
            {
                if (grid[i, pColIndex].Value == Letter.NoLetter)
                {
                    if ((grid[i, pColIndex].IsValid(pScrabbleWord[j]) == false) || (RemoveLetter(pScrabbleWord[j], ref pstrLetters) == false))
                        return false;
                }
                else
                {
                    if (grid[i, pColIndex].Value != pScrabbleWord[j])
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
            GetColumn(pRowIndex, pColIndex, out Word pColAbove, out _);
            //Get Word after the last letter of our scrabble word in the grid
            GetColumn(pRowIndex + pScrabbleWord.Length - 1, pColIndex, out _, out Word pColBelow);
            string catWord = pColAbove.Value + pScrabbleWord + pColBelow.Value;
            //If we actually now have a concatenated word...
            if (catWord.Length > pScrabbleWord.Length)
            {
                // Our scrabble word is actually part of a bigger word
                // If this is the case, we'll iterate through that bigger word later in our caller method - so just return false now
                return false;
            }

            //Now deal with row words
            for (int i = 0; i < pScrabbleWord.Length; i++)
            {
                GetRow(pRowIndex + i, pColIndex, out Word pRowLeft, out Word pRowRight);
                catWord = pRowLeft.Value + pScrabbleWord[i].ToString() + pRowRight.Value;

                //If we actually now have a concatenated word...
                if (catWord.Length > 1)
                {
                    // If it is a valid word, we should add it to our list of words created
                    if (dict.CheckWord(catWord) == true)
                    {
                        Word word = new Word(catWord);
                        word.SetAsRow();
                        word.RowIndex = pRowIndex + i;
                        word.ColumnIndex = pRowLeft.ColumnIndex;
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
                word.SetAsColumn();
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

        public int CalculateWordPoints(Word word)
        {
            //Calculate its points
            int colIndex = word.ColumnIndex;
            int rowIndex = word.RowIndex;
            bool isColumn = word.isColumn;
            bool isRow = word.isRow;

            if ((rowIndex < 0) || 
                (colIndex < 0) || 
                ((isRow == false) && (isColumn == false)) || 
                ((isRow == true) && (isColumn == true)))
            {
                throw new Exception("Can't calculate the word's points because it's missing a positioning parameter");
            }

            int resPoints = 0;
            int multiplicationFactor = 1;

            if (word.isRow == true)
            {
                for (int c = 0; c < word.Length; c++)
                {
                    if ((colIndex + c) > gridDimension)
                    {
                        throw new Exception("Word is longer than the grid's column dimension!");
                    }

                    resPoints += grid[rowIndex, colIndex + c].Points(ref multiplicationFactor);
                }
            }
            else // (word.isColumn == true
            {
                for (int r = 0; r < word.Length; r++)
                {
                    if ((rowIndex + r) > gridDimension)
                    {
                        throw new Exception("Word is longer than the grid's row dimension!");
                    }

                    resPoints += grid[rowIndex + r, colIndex].Points(ref multiplicationFactor);
                }
            }

            resPoints *= multiplicationFactor;

            return resPoints;
        }
    }
}
