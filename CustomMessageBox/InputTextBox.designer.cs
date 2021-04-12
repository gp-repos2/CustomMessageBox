namespace CustomDialogs
{
    partial class InputTextBox
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
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.cbVerification = new System.Windows.Forms.CheckBox();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tbText = new System.Windows.Forms.TextBox();
            this.bottomPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.cbVerification);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 77);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Padding = new System.Windows.Forms.Padding(10, 9, 10, 8);
            this.bottomPanel.Size = new System.Drawing.Size(349, 40);
            this.bottomPanel.TabIndex = 1;
            // 
            // cbVerification
            // 
            this.cbVerification.AutoSize = true;
            this.cbVerification.BackColor = System.Drawing.Color.Transparent;
            this.cbVerification.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbVerification.Location = new System.Drawing.Point(10, 9);
            this.cbVerification.Name = "cbVerification";
            this.cbVerification.Size = new System.Drawing.Size(108, 23);
            this.cbVerification.TabIndex = 0;
            this.cbVerification.Text = "VerificationText";
            this.cbVerification.UseVisualStyleBackColor = false;
            // 
            // mainPanel
            // 
            this.mainPanel.AutoSize = true;
            this.mainPanel.BackColor = System.Drawing.Color.Transparent;
            this.mainPanel.Controls.Add(this.lblTitle);
            this.mainPanel.Controls.Add(this.tbText);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(10, 16, 10, 16);
            this.mainPanel.Size = new System.Drawing.Size(349, 77);
            this.mainPanel.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Location = new System.Drawing.Point(10, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(329, 15);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Title";
            // 
            // tbText
            // 
            this.tbText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbText.Location = new System.Drawing.Point(10, 38);
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(329, 23);
            this.tbText.TabIndex = 2;
            // 
            // InputTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(349, 117);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.bottomPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(155, 155);
            this.Name = "InputTextBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Caption";
            this.Load += new System.EventHandler(this.InputTextBox_Load);
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.CheckBox cbVerification;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox tbText;
    }
}