namespace Keyshift.Forms.Windows
{
    partial class LengthChangeFrm
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
            this.nudFrame = new System.Windows.Forms.NumericUpDown();
            this.lbFps = new System.Windows.Forms.Label();
            this.lbTimecode = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbNoFractional = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // nudFrame
            // 
            this.nudFrame.Location = new System.Drawing.Point(49, 36);
            this.nudFrame.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudFrame.Name = "nudFrame";
            this.nudFrame.Size = new System.Drawing.Size(91, 20);
            this.nudFrame.TabIndex = 0;
            this.nudFrame.ValueChanged += new System.EventHandler(this.nudFrame_ValueChanged);
            // 
            // lbFps
            // 
            this.lbFps.AutoSize = true;
            this.lbFps.Location = new System.Drawing.Point(146, 40);
            this.lbFps.Name = "lbFps";
            this.lbFps.Size = new System.Drawing.Size(83, 13);
            this.lbFps.TabIndex = 1;
            this.lbFps.Text = "frames @ XXfps";
            // 
            // lbTimecode
            // 
            this.lbTimecode.AutoSize = true;
            this.lbTimecode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTimecode.Location = new System.Drawing.Point(35, 63);
            this.lbTimecode.Name = "lbTimecode";
            this.lbTimecode.Size = new System.Drawing.Size(209, 20);
            this.lbTimecode.TabIndex = 2;
            this.lbTimecode.Text = "Equivalent to: XX:XX:XX.XX";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(197, 132);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(116, 132);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lbNoFractional
            // 
            this.lbNoFractional.AutoSize = true;
            this.lbNoFractional.Location = new System.Drawing.Point(55, 14);
            this.lbNoFractional.Name = "lbNoFractional";
            this.lbNoFractional.Size = new System.Drawing.Size(164, 13);
            this.lbNoFractional.TabIndex = 4;
            this.lbNoFractional.Text = "(Fractional frames not supported.)";
            // 
            // LengthChangeFrm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(284, 167);
            this.ControlBox = false;
            this.Controls.Add(this.lbNoFractional);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lbTimecode);
            this.Controls.Add(this.lbFps);
            this.Controls.Add(this.nudFrame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LengthChangeFrm";
            this.Text = "Change Timeline Length...";
            ((System.ComponentModel.ISupportInitialize)(this.nudFrame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudFrame;
        private System.Windows.Forms.Label lbFps;
        private System.Windows.Forms.Label lbTimecode;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbNoFractional;
    }
}