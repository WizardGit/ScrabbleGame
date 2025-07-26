
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

        public List<Word> BoardCheck(string pstrLetters, ProgressBar pb)
        {
            List<Word> possibleWordList = new List<Word>();

            for (int r = 0; r < gridDimension; r++)
            {
                RowCheck(pstrLetters, out List<Word>pWordList, pb, r);
                possibleWordList.AddRange(possibleWordList);
            }
            

            for (int c = 0; c < gridDimension; c++)
            {
                ColumnCheck(pstrLetters, out List<Word> pWordList, pb, c);
                possibleWordList.AddRange(possibleWordList);
            }
            return possibleWordList;
        }

        public Square this[int row, int column]
        {
            get { return this.grid[row, column]; }
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
            line.LineCheck(pstrLetters, out pWordList, pb);

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
            line.LineCheck(pstrLetters, out pWordList, pb);

            //All words in "line" are words for a column so set that
            foreach (Word word in pWordList)
            {
                word.SetAsColumn();
                //line check is setting each word as the column index, we need to flip that for this function
                word.RowIndex = word.ColumnIndex;
                word.ColumnIndex = pColumn;
            }
        }

        public void RefreshBoard()
        {
            for(int r = 0;r < gridDimension; r++)
            {
                for (int c = 0;c < gridDimension; c++)
                {
                    if (grid[r,c].Value != Letter.NoLetter)
                    {
                        RefreshSquare(r,c);
                    }
                }
            }
        }

        public void GetColumn(int pColIndex, int pRowIndex, out string pColAbove, out string pColBelow)
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
            pColAbove = new string(lstChars.ToArray());
            lstChars = new List<char>();

            for (int i = pRowIndex+1; (i < gridDimension) && (this.grid[r, pColIndex].Value != Letter.NoLetter); i++)
            {
                lstChars.Add(this.grid[i, pColIndex].Value);
            }
            pColBelow = new string(lstChars.ToArray());
        }

        public void GetRow(int pColIndex, int pRowIndex, out string pRowLeft, out string pRowRight)
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
            pRowLeft = new string(lstChars.ToArray());
            lstChars = new List<char>();

            for (int i = pRowIndex + 1; (i < gridDimension) && (this.grid[pRowIndex, c].Value != Letter.NoLetter); i++)
            {
                lstChars.Add(this.grid[pRowIndex, i].Value);
            }
            pRowRight = new string(lstChars.ToArray());
        }

        public void RefreshSquare(int pRowIndex, int pColIndex)
        {
            GetColumn(pRowIndex, pColIndex, out string pColAbove, out string pColBelow);
            GetRow(pRowIndex, pColIndex, out string pRowLeft, out string pRowRight);
           
            Square square = grid[pRowIndex, pColIndex];
            List<Letter> valchars = square.ValidValues;

            //Check Column
            for(int i = 0; i < valchars.Count; i++)
            {                
                if (dict.CheckWord(pColAbove + square.ValidValues[i].Value + pColBelow) == false)
                {
                    square.RemoveLetter(valchars[i]);
                }
            }

            // Check Row
            for (int i = 0; i < valchars.Count; i++)
            {
                if (dict.CheckWord(pRowLeft + square.ValidValues[i].Value + pRowRight) == false)
                {
                    square.RemoveLetter(valchars[i]);
                }
            }
        }
    }
}
