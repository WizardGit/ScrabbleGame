
using System.Diagnostics;

/*
 * TODO:
 *   what exactly is refresh checking?
 *
 *   does findnewword find wordsand multiple others words created?
 *   methodically test the new row check functionality
 *   duplicate that for the column check functionality
 *   change up the more front end methods to handle lists of lists and properly display that
 *   sort is broken because of listlistword
 *   we need to be able to handle if our letter is an empty square   
 * 
 * NOTE: with wildcard letters, it's gonna mark a letter not in the list of letters as used and then say the word is playable
 * once the word is played, it looks like a wildcard letter wasn't used, but it was.  For now, we're gonna do it ths way
 * 
 */

namespace ScrabbleEngine
{
    public partial class MainForm : Form
    {
        private bool blnSortAscending;
        private List<Word> lstWords;
        private List<List<Word>> lstLstWords;
        private Board dataBoard;

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Testing speeds of DAWG vs dictionary - can really vary but seems like over lots of word checks dawg is better
            //Console.WriteLine("starting");

            //bool exists;
            //Stopwatch stopwatch;

            //Dictionary ddict = new Dictionary();
            //stopwatch = Stopwatch.StartNew(); // Start timing
            //exists = ddict.CheckWord("amazing");
            //exists = ddict.CheckWord("night");
            //exists = ddict.CheckWord("zebra");
            //System.Threading.Thread.Sleep(300);
            //stopwatch.Stop(); // Stop timing
            //Debug.WriteLine($"Word exists: {exists}");
            //Debug.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");

            //var dawg = new DAWG();
            //dawg.BuildFromFile();
            //stopwatch = Stopwatch.StartNew(); // Start timing
            //exists = dawg.Search("amazing");
            //exists = dawg.Search("night");
            //exists = dawg.Search("zebra");
            //System.Threading.Thread.Sleep(300);
            //stopwatch.Stop(); // Stop timing
            //Debug.WriteLine($"Word exists: {exists}");
            //Debug.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
        }
        public MainForm()
        {
            InitializeComponent();
            dataBoard = new Board();
            blnSortAscending = true;
            lstWords = new List<Word>();
            lstLstWords = new List<List<Word>>();
            SortComboBox.SelectedIndex = 0;
            CreateBoard();
            HookUpTextChangedHandlers();
        }

        private void HookUpTextChangedHandlers()
        {
            foreach (Control c in gridTableLayout.Controls)
            {
                if (c is TextBox tb)
                {
                    tb.TextChanged += TextBox_TextChanged;
                }
            }
        }

