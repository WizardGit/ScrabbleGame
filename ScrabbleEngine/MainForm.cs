
/*
 * TODO:
 * allow text box mask to restrict possible letters - maybe not mask it, but just send message box 
 * Add ability to place a word on the board
 */

namespace ScrabbleEngine
{
    public partial class MainForm : Form
    {
        private bool blnSortAscending;
        private List<Word> lstWords;
        private Board dataBoard;

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        public MainForm()
        {
            InitializeComponent();
            dataBoard = new Board();
            blnSortAscending = true;
            lstWords = new List<Word>();
            SortComboBox.SelectedIndex = 0;
            CreateBoard();
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
                DisplayListBox.Items.Add(word.PrintWordPoints());
            }
        }

        private void ProcessBtn_Click(object sender, EventArgs e)
        {
            string strLetters = LettersTextbox.Text;
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
                DisplayListBox.Items.Add(word.PrintWordPoints());
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
                DisplayListBox.Items.Add(word.PrintWordPoints());
            }
        }

        private void CheckWordBtn_Click(object sender, EventArgs e)
        {
            //All the words in the scrabble text list are uppercase, so let's just do that and match
            string strCheckWord = CheckWordTextBox.Text.ToUpper().Trim();

            string filePath = "ScrabbleWords.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Word list file not found.");
                return;
            }

            // Read words line by line
            using (StreamReader reader = new StreamReader(filePath))
            {
                string? word;
                while ((word = reader.ReadLine()) != null)
                {
                    if (word == strCheckWord)
                    {
                        MessageBox.Show(strCheckWord + " is a Scrabble word!", "Word Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            MessageBox.Show(strCheckWord + " is NOT a Scrabble word!", "Word Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
