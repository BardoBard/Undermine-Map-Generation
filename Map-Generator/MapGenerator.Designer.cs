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
            this.SaveNumber = new System.Windows.Forms.NumericUpDown();
            this.SaveNumberLabel = new System.Windows.Forms.Label();
            this.FindMapButton = new System.Windows.Forms.Button();
            this.FloorNameLabel = new System.Windows.Forms.Label();
            this.WhipSeed = new System.Windows.Forms.Label();
            this.IssueButton = new System.Windows.Forms.Button();
#if DEBUG
            this.CreateTestButton = new System.Windows.Forms.Button();
#endif
            this.SimpleAStarRadio = new System.Windows.Forms.RadioButton();
            this.AdvancedAStarRadio = new System.Windows.Forms.RadioButton();
            this.FindFastMapButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SaveNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveNumber
            // 
            this.SaveNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveNumber.Location = new System.Drawing.Point(1065, 642);
            this.SaveNumber.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.SaveNumber.Name = "SaveNumber";
            this.SaveNumber.Size = new System.Drawing.Size(120, 20);
            this.SaveNumber.TabIndex = 0;
            this.SaveNumber.ValueChanged += new System.EventHandler(this.SaveNumber_ValueChanged);
            // 
            // SaveNumberLabel
            // 
            this.SaveNumberLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveNumberLabel.AutoSize = true;
            this.SaveNumberLabel.Location = new System.Drawing.Point(1088, 626);
            this.SaveNumberLabel.Name = "SaveNumberLabel";
            this.SaveNumberLabel.Size = new System.Drawing.Size(72, 13);
            this.SaveNumberLabel.TabIndex = 1;
            this.SaveNumberLabel.Text = "Save Number";
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
            this.FindMapButton.Click += new System.EventHandler(this.FindMapButton_Click);
            // 
            // FloorNameLabel
            // 
            this.FloorNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FloorNameLabel.AutoSize = true;
            this.FloorNameLabel.Location = new System.Drawing.Point(592, 11);
            this.FloorNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.FloorNameLabel.Name = "FloorNameLabel";
            this.FloorNameLabel.Size = new System.Drawing.Size(0, 13);
            this.FloorNameLabel.TabIndex = 7;
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
#if DEBUG
            // 
            // CreateTestButton
            // 
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
            // SimpleAStarRadio
            // 
            this.SimpleAStarRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SimpleAStarRadio.AutoSize = true;
            this.SimpleAStarRadio.Checked = true;
            this.SimpleAStarRadio.Location = new System.Drawing.Point(1055, 81);
            this.SimpleAStarRadio.Name = "SimpleAStarRadio";
            this.SimpleAStarRadio.Size = new System.Drawing.Size(112, 17);
            this.SimpleAStarRadio.TabIndex = 8;
            this.SimpleAStarRadio.TabStop = true;
            this.SimpleAStarRadio.Text = "Simple Pathfinding";
            this.SimpleAStarRadio.UseVisualStyleBackColor = true;
            this.SimpleAStarRadio.Click += new System.EventHandler(this.AStarRadio_Click);
            // 
            // AdvancedAStarRadio
            // 
            this.AdvancedAStarRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AdvancedAStarRadio.AutoSize = true;
            this.AdvancedAStarRadio.Location = new System.Drawing.Point(1055, 104);
            this.AdvancedAStarRadio.Name = "AdvancedAStarRadio";
            this.AdvancedAStarRadio.Size = new System.Drawing.Size(130, 17);
            this.AdvancedAStarRadio.TabIndex = 9;
            this.AdvancedAStarRadio.Text = "Advanced Pathfinding";
            this.AdvancedAStarRadio.UseVisualStyleBackColor = true;
            this.AdvancedAStarRadio.Click += new System.EventHandler(this.AStarRadio_Click);
            // 
            // FindFastMapButton
            // 
            this.FindFastMapButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FindFastMapButton.Location = new System.Drawing.Point(1065, 127);
            this.FindFastMapButton.Name = "FindFastMapButton";
            this.FindFastMapButton.Size = new System.Drawing.Size(107, 23);
            this.FindFastMapButton.TabIndex = 10;
            this.FindFastMapButton.Text = "Find Fast Map";
            this.FindFastMapButton.UseVisualStyleBackColor = true;
            this.FindFastMapButton.Click += new System.EventHandler(this.FindFastMapButton_Click);
            // 
            // MapGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.FindFastMapButton);
            this.Controls.Add(this.AdvancedAStarRadio);
            this.Controls.Add(this.SimpleAStarRadio);
            this.Controls.Add(this.IssueButton);
            this.Controls.Add(this.WhipSeed);
            this.Controls.Add(this.FindMapButton);
            this.Controls.Add(this.FloorNameLabel);
            this.Controls.Add(this.SaveNumberLabel);
            this.Controls.Add(this.SaveNumber);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MapGenerator";
            this.Text = "MapGenerator";
            ((System.ComponentModel.ISupportInitialize)(this.SaveNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

#if DEBUG
        private System.Windows.Forms.Button CreateTestButton;
#endif
        #endregion

        private System.Windows.Forms.NumericUpDown SaveNumber;
        private System.Windows.Forms.Label SaveNumberLabel;
        private System.Windows.Forms.Label FloorNameLabel;
        private System.Windows.Forms.Button FindMapButton;
        private System.Windows.Forms.Label WhipSeed;
        private System.Windows.Forms.Button IssueButton;
        private System.Windows.Forms.RadioButton SimpleAStarRadio;
        private System.Windows.Forms.RadioButton AdvancedAStarRadio;
        private System.Windows.Forms.Button FindFastMapButton;
    }
}