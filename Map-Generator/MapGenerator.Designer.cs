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
            this.saveNumber = new System.Windows.Forms.NumericUpDown();
            this.saveNumberLabel = new System.Windows.Forms.Label();
            this.findMapButton = new System.Windows.Forms.Button();
            this.WhipSeed = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.saveNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // saveNumber
            // 
            this.saveNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveNumber.Location = new System.Drawing.Point(1065, 642);
            this.saveNumber.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
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
            // findMapButton
            // 
            this.findMapButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.findMapButton.Location = new System.Drawing.Point(522, 639);
            this.findMapButton.Name = "findMapButton";
            this.findMapButton.Size = new System.Drawing.Size(75, 23);
            this.findMapButton.TabIndex = 2;
            this.findMapButton.Text = "Find Map";
            this.findMapButton.UseVisualStyleBackColor = true;
            this.findMapButton.Click += new System.EventHandler(this.findMapButton_Click);
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
            // MapGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.WhipSeed);
            this.Controls.Add(this.findMapButton);
            this.Controls.Add(this.saveNumberLabel);
            this.Controls.Add(this.saveNumber);
            this.Name = "MapGenerator";
            this.Text = "MapGenerator";
            ((System.ComponentModel.ISupportInitialize)(this.saveNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown saveNumber;
        private System.Windows.Forms.Label saveNumberLabel;
        private System.Windows.Forms.Button findMapButton;
        private System.Windows.Forms.Label WhipSeed;
    }
}