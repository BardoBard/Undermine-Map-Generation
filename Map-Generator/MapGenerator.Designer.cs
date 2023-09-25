namespace Map_Generator
{
    partial class MapGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapGenerator));
            this.saveNumber = new System.Windows.Forms.NumericUpDown();
            this.saveNumberLabel = new System.Windows.Forms.Label();
            this.FindMapButton = new System.Windows.Forms.Button();
            this.WhipSeed = new System.Windows.Forms.Label();
            this.IssueButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.saveNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // saveNumber
            // 
            this.saveNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveNumber.Location = new System.Drawing.Point(1065, 642);
            this.saveNumber.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            this.saveNumber.Name = "saveNumber";
            this.saveNumber.Size = new System.Drawing.Size(120, 20);
            this.saveNumber.TabIndex = 0;
            // 
            // saveNumberLabel
            // 
            this.saveNumberLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveNumberLabel.AutoSize = true;
            this.saveNumberLabel.Location = new System.Drawing.Point(1088, 626);
            this.saveNumberLabel.Name = "saveNumberLabel";
            this.saveNumberLabel.Size = new System.Drawing.Size(72, 13);
            this.saveNumberLabel.TabIndex = 1;
            this.saveNumberLabel.Text = "Save Number";
            // 
            // FindMapButton
            // 
            this.FindMapButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.FindMapButton.Location = new System.Drawing.Point(522, 639);
            this.FindMapButton.Name = "FindMapButton";
            this.FindMapButton.Size = new System.Drawing.Size(75, 23);
            this.FindMapButton.TabIndex = 2;
            this.FindMapButton.Text = "Find Map";
            this.FindMapButton.UseVisualStyleBackColor = true;
            this.FindMapButton.Click += new System.EventHandler(this.findMapButton_Click);
            // 
            // WhipSeed
            // 
            this.WhipSeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WhipSeed.AutoSize = true;
            this.WhipSeed.Location = new System.Drawing.Point(1062, 9);
            this.WhipSeed.Name = "WhipSeed";
            this.WhipSeed.Size = new System.Drawing.Size(70, 13);
            this.WhipSeed.TabIndex = 3;
            this.WhipSeed.Text = "WHIP Seed: ";
            // 
            // IssueButton
            // 
            this.IssueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.IssueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IssueButton.Location = new System.Drawing.Point(3, 638);
            this.IssueButton.Name = "IssueButton";
            this.IssueButton.Size = new System.Drawing.Size(75, 23);
            this.IssueButton.TabIndex = 5;
            this.IssueButton.Text = "Issues?";
            this.IssueButton.UseVisualStyleBackColor = true;
            this.IssueButton.Click += new System.EventHandler(this.IssueButton_Click);
            // 
            // CreateTestButton
            // 
#if DEBUG

            this.CreateTestButton = new System.Windows.Forms.Button();
            this.CreateTestButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CreateTestButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.CreateTestButton.Location = new System.Drawing.Point(710, 638);
            this.CreateTestButton.Name = "CreateTestButton";
            this.CreateTestButton.Size = new System.Drawing.Size(75, 23);
            this.CreateTestButton.TabIndex = 6;
            this.CreateTestButton.Text = "Create Test";
            this.CreateTestButton.UseVisualStyleBackColor = true;
            this.CreateTestButton.Click += new System.EventHandler(this.CreateTestButton_Click);
            this.Controls.Add(this.CreateTestButton);
#endif
            // 
            // MapGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.IssueButton);
            this.Controls.Add(this.WhipSeed);
            this.Controls.Add(this.FindMapButton);
            this.Controls.Add(this.saveNumberLabel);
            this.Controls.Add(this.saveNumber);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MapGenerator";
            this.Text = "MapGenerator";
            ((System.ComponentModel.ISupportInitialize)(this.saveNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

#if DEBUG
        private System.Windows.Forms.Button CreateTestButton;
#endif
        #endregion

        private System.Windows.Forms.NumericUpDown saveNumber;
        private System.Windows.Forms.Label saveNumberLabel;
        private System.Windows.Forms.Button FindMapButton;
        private System.Windows.Forms.Label WhipSeed;
        private System.Windows.Forms.Button IssueButton;
    }
}