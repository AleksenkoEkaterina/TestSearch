namespace TestSearch
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
            this.dirLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.searchButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.resultView = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.processingLabel = new System.Windows.Forms.Label();
            this.processingFileLabel = new System.Windows.Forms.Label();
            this.templateLabel = new System.Windows.Forms.Label();
            this.templateTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dirTextBox
            // 
            this.dirTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dirTextBox.Location = new System.Drawing.Point(125, 3);
            this.dirTextBox.MinimumSize = new System.Drawing.Size(70, 4);
            this.dirTextBox.Name = "dirTextBox";
            this.dirTextBox.Size = new System.Drawing.Size(104, 22);
            this.dirTextBox.TabIndex = 0;
            this.dirTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dirTextBox_KeyUp);
            this.dirTextBox.Leave += new System.EventHandler(this.dirTextBox_Leave);
            // 
            // iconList
            // 
            this.iconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.iconList.ImageSize = new System.Drawing.Size(16, 16);
            this.iconList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // dirLabel
            // 
            this.dirLabel.AutoSize = true;
            this.dirLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dirLabel.Location = new System.Drawing.Point(3, 0);
            this.dirLabel.Name = "dirLabel";
            this.dirLabel.Size = new System.Drawing.Size(116, 28);
            this.dirLabel.TabIndex = 2;
            this.dirLabel.Text = "Search directory:";
            this.dirLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.resultView, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(400, 542);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.templateLabel, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.templateTextBox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.dirLabel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.dirTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.searchButton, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.stopButton, 3, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.MinimumSize = new System.Drawing.Size(370, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(394, 54);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // searchButton
            // 
            this.searchButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchButton.Location = new System.Drawing.Point(235, 31);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 3;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(316, 31);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 4;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            // 
            // resultView
            // 
            this.resultView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultView.ImageIndex = 0;
            this.resultView.ImageList = this.iconList;
            this.resultView.Location = new System.Drawing.Point(3, 98);
            this.resultView.Name = "resultView";
            this.resultView.SelectedImageIndex = 0;
            this.resultView.Size = new System.Drawing.Size(394, 441);
            this.resultView.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.processingLabel, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.processingFileLabel, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 63);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(394, 29);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // processingLabel
            // 
            this.processingLabel.AutoSize = true;
            this.processingLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processingLabel.Location = new System.Drawing.Point(3, 0);
            this.processingLabel.Name = "processingLabel";
            this.processingLabel.Size = new System.Drawing.Size(82, 29);
            this.processingLabel.TabIndex = 2;
            this.processingLabel.Text = "Processing:";
            this.processingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // processingFileLabel
            // 
            this.processingFileLabel.AutoSize = true;
            this.processingFileLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processingFileLabel.Location = new System.Drawing.Point(91, 0);
            this.processingFileLabel.Name = "processingFileLabel";
            this.processingFileLabel.Size = new System.Drawing.Size(300, 29);
            this.processingFileLabel.TabIndex = 3;
            this.processingFileLabel.Text = "filename";
            this.processingFileLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // templateLabel
            // 
            this.templateLabel.AutoSize = true;
            this.templateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.templateLabel.Location = new System.Drawing.Point(3, 28);
            this.templateLabel.Name = "templateLabel";
            this.templateLabel.Size = new System.Drawing.Size(116, 29);
            this.templateLabel.TabIndex = 6;
            this.templateLabel.Text = "Search template:";
            this.templateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // templateTextBox
            // 
            this.templateTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.templateTextBox.Location = new System.Drawing.Point(125, 31);
            this.templateTextBox.MinimumSize = new System.Drawing.Size(70, 4);
            this.templateTextBox.Name = "templateTextBox";
            this.templateTextBox.Size = new System.Drawing.Size(104, 22);
            this.templateTextBox.TabIndex = 5;
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 542);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(400, 47);
            this.Name = "SearchForm";
            this.Text = "Search";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList iconList;
        private System.Windows.Forms.TextBox dirTextBox;
        private System.Windows.Forms.Label dirLabel;
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
    }
}

