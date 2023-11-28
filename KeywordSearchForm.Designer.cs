namespace Sysmap_udemy_test
{
    partial class KeywordSearchForm
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
            this.instructionLabel = new System.Windows.Forms.Label();
            this.keywordTextBox = new System.Windows.Forms.TextBox();
            this.searchKeywordButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // instructionLabel
            // 
            this.instructionLabel.AutoSize = true;
            this.instructionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.instructionLabel.Location = new System.Drawing.Point(13, 13);
            this.instructionLabel.Name = "instructionLabel";
            this.instructionLabel.Size = new System.Drawing.Size(271, 20);
            this.instructionLabel.TabIndex = 0;
            this.instructionLabel.Text = "Please type the keyword for search";
            // 
            // keywordTextBox
            // 
            this.keywordTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.keywordTextBox.Location = new System.Drawing.Point(291, 12);
            this.keywordTextBox.Name = "keywordTextBox";
            this.keywordTextBox.Size = new System.Drawing.Size(234, 26);
            this.keywordTextBox.TabIndex = 1;
            this.keywordTextBox.Text = "Keyword";
            // 
            // searchKeywordButton
            // 
            this.searchKeywordButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchKeywordButton.Location = new System.Drawing.Point(208, 37);
            this.searchKeywordButton.Name = "searchKeywordButton";
            this.searchKeywordButton.Size = new System.Drawing.Size(76, 34);
            this.searchKeywordButton.TabIndex = 2;
            this.searchKeywordButton.Text = "Search";
            this.searchKeywordButton.UseVisualStyleBackColor = true;
            this.searchKeywordButton.Click += new System.EventHandler(this.searchKeywordButton_Click);
            // 
            // KeywordSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 83);
            this.Controls.Add(this.searchKeywordButton);
            this.Controls.Add(this.keywordTextBox);
            this.Controls.Add(this.instructionLabel);
            this.Name = "KeywordSearchForm";
            this.Text = "Udemy keyword searcher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label instructionLabel;
        private System.Windows.Forms.TextBox keywordTextBox;
        private System.Windows.Forms.Button searchKeywordButton;
    }
}