        private void TextBox_TextChanged(object? sender, EventArgs e)
        {
            // Do your logic here
            if (sender is TextBox changedBox)
            {
                TableLayoutPanelCellPosition pos = gridTableLayout.GetPositionFromControl(changedBox);

                if (changedBox.Text.Length > 1)
                {
                    // The text boxes are restricted to one letter, so this should never be reached!
                    MessageBox.Show("You can only have one letter in each square!", "Invalid Letter", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    changedBox.Text = "";
                }
                else if (changedBox.Text.Length == 0)
                {
                    return;
                }
                else if (dataBoard[pos.Row, pos.Column].IsValid(changedBox.Text[0]) == false)
                {
                    MessageBox.Show("Not a valid letter to put here!", "Invalid Letter", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    changedBox.Text = "";
                }
                Console.WriteLine($"Text changed at Row: {pos.Row}, Column: {pos.Column}");
            }
        }

        public char[,] GetTableLayoutPanelBoard()
        {
            char[,] board = new char[15, 15];

            for (int row = 0; row < 15; row++)
            {
                for (int col = 0; col < 15; col++)
                {
                    if (gridTableLayout.GetControlFromPosition(col, row) is TextBox tb)
                    {
                        if (tb.Text.Length == 0)
                            board[row, col] = Letter.NoLetter;
                        else
                            board[row, col] = tb.Text[0];
                    }
                    else
                        throw new Exception("One of the form tablelayout textboxes is null...");
                }
            }
            return board;
        }

        private void CreateBoard()
        {
            //Set text boxes in my table layout form object
            TextBox[,] board = new TextBox[15, 15];

            for (int row = 0; row < 15; row++)
            {
                for (int col = 0; col < 15; col++)
                {
                    TextBox tb = new TextBox
                    {
                        MaxLength = 1,
                        TextAlign = HorizontalAlignment.Center,
                        Margin = new Padding(0),
                        Width = 20,
                        ForeColor = System.Drawing.Color.LimeGreen,
                        Font = new Font(Font, FontStyle.Bold)
                    };

                    board[row, col] = tb;
                    gridTableLayout.Controls.Add(tb, col, row);
                }
            }

            //Set the text box colors according to their bonus
            for (int r = 0; r < dataBoard.GridDimension; r++)
            {
                for (int c = 0; c < dataBoard.GridDimension; c++)
                {
                    if (dataBoard[r, c].Bonus == Square.BonusType.doubleLetter)
                        board[r, c].BackColor = Color.LightBlue;
                    else if (dataBoard[r, c].Bonus == Square.BonusType.tripleLetter)
                        board[r, c].BackColor = Color.Blue;
                    else if (dataBoard[r, c].Bonus == Square.BonusType.doubleWord)
                        board[r, c].BackColor = Color.Pink;
                    else if (dataBoard[r, c].Bonus == Square.BonusType.tripleWord)
                        board[r, c].BackColor = Color.Red;
                }
            }
        }

        private void LineCheckBtn_Click(object sender, EventArgs e)
        {
            string strLetters = LettersTextbox.Text.ToLower().Trim();
            string strMask = LineCheckMaskTextBox.Text;

            Line line = new Line(strMask);
            line.LineCheck(strLetters, out List<Word> pLstStrWords, ProgressBar);

            lstWords = pLstStrWords;

            DisplayListBox.Items.Clear();

            foreach (Word word in pLstStrWords)
            {
                DisplayListBox.Items.Add(word.PrintWordIndexPoints());
            }
        }

        private void ProcessBtn_Click(object sender, EventArgs e)
        {
            string strLetters = LettersTextbox.Text.ToLower().Trim();
            string strMask = MaskTextBox.Text;

            Line line = new Line(strMask);
            line.SimpleLineCheck(strLetters, out List<Word> pLstStrWords, ProgressBar);

            lstWords = pLstStrWords;

            DisplayListBox.Items.Clear();

            foreach (Word word in pLstStrWords)
            {
                DisplayListBox.Items.Add(word.PrintWordPoints());
            }
        }

        private void SortLengthBtn_Click(object sender, EventArgs e)
        {
            DisplayListBox.Items.Clear();

            lstWords.Sort((a, b) =>
            {
                return blnSortAscending
                    ? a.Length.CompareTo(b.Length)      // Ascending
                    : b.Length.CompareTo(a.Length);     // Descending
            });

            //lstWords.Sort((a, b) => b.Points.CompareTo(a.Points));
            foreach (Word word in lstWords)
            {
                DisplayListBox.Items.Add(word.PrintWordIndexPoints());
            }
        }

        private void SortPointsBtn_Click(object sender, EventArgs e)
        {
            DisplayListBox.Items.Clear();

            lstWords.Sort((a, b) =>
            {
                return blnSortAscending
                    ? a.Points.CompareTo(b.Points)      // Ascending
                    : b.Points.CompareTo(a.Points);     // Descending
            });

            foreach (Word word in lstWords)
            {
                DisplayListBox.Items.Add(word.PrintWordIndexPoints());
            }
        }

        private void CheckWordBtn_Click(object sender, EventArgs e)
        {
            //All the words in the dictionary are lowercase, so let's just do that and match
            string strCheckWord = CheckWordTextBox.Text.ToLower().Trim();
            Dictionary dict = new Dictionary();

            if (dict.CheckWord(strCheckWord) == true)
                MessageBox.Show(strCheckWord + " is a Scrabble word!", "Word Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(strCheckWord + " is NOT a Scrabble word!", "Word Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        private void SortComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SortComboBox.SelectedIndex == 0)
            {
                blnSortAscending = true;
            }
            else if (SortComboBox.SelectedIndex == 1)
            {
                blnSortAscending = false;
            }
            else
            {
                throw new Exception("Not Valid selection");
            }
        }

        private void ListWordsBtn_Click(object sender, EventArgs e)
        {
            string strLetters = LettersTextbox.Text.ToLower().Trim();
            List<List<Word>> lstStrWords = dataBoard.BoardCheck(strLetters, ProgressBar);
            lstLstWords = lstStrWords;
            DisplayListBox.Items.Clear();

            foreach(List<Word> lstWords in lstStrWords)
            {
                string strBigWords = "";
                int totalPoints = 0;

                foreach (Word word in lstWords)
                {
                    strBigWords += word.PrintWordIndexPoints() + " ";
                    totalPoints += word.Points;                    
                }
                DisplayListBox.Items.Add(strBigWords + " (" + totalPoints.ToString() + ")");
            }            
        }

        private void RefreshValidBtn_Click(object sender, EventArgs e)
        {
            dataBoard.RefreshBoard(ProgressBar);
        }

        private void ValidateWordsBtn_Click(object sender, EventArgs e)
        {
            if (dataBoard.CheckPlayedWords(out string wrongWord) == false)
            {
                MessageBox.Show(wrongWord + " is NOT a valid Scrabble word!", "Word Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("All words are valid Scrabble words!", "Word Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Compares pBoard against this.grid to find new words on pBoard.  
        /// Unless specified, this method assumes all words on board are valid
        /// </summary>
        /// <param name="pBoard"></param>
        /// <returns>Returns list of new words on board</returns>
        /// <exception cref="Exception">Square that was different but no new words there...???</exception>
        public List<Word> GetNewWord(Board pOldBoard, Board pNewBoard, bool pVerifyFirst = false)
        {
            if (pVerifyFirst == true)
            {
                throw new Exception("Board has at least one incorrectly played word on it!");
            }

            List<Word> lstWords = new List<Word>();

            for (int r = 0; r < pOldBoard.GridDimension; r++)
            {
                for (int c = 0; c < pOldBoard.GridDimension; c++)
                {
                    if (pOldBoard[r, c].Value != pNewBoard[r, c].Value)
                    {
                        if (pNewBoard.GetWord(r, c, out Word pColWord, out Word pRowWord) == true)
                        {
                            //GetWord will just return any letters consecutively.
                            //If we get no word or the word is just one letter, ignore it, we (should have anyway) validated that we don't have any bad words in play
                            if ((pColWord.Value != "") && (pColWord.Length > 1))
                                pColWord.AddToList(ref lstWords, true);
                            if ((pRowWord.Value != "") && (pRowWord.Length > 1))
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

        private void CalcNewWordBtn_Click(object sender, EventArgs e)
        {
            int multiplicationFactor = 1;

            if ((gridTableLayout.GetControlFromPosition(7, 7) is not TextBox tb) || 
                (tb.Text.Length != 1) || 
                (char.IsLetter(tb.Text[0]) == false))
            {
                MessageBox.Show("There is no word on the middle space!", "Word Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (dataBoard.IsEmpty() == true)
            {
                // First scrabble word gets automatic double word score
                // It's impossible to hit any other word bonuses on the first word play
                multiplicationFactor *= 2;
            }

            Board charBoard = new Board(GetTableLayoutPanelBoard());
            List<Word> newWords = GetNewWord(charBoard, dataBoard);

            foreach (Word newWord in newWords)
            {
                if (newWord.Value == "")
                {
                    MessageBox.Show("New Word is not valid!", "Word Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int finalScore = 0;
                //Validate that it is valid
                Dictionary dict = new Dictionary();
                if (dict.CheckWord(newWord.Value) == false)
                {
                    MessageBox.Show("New Word is not valid!", "Word Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //Calculate its points
                int colIndex = newWord.ColumnIndex;
                int rowIndex = newWord.RowIndex;
                bool isColumn = newWord.isColumn;
                bool isRow = newWord.isRow;


                if (isColumn == true)
                {
                    for (int i = 0; i < newWord.Value.Length; i++)
                    {
                        finalScore += dataBoard[rowIndex + i, colIndex].Points(ref multiplicationFactor);
                    }
                }
                else if (isRow == true)
                {
                    for (int i = 0; i < newWord.Value.Length; i++)
                    {
                        finalScore += dataBoard[rowIndex, colIndex + i].Points(ref multiplicationFactor);
                    }
                }
                else
                {
                    throw new Exception("word isn't row or column!");
                }
                //Set the output fields
                DisplayPointsLabel.Text = newWord.Value.ToString() + " scored " + (finalScore * multiplicationFactor).ToString() + " points!";
            }            

            dataBoard.SyncTableLayoutPanelToBoard(gridTableLayout);
        }

        private void RefreshBoardBtn_Click(object sender, EventArgs e)
        {
            dataBoard.SyncBoardToTableLayoutPanel(ref gridTableLayout);
        }
    }
}
