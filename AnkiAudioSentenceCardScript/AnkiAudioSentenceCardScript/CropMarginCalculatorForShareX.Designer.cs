namespace AnkiAudioSentenceCardScript
{
    partial class CropMarginCalculatorForShareX
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
            this.txtPastedText = new System.Windows.Forms.TextBox();
            this.txtMonitorWidth = new System.Windows.Forms.TextBox();
            this.txtMonitorHeight = new System.Windows.Forms.TextBox();
            this.btnCalculateValues = new System.Windows.Forms.Button();
            this.txtValues = new System.Windows.Forms.TextBox();
            this.lblPrompt = new System.Windows.Forms.Label();
            this.lblMonitorWidth = new System.Windows.Forms.Label();
            this.lblMonitorHeight = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPastedText
            // 
            this.txtPastedText.Location = new System.Drawing.Point(12, 51);
            this.txtPastedText.Name = "txtPastedText";
            this.txtPastedText.Size = new System.Drawing.Size(324, 20);
            this.txtPastedText.TabIndex = 0;
            // 
            // txtMonitorWidth
            // 
            this.txtMonitorWidth.Location = new System.Drawing.Point(161, 91);
            this.txtMonitorWidth.Name = "txtMonitorWidth";
            this.txtMonitorWidth.Size = new System.Drawing.Size(100, 20);
            this.txtMonitorWidth.TabIndex = 1;
            // 
            // txtMonitorHeight
            // 
            this.txtMonitorHeight.Location = new System.Drawing.Point(161, 117);
            this.txtMonitorHeight.Name = "txtMonitorHeight";
            this.txtMonitorHeight.Size = new System.Drawing.Size(100, 20);
            this.txtMonitorHeight.TabIndex = 2;
            // 
            // btnCalculateValues
            // 
            this.btnCalculateValues.Location = new System.Drawing.Point(73, 148);
            this.btnCalculateValues.Name = "btnCalculateValues";
            this.btnCalculateValues.Size = new System.Drawing.Size(75, 23);
            this.btnCalculateValues.TabIndex = 3;
            this.btnCalculateValues.Text = "Enter";
            this.btnCalculateValues.UseVisualStyleBackColor = true;
            this.btnCalculateValues.Click += new System.EventHandler(this.btnCalculateValues_Click);
            // 
            // txtValues
            // 
            this.txtValues.Location = new System.Drawing.Point(73, 187);
            this.txtValues.Name = "txtValues";
            this.txtValues.ReadOnly = true;
            this.txtValues.Size = new System.Drawing.Size(193, 20);
            this.txtValues.TabIndex = 5;
            // 
            // lblPrompt
            // 
            this.lblPrompt.AutoSize = true;
            this.lblPrompt.Location = new System.Drawing.Point(12, 9);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(308, 39);
            this.lblPrompt.TabIndex = 0;
            this.lblPrompt.Text = "In ShareX, go to Tools > Ruler. Select the desired region,\r\ncopy the text with Co" +
    "ntrol C, press Escape and paste that text in\r\nthe text box below.";
            // 
            // lblMonitorWidth
            // 
            this.lblMonitorWidth.AutoSize = true;
            this.lblMonitorWidth.Location = new System.Drawing.Point(86, 94);
            this.lblMonitorWidth.Name = "lblMonitorWidth";
            this.lblMonitorWidth.Size = new System.Drawing.Size(73, 13);
            this.lblMonitorWidth.TabIndex = 2;
            this.lblMonitorWidth.Text = "Monitor width:";
            // 
            // lblMonitorHeight
            // 
            this.lblMonitorHeight.AutoSize = true;
            this.lblMonitorHeight.Location = new System.Drawing.Point(82, 120);
            this.lblMonitorHeight.Name = "lblMonitorHeight";
            this.lblMonitorHeight.Size = new System.Drawing.Size(77, 13);
            this.lblMonitorHeight.TabIndex = 4;
            this.lblMonitorHeight.Text = "Monitor height:";
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(174, 148);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // CropMarginCalculatorForShareX
            // 
            this.AcceptButton = this.btnCalculateValues;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(348, 228);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblMonitorHeight);
            this.Controls.Add(this.lblMonitorWidth);
            this.Controls.Add(this.lblPrompt);
            this.Controls.Add(this.txtValues);
            this.Controls.Add(this.btnCalculateValues);
            this.Controls.Add(this.txtMonitorHeight);
            this.Controls.Add(this.txtMonitorWidth);
            this.Controls.Add(this.txtPastedText);
            this.Name = "CropMarginCalculatorForShareX";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CropMarginCalculatorForShareX";
            this.Load += new System.EventHandler(this.CropMarginCalculatorForShareX_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPastedText;
        private System.Windows.Forms.TextBox txtMonitorWidth;
        private System.Windows.Forms.TextBox txtMonitorHeight;
        private System.Windows.Forms.Button btnCalculateValues;
        private System.Windows.Forms.TextBox txtValues;
        private System.Windows.Forms.Label lblPrompt;
        private System.Windows.Forms.Label lblMonitorWidth;
        private System.Windows.Forms.Label lblMonitorHeight;
        private System.Windows.Forms.Button btnExit;
    }
}