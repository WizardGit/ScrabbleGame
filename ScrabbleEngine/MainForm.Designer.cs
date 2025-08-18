namespace ScrabbleEngine
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            LettersLabel = new Label();
            ProgressLabel = new Label();
            InstructionsLabel = new Label();
            LettersTextbox = new TextBox();
            MaskTextBox = new TextBox();
            LineCheckMaskTextBox = new TextBox();
            CheckWordTextBox = new TextBox();
            DisplayListBox = new ListBox();
            LineCheckBtn = new Button();
            ProcessBtn = new Button();
            SortPointsBtn = new Button();
            CheckWordBtn = new Button();
            ProgressBar = new ProgressBar();
            gridTableLayout = new TableLayoutPanel();
            SortComboBox = new ComboBox();
            ListWordsBtn = new Button();
            ValidateWordsBtn = new Button();
            CalcNewWordBtn = new Button();
            PointsScoredLbl = new Label();
            DisplayPointsLabel = new Label();
            RefreshBoardBtn = new Button();
            SuspendLayout();
            // 
            // LettersLabel
            // 
            LettersLabel.AutoSize = true;
            LettersLabel.Location = new Point(12, 22);
            LettersLabel.Name = "LettersLabel";
            LettersLabel.Size = new Size(42, 15);
            LettersLabel.TabIndex = 0;
            LettersLabel.Text = "Letters";
            // 
            // ProgressLabel
            // 
            ProgressLabel.AutoSize = true;
            ProgressLabel.Location = new Point(352, 150);
            ProgressLabel.Name = "ProgressLabel";
            ProgressLabel.Size = new Size(52, 15);
            ProgressLabel.TabIndex = 1;
            ProgressLabel.Text = "Progress";
            // 
            // InstructionsLabel
            // 
            InstructionsLabel.AutoSize = true;
            InstructionsLabel.Location = new Point(352, 236);
            InstructionsLabel.Name = "InstructionsLabel";
            InstructionsLabel.Size = new Size(126, 15);
            InstructionsLabel.TabIndex = 2;
            InstructionsLabel.Text = "Use * as wildcard letter";
            // 
            // LettersTextbox
            // 
            LettersTextbox.Location = new Point(12, 40);
            LettersTextbox.Name = "LettersTextbox";
            LettersTextbox.Size = new Size(100, 23);
            LettersTextbox.TabIndex = 3;
            LettersTextbox.Text = "abcdefg";
            // 
            // MaskTextBox
            // 
            MaskTextBox.Location = new Point(480, 102);
            MaskTextBox.Name = "MaskTextBox";
            MaskTextBox.Size = new Size(100, 23);
            MaskTextBox.TabIndex = 4;
            MaskTextBox.Text = "-------";
            // 
            // LineCheckMaskTextBox
            // 
            LineCheckMaskTextBox.Location = new Point(480, 74);
            LineCheckMaskTextBox.Name = "LineCheckMaskTextBox";
            LineCheckMaskTextBox.Size = new Size(100, 23);
            LineCheckMaskTextBox.TabIndex = 5;
            LineCheckMaskTextBox.Text = "-a------d----z-";
            // 
            // CheckWordTextBox
            // 
            CheckWordTextBox.Location = new Point(480, 354);
            CheckWordTextBox.Name = "CheckWordTextBox";
            CheckWordTextBox.Size = new Size(100, 23);
            CheckWordTextBox.TabIndex = 6;
            // 
            // DisplayListBox
            // 
            DisplayListBox.FormattingEnabled = true;
            DisplayListBox.ItemHeight = 15;
            DisplayListBox.Location = new Point(12, 73);
            DisplayListBox.Name = "DisplayListBox";
            DisplayListBox.Size = new Size(334, 304);
            DisplayListBox.TabIndex = 7;
            // 
            // LineCheckBtn
            // 
            LineCheckBtn.Location = new Point(352, 73);
            LineCheckBtn.Name = "LineCheckBtn";
            LineCheckBtn.Size = new Size(122, 23);
            LineCheckBtn.TabIndex = 8;
            LineCheckBtn.Text = "CheckLine";
            LineCheckBtn.UseVisualStyleBackColor = true;
            LineCheckBtn.Click += LineCheckBtn_Click;
            // 
            // ProcessBtn
            // 
            ProcessBtn.Location = new Point(352, 102);
            ProcessBtn.Name = "ProcessBtn";
            ProcessBtn.Size = new Size(122, 23);
            ProcessBtn.TabIndex = 9;
            ProcessBtn.Text = "Process";
            ProcessBtn.UseVisualStyleBackColor = true;
            ProcessBtn.Click += ProcessBtn_Click;
            // 
            // SortPointsBtn
            // 
            SortPointsBtn.Location = new Point(356, 295);
            SortPointsBtn.Name = "SortPointsBtn";
            SortPointsBtn.Size = new Size(122, 23);
            SortPointsBtn.TabIndex = 11;
            SortPointsBtn.Text = "Sort by Points";
            SortPointsBtn.UseVisualStyleBackColor = true;
            SortPointsBtn.Click += SortPointsBtn_Click;
            // 
            // CheckWordBtn
            // 
            CheckWordBtn.Location = new Point(352, 354);
            CheckWordBtn.Name = "CheckWordBtn";
            CheckWordBtn.Size = new Size(122, 23);
            CheckWordBtn.TabIndex = 12;
            CheckWordBtn.Text = "Check Word";
            CheckWordBtn.UseVisualStyleBackColor = true;
            CheckWordBtn.Click += CheckWordBtn_Click;
            // 
            // ProgressBar
            // 
            ProgressBar.Location = new Point(352, 168);
            ProgressBar.Name = "ProgressBar";
            ProgressBar.Size = new Size(228, 23);
            ProgressBar.TabIndex = 13;
            // 
            // gridTableLayout
            // 
            gridTableLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            gridTableLayout.ColumnCount = 15;
            gridTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            gridTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            gridTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            gridTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            gridTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            gridTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            gridTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            gridTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            gridTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            gridTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            gridTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            gridTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            gridTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            gridTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            gridTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            gridTableLayout.Location = new Point(590, 70);
            gridTableLayout.Name = "gridTableLayout";
            gridTableLayout.RowCount = 15;
            gridTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            gridTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            gridTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            gridTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            gridTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            gridTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            gridTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            gridTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            gridTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            gridTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            gridTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            gridTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            gridTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            gridTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            gridTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            gridTableLayout.Size = new Size(316, 316);
            gridTableLayout.TabIndex = 14;
            // 
            // SortComboBox
            // 
            SortComboBox.FormattingEnabled = true;
            SortComboBox.Items.AddRange(new object[] { "Ascending", "Descending" });
            SortComboBox.Location = new Point(481, 296);
            SortComboBox.Name = "SortComboBox";
            SortComboBox.Size = new Size(103, 23);
            SortComboBox.TabIndex = 15;
            SortComboBox.SelectedIndexChanged += SortComboBox_SelectedIndexChanged;
            // 
            // ListWordsBtn
            // 
            ListWordsBtn.Location = new Point(593, 27);
            ListWordsBtn.Name = "ListWordsBtn";
            ListWordsBtn.Size = new Size(86, 23);
            ListWordsBtn.TabIndex = 16;
            ListWordsBtn.Text = "List Words";
            ListWordsBtn.UseVisualStyleBackColor = true;
            ListWordsBtn.Click += ListWordsBtn_Click;
            // 
            // ValidateWordsBtn
            // 
            ValidateWordsBtn.Location = new Point(805, 26);
            ValidateWordsBtn.Name = "ValidateWordsBtn";
            ValidateWordsBtn.Size = new Size(101, 23);
            ValidateWordsBtn.TabIndex = 17;
            ValidateWordsBtn.Text = "Validate Words";
            ValidateWordsBtn.UseVisualStyleBackColor = true;
            ValidateWordsBtn.Click += ValidateWordsBtn_Click;
            // 
            // CalcNewWordBtn
            // 
            CalcNewWordBtn.Location = new Point(593, 392);
            CalcNewWordBtn.Name = "CalcNewWordBtn";
            CalcNewWordBtn.Size = new Size(166, 49);
            CalcNewWordBtn.TabIndex = 19;
            CalcNewWordBtn.Text = "Calculate New Word";
            CalcNewWordBtn.UseVisualStyleBackColor = true;
            CalcNewWordBtn.Click += CalcNewWordBtn_Click;
            // 
            // PointsScoredLbl
            // 
            PointsScoredLbl.AutoSize = true;
            PointsScoredLbl.Location = new Point(352, 392);
            PointsScoredLbl.Name = "PointsScoredLbl";
            PointsScoredLbl.Size = new Size(79, 15);
            PointsScoredLbl.TabIndex = 20;
            PointsScoredLbl.Text = "Points Scored";
            // 
            // DisplayPointsLabel
            // 
            DisplayPointsLabel.AutoSize = true;
            DisplayPointsLabel.Location = new Point(352, 426);
            DisplayPointsLabel.Name = "DisplayPointsLabel";
            DisplayPointsLabel.Size = new Size(62, 15);
            DisplayPointsLabel.TabIndex = 21;
            DisplayPointsLabel.Text = "No Points!";
            // 
            // RefreshBoardBtn
            // 
            RefreshBoardBtn.Location = new Point(780, 392);
            RefreshBoardBtn.Name = "RefreshBoardBtn";
            RefreshBoardBtn.Size = new Size(127, 49);
            RefreshBoardBtn.TabIndex = 22;
            RefreshBoardBtn.Text = "Refresh Board";
            RefreshBoardBtn.UseVisualStyleBackColor = true;
            RefreshBoardBtn.Click += RefreshBoardBtn_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(919, 444);
            Controls.Add(RefreshBoardBtn);
            Controls.Add(DisplayPointsLabel);
            Controls.Add(PointsScoredLbl);
            Controls.Add(CalcNewWordBtn);
            Controls.Add(ValidateWordsBtn);
            Controls.Add(ListWordsBtn);
            Controls.Add(SortComboBox);
            Controls.Add(gridTableLayout);
            Controls.Add(ProgressBar);
            Controls.Add(CheckWordBtn);
            Controls.Add(SortPointsBtn);
            Controls.Add(ProcessBtn);
            Controls.Add(LineCheckBtn);
            Controls.Add(DisplayListBox);
            Controls.Add(CheckWordTextBox);
            Controls.Add(LineCheckMaskTextBox);
            Controls.Add(MaskTextBox);
            Controls.Add(LettersTextbox);
            Controls.Add(InstructionsLabel);
            Controls.Add(ProgressLabel);
            Controls.Add(LettersLabel);
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LettersLabel;
        private Label ProgressLabel;
        private Label InstructionsLabel;
        private TextBox LettersTextbox;
        private TextBox MaskTextBox;
        private TextBox LineCheckMaskTextBox;
        private TextBox CheckWordTextBox;
        private ListBox DisplayListBox;
        private Button LineCheckBtn;
        private Button ProcessBtn;
        private Button SortPointsBtn;
        private Button CheckWordBtn;
        private ProgressBar ProgressBar;
        private TableLayoutPanel gridTableLayout;
        private ComboBox SortComboBox;
        private Button ListWordsBtn;
        private Button ValidateWordsBtn;
        private Button CalcNewWordBtn;
        private Label PointsScoredLbl;
        private Label DisplayPointsLabel;
        private Button RefreshBoardBtn;
    }
}
