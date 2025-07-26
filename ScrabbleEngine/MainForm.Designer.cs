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
            SortLengthBtn = new Button();
            SortPointsBtn = new Button();
            CheckWordBtn = new Button();
            ProgressBar = new ProgressBar();
            gridTableLayout = new TableLayoutPanel();
            SortComboBox = new ComboBox();
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
            ProgressLabel.Location = new Point(205, 146);
            ProgressLabel.Name = "ProgressLabel";
            ProgressLabel.Size = new Size(52, 15);
            ProgressLabel.TabIndex = 1;
            ProgressLabel.Text = "Progress";
            // 
            // InstructionsLabel
            // 
            InstructionsLabel.AutoSize = true;
            InstructionsLabel.Location = new Point(205, 232);
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
            MaskTextBox.Location = new Point(333, 98);
            MaskTextBox.Name = "MaskTextBox";
            MaskTextBox.Size = new Size(100, 23);
            MaskTextBox.TabIndex = 4;
            MaskTextBox.Text = "-------";
            // 
            // LineCheckMaskTextBox
            // 
            LineCheckMaskTextBox.Location = new Point(333, 70);
            LineCheckMaskTextBox.Name = "LineCheckMaskTextBox";
            LineCheckMaskTextBox.Size = new Size(100, 23);
            LineCheckMaskTextBox.TabIndex = 5;
            LineCheckMaskTextBox.Text = "-a------d----z-";
            // 
            // CheckWordTextBox
            // 
            CheckWordTextBox.Location = new Point(333, 350);
            CheckWordTextBox.Name = "CheckWordTextBox";
            CheckWordTextBox.Size = new Size(100, 23);
            CheckWordTextBox.TabIndex = 6;
            // 
            // DisplayListBox
            // 
            DisplayListBox.FormattingEnabled = true;
            DisplayListBox.ItemHeight = 15;
            DisplayListBox.Location = new Point(12, 69);
            DisplayListBox.Name = "DisplayListBox";
            DisplayListBox.Size = new Size(187, 304);
            DisplayListBox.TabIndex = 7;
            // 
            // LineCheckBtn
            // 
            LineCheckBtn.Location = new Point(205, 69);
            LineCheckBtn.Name = "LineCheckBtn";
            LineCheckBtn.Size = new Size(122, 23);
            LineCheckBtn.TabIndex = 8;
            LineCheckBtn.Text = "CheckLine";
            LineCheckBtn.UseVisualStyleBackColor = true;
            LineCheckBtn.Click += LineCheckBtn_Click;
            // 
            // ProcessBtn
            // 
            ProcessBtn.Location = new Point(205, 98);
            ProcessBtn.Name = "ProcessBtn";
            ProcessBtn.Size = new Size(122, 23);
            ProcessBtn.TabIndex = 9;
            ProcessBtn.Text = "Process";
            ProcessBtn.UseVisualStyleBackColor = true;
            ProcessBtn.Click += ProcessBtn_Click;
            // 
            // SortLengthBtn
            // 
            SortLengthBtn.Location = new Point(205, 291);
            SortLengthBtn.Name = "SortLengthBtn";
            SortLengthBtn.Size = new Size(122, 23);
            SortLengthBtn.TabIndex = 10;
            SortLengthBtn.Text = "SortByLength";
            SortLengthBtn.UseVisualStyleBackColor = true;
            SortLengthBtn.Click += SortLengthBtn_Click;
            // 
            // SortPointsBtn
            // 
            SortPointsBtn.Location = new Point(205, 321);
            SortPointsBtn.Name = "SortPointsBtn";
            SortPointsBtn.Size = new Size(122, 23);
            SortPointsBtn.TabIndex = 11;
            SortPointsBtn.Text = "Sort by Points";
            SortPointsBtn.UseVisualStyleBackColor = true;
            SortPointsBtn.Click += SortPointsBtn_Click;
            // 
            // CheckWordBtn
            // 
            CheckWordBtn.Location = new Point(205, 350);
            CheckWordBtn.Name = "CheckWordBtn";
            CheckWordBtn.Size = new Size(122, 23);
            CheckWordBtn.TabIndex = 12;
            CheckWordBtn.Text = "Check Word";
            CheckWordBtn.UseVisualStyleBackColor = true;
            CheckWordBtn.Click += CheckWordBtn_Click;
            // 
            // ProgressBar
            // 
            ProgressBar.Location = new Point(205, 164);
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
            gridTableLayout.Location = new Point(443, 66);
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
            SortComboBox.Location = new Point(334, 292);
            SortComboBox.Name = "SortComboBox";
            SortComboBox.Size = new Size(103, 23);
            SortComboBox.TabIndex = 15;
            SortComboBox.SelectedIndexChanged += SortComboBox_SelectedIndexChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(772, 392);
            Controls.Add(SortComboBox);
            Controls.Add(gridTableLayout);
            Controls.Add(ProgressBar);
            Controls.Add(CheckWordBtn);
            Controls.Add(SortPointsBtn);
            Controls.Add(SortLengthBtn);
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
        private Button SortLengthBtn;
        private Button SortPointsBtn;
        private Button CheckWordBtn;
        private ProgressBar ProgressBar;
        private TableLayoutPanel gridTableLayout;
        private ComboBox SortComboBox;
    }
}
