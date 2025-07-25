
namespace ScrabbleEngine
{
    public class Board
    {
        public Square[,] grid;
        private const int gridDimension = 15;

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
            grid = new Square[gridDimension, gridDimension];

            for (int r = 0; r < gridDimension; r++)
            {
                for (int c = 0; c < gridDimension; c++)
                {
                    grid[r, c] = new Square();
                }
            }

            grid[0, 0] = new Square(Square.BonusType.tripleWord);
            grid[0, 1] = new Square(Square.BonusType.nothing);
            grid[0, 2] = new Square(Square.BonusType.nothing);
            grid[0, 3] = new Square(Square.BonusType.doubleLetter);
            grid[1, 1] = new Square(Square.BonusType.doubleWord);

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
                word.SetAsRow();
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
                        RefreshSquare(grid[r,c]);
                    }
                }
            }
        }

        public void RefreshSquare(Square square)
        {
            //Get word above (probably will need to create your own function, don't want word to be value on the square
            //get word below
            //get word to right
            //get word to left
            //combine column word
            //combine row word
            //remove any letters from validvalues that dont' make words for column, row, column+row
        }
    }
}
