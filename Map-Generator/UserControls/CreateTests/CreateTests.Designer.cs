using System.ComponentModel;

namespace Map_Generator.UserControls.CreateTests;

partial class CreateTests
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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

    #region Component Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.CreateTestButton = new System.Windows.Forms.Button();
        this.SaveNumberLabel = new System.Windows.Forms.Label();
        this.SaveNumberNumeric = new System.Windows.Forms.NumericUpDown();
        ((System.ComponentModel.ISupportInitialize)(this.SaveNumberNumeric)).BeginInit();
        this.SuspendLayout();
        // 
        // CreateTestButton
        // 
        this.CreateTestButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.CreateTestButton.Location = new System.Drawing.Point(189, 186);
        this.CreateTestButton.Name = "CreateTestButton";
        this.CreateTestButton.Size = new System.Drawing.Size(75, 23);
        this.CreateTestButton.TabIndex = 0;
        this.CreateTestButton.Text = "Create Test";
        this.CreateTestButton.UseVisualStyleBackColor = true;
        this.CreateTestButton.Click += new System.EventHandler(this.CreateTestButton_Click);
        // 
        // SaveNumberLabel
        // 
        this.SaveNumberLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.SaveNumberLabel.Location = new System.Drawing.Point(354, 171);
        this.SaveNumberLabel.Name = "SaveNumberLabel";
        this.SaveNumberLabel.Size = new System.Drawing.Size(73, 15);
        this.SaveNumberLabel.TabIndex = 2;
        this.SaveNumberLabel.Text = "Save Number";
        // 
        // SaveNumberNumeric
        // 
        this.SaveNumberNumeric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.SaveNumberNumeric.AutoSize = true;
        this.SaveNumberNumeric.Location = new System.Drawing.Point(332, 189);
        this.SaveNumberNumeric.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
        this.SaveNumberNumeric.Name = "SaveNumberNumeric";
        this.SaveNumberNumeric.Size = new System.Drawing.Size(120, 20);
        this.SaveNumberNumeric.TabIndex = 3;
        // 
        // CreateTests
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.SaveNumberNumeric);
        this.Controls.Add(this.SaveNumberLabel);
        this.Controls.Add(this.CreateTestButton);
        this.Name = "CreateTests";
        this.Size = new System.Drawing.Size(452, 209);
        ((System.ComponentModel.ISupportInitialize)(this.SaveNumberNumeric)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.NumericUpDown SaveNumberNumeric;

    private System.Windows.Forms.Label SaveNumberLabel;

    private System.Windows.Forms.Button CreateTestButton;

    #endregion
}