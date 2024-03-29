﻿namespace TestSearch
{
    partial class SearchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dirTextBox = new System.Windows.Forms.TextBox();
            this.iconList = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.templateTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.browseButton = new System.Windows.Forms.Button();
            this.templateLabel = new System.Windows.Forms.Label();
            this.dirLabel = new System.Windows.Forms.Label();
            this.resultView = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.processingFileLabel = new System.Windows.Forms.Label();
            this.processingLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.inTextCheck = new System.Windows.Forms.CheckBox();
            this.binaryCheckCheck = new System.Windows.Forms.CheckBox();
            this.wildRadioButton = new System.Windows.Forms.RadioButton();
            this.regRadioButton = new System.Windows.Forms.RadioButton();
            this.folderCheck = new System.Windows.Forms.CheckBox();
            this.caseCheck = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.foundNum = new System.Windows.Forms.Label();
            this.foundLabel = new System.Windows.Forms.Label();
            this.processedNum = new System.Windows.Forms.Label();
            this.processedLabel = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.elapsedTime = new System.Windows.Forms.Label();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.elapsedTimeTimer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // dirTextBox
            // 
            this.dirTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dirTextBox.Location = new System.Drawing.Point(125, 3);
            this.dirTextBox.MinimumSize = new System.Drawing.Size(70, 4);
            this.dirTextBox.Name = "dirTextBox";
            this.dirTextBox.Size = new System.Drawing.Size(272, 22);
            this.dirTextBox.TabIndex = 0;
            this.dirTextBox.TextChanged += new System.EventHandler(this.dirTextBox_TextChanged);
            this.dirTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dirTextBox_KeyUp);
            this.dirTextBox.Leave += new System.EventHandler(this.dirTextBox_Leave);
            // 
            // iconList
            // 
            this.iconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.iconList.ImageSize = new System.Drawing.Size(16, 16);
            this.iconList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.resultView, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(569, 510);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.templateTextBox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.dirTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.searchButton, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.stopButton, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.browseButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.templateLabel, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.dirLabel, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.MinimumSize = new System.Drawing.Size(370, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(563, 66);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // templateTextBox
            // 
            this.templateTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.templateTextBox.Location = new System.Drawing.Point(125, 36);
            this.templateTextBox.MinimumSize = new System.Drawing.Size(70, 4);
            this.templateTextBox.Name = "templateTextBox";
            this.templateTextBox.Size = new System.Drawing.Size(272, 22);
            this.templateTextBox.TabIndex = 5;
            this.templateTextBox.TextChanged += new System.EventHandler(this.templateTextBox_TextChanged);
            this.templateTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.templateTextBox_KeyUp);
            this.templateTextBox.Leave += new System.EventHandler(this.templateTextBox_Leave);
            // 
            // searchButton
            // 
            this.searchButton.AutoSize = true;
            this.searchButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchButton.Enabled = false;
            this.searchButton.Location = new System.Drawing.Point(403, 36);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(76, 27);
            this.searchButton.TabIndex = 3;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.AutoSize = true;
            this.stopButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(485, 36);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 27);
            this.stopButton.TabIndex = 4;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // browseButton
            // 
            this.browseButton.AutoSize = true;
            this.browseButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browseButton.Location = new System.Drawing.Point(403, 3);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(76, 27);
            this.browseButton.TabIndex = 7;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // templateLabel
            // 
            this.templateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.templateLabel.Location = new System.Drawing.Point(3, 33);
            this.templateLabel.Name = "templateLabel";
            this.templateLabel.Size = new System.Drawing.Size(116, 33);
            this.templateLabel.TabIndex = 6;
            this.templateLabel.Text = "Search template:";
            this.templateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dirLabel
            // 
            this.dirLabel.AutoSize = true;
            this.dirLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dirLabel.Location = new System.Drawing.Point(3, 0);
            this.dirLabel.Name = "dirLabel";
            this.dirLabel.Size = new System.Drawing.Size(116, 33);
            this.dirLabel.TabIndex = 2;
            this.dirLabel.Text = "Search directory:";
            this.dirLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // resultView
            // 
            this.resultView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultView.ImageIndex = 0;
            this.resultView.ImageList = this.iconList;
            this.resultView.Location = new System.Drawing.Point(3, 350);
            this.resultView.Name = "resultView";
            this.resultView.SelectedImageIndex = 0;
            this.resultView.Size = new System.Drawing.Size(563, 157);
            this.resultView.TabIndex = 1;
            this.resultView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.resultView_NodeMouseDoubleClick);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.processingFileLabel, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.processingLabel, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 215);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(563, 39);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // processingFileLabel
            // 
            this.processingFileLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.processingFileLabel.Location = new System.Drawing.Point(103, 0);
            this.processingFileLabel.MinimumSize = new System.Drawing.Size(0, 38);
            this.processingFileLabel.Name = "processingFileLabel";
            this.processingFileLabel.Size = new System.Drawing.Size(457, 39);
            this.processingFileLabel.TabIndex = 3;
            this.processingFileLabel.Text = "filename";
            // 
            // processingLabel
            // 
            this.processingLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.processingLabel.Location = new System.Drawing.Point(3, 0);
            this.processingLabel.Name = "processingLabel";
            this.processingLabel.Size = new System.Drawing.Size(94, 39);
            this.processingLabel.TabIndex = 2;
            this.processingLabel.Text = "Processing:";
            this.processingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.tableLayoutPanel4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(563, 134);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search options";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.inTextCheck, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.binaryCheckCheck, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.wildRadioButton, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.regRadioButton, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.folderCheck, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.caseCheck, 0, 3);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(6, 21);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(388, 108);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // inTextCheck
            // 
            this.inTextCheck.AutoSize = true;
            this.inTextCheck.Location = new System.Drawing.Point(3, 3);
            this.inTextCheck.Name = "inTextCheck";
            this.inTextCheck.Size = new System.Drawing.Size(67, 21);
            this.inTextCheck.TabIndex = 0;
            this.inTextCheck.Text = "In text";
            this.inTextCheck.UseVisualStyleBackColor = true;
            this.inTextCheck.CheckedChanged += new System.EventHandler(this.inTextCheck_CheckedChanged);
            // 
            // binaryCheckCheck
            // 
            this.binaryCheckCheck.AutoSize = true;
            this.binaryCheckCheck.Location = new System.Drawing.Point(3, 30);
            this.binaryCheckCheck.Name = "binaryCheckCheck";
            this.binaryCheckCheck.Size = new System.Drawing.Size(111, 21);
            this.binaryCheckCheck.TabIndex = 1;
            this.binaryCheckCheck.Text = "Binary check";
            this.binaryCheckCheck.UseVisualStyleBackColor = true;
            this.binaryCheckCheck.CheckedChanged += new System.EventHandler(this.binaryCheckCheck_CheckedChanged);
            // 
            // wildRadioButton
            // 
            this.wildRadioButton.AutoSize = true;
            this.wildRadioButton.Checked = true;
            this.wildRadioButton.Location = new System.Drawing.Point(130, 3);
            this.wildRadioButton.Name = "wildRadioButton";
            this.wildRadioButton.Size = new System.Drawing.Size(91, 21);
            this.wildRadioButton.TabIndex = 2;
            this.wildRadioButton.TabStop = true;
            this.wildRadioButton.Text = "Wildcards";
            this.wildRadioButton.UseVisualStyleBackColor = true;
            this.wildRadioButton.CheckedChanged += new System.EventHandler(this.wildRadioButton_CheckedChanged);
            // 
            // regRadioButton
            // 
            this.regRadioButton.AutoSize = true;
            this.regRadioButton.Location = new System.Drawing.Point(130, 30);
            this.regRadioButton.Name = "regRadioButton";
            this.regRadioButton.Size = new System.Drawing.Size(77, 21);
            this.regRadioButton.TabIndex = 3;
            this.regRadioButton.Text = "Regexp";
            this.regRadioButton.UseVisualStyleBackColor = true;
            // 
            // folderCheck
            // 
            this.folderCheck.AutoSize = true;
            this.folderCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.folderCheck.Location = new System.Drawing.Point(3, 57);
            this.folderCheck.Name = "folderCheck";
            this.folderCheck.Size = new System.Drawing.Size(121, 21);
            this.folderCheck.TabIndex = 4;
            this.folderCheck.Text = "Folders";
            this.folderCheck.UseVisualStyleBackColor = true;
            this.folderCheck.CheckedChanged += new System.EventHandler(this.folderCheck_CheckedChanged);
            // 
            // caseCheck
            // 
            this.caseCheck.AutoSize = true;
            this.caseCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.caseCheck.Location = new System.Drawing.Point(3, 84);
            this.caseCheck.Name = "caseCheck";
            this.caseCheck.Size = new System.Drawing.Size(121, 21);
            this.caseCheck.TabIndex = 5;
            this.caseCheck.Text = "Case sensitive";
            this.caseCheck.UseVisualStyleBackColor = true;
            this.caseCheck.CheckedChanged += new System.EventHandler(this.caseCheck_CheckedChanged);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.foundNum, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.foundLabel, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.processedNum, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.processedLabel, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.timeLabel, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.elapsedTime, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 260);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(563, 84);
            this.tableLayoutPanel5.TabIndex = 4;
            // 
            // foundNum
            // 
            this.foundNum.AutoSize = true;
            this.foundNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foundNum.Location = new System.Drawing.Point(103, 55);
            this.foundNum.Name = "foundNum";
            this.foundNum.Size = new System.Drawing.Size(457, 29);
            this.foundNum.TabIndex = 3;
            this.foundNum.Text = "num";
            this.foundNum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // foundLabel
            // 
            this.foundLabel.AutoSize = true;
            this.foundLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foundLabel.Location = new System.Drawing.Point(3, 55);
            this.foundLabel.Name = "foundLabel";
            this.foundLabel.Size = new System.Drawing.Size(94, 29);
            this.foundLabel.TabIndex = 2;
            this.foundLabel.Text = "Found:";
            this.foundLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // processedNum
            // 
            this.processedNum.AutoSize = true;
            this.processedNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processedNum.Location = new System.Drawing.Point(103, 27);
            this.processedNum.Name = "processedNum";
            this.processedNum.Size = new System.Drawing.Size(457, 28);
            this.processedNum.TabIndex = 1;
            this.processedNum.Text = "num";
            this.processedNum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // processedLabel
            // 
            this.processedLabel.AutoSize = true;
            this.processedLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processedLabel.Location = new System.Drawing.Point(3, 27);
            this.processedLabel.Name = "processedLabel";
            this.processedLabel.Size = new System.Drawing.Size(94, 28);
            this.processedLabel.TabIndex = 0;
            this.processedLabel.Text = "Processed:";
            this.processedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeLabel.Location = new System.Drawing.Point(3, 0);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(94, 27);
            this.timeLabel.TabIndex = 0;
            this.timeLabel.Text = "Elapsed time:";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // elapsedTime
            // 
            this.elapsedTime.AutoSize = true;
            this.elapsedTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elapsedTime.Location = new System.Drawing.Point(103, 0);
            this.elapsedTime.Name = "elapsedTime";
            this.elapsedTime.Size = new System.Drawing.Size(457, 27);
            this.elapsedTime.TabIndex = 1;
            this.elapsedTime.Text = "time";
            this.elapsedTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // elapsedTimeTimer
            // 
            this.elapsedTimeTimer.Interval = 200;
            this.elapsedTimeTimer.Tick += new System.EventHandler(this.elapsedTimeTimer_Tick);
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 510);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(400, 47);
            this.Name = "SearchForm";
            this.Text = "Search";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList iconList;
        private System.Windows.Forms.TextBox dirTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TreeView resultView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label processingLabel;
        private System.Windows.Forms.Label processingFileLabel;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Label templateLabel;
        private System.Windows.Forms.TextBox templateTextBox;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.CheckBox inTextCheck;
        private System.Windows.Forms.CheckBox binaryCheckCheck;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Label dirLabel;
        private System.Windows.Forms.RadioButton wildRadioButton;
        private System.Windows.Forms.RadioButton regRadioButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label elapsedTime;
        private System.Windows.Forms.Timer elapsedTimeTimer;
        private System.Windows.Forms.Label processedLabel;
        private System.Windows.Forms.Label processedNum;
        private System.Windows.Forms.Label foundLabel;
        private System.Windows.Forms.Label foundNum;
        private System.Windows.Forms.CheckBox folderCheck;
        private System.Windows.Forms.CheckBox caseCheck;
    }
}

