namespace PWManager.Help
{
    partial class HelpAbout
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
            this.headerLbl = new System.Windows.Forms.Label();
            this.versionLbl = new System.Windows.Forms.Label();
            this.copyrLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.githubLbl = new System.Windows.Forms.LinkLabel();
            this.closeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // headerLbl
            // 
            this.headerLbl.AutoSize = true;
            this.headerLbl.BackColor = System.Drawing.Color.White;
            this.headerLbl.Font = new System.Drawing.Font("Impact", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerLbl.ForeColor = System.Drawing.Color.Black;
            this.headerLbl.Location = new System.Drawing.Point(0, 0);
            this.headerLbl.MaximumSize = new System.Drawing.Size(800, 250);
            this.headerLbl.MinimumSize = new System.Drawing.Size(800, 250);
            this.headerLbl.Name = "headerLbl";
            this.headerLbl.Padding = new System.Windows.Forms.Padding(50, 50, 0, 0);
            this.headerLbl.Size = new System.Drawing.Size(800, 250);
            this.headerLbl.TabIndex = 0;
            this.headerLbl.Text = "Password Manager";
            // 
            // versionLbl
            // 
            this.versionLbl.AutoSize = true;
            this.versionLbl.BackColor = System.Drawing.Color.White;
            this.versionLbl.Location = new System.Drawing.Point(59, 107);
            this.versionLbl.Name = "versionLbl";
            this.versionLbl.Size = new System.Drawing.Size(92, 17);
            this.versionLbl.TabIndex = 1;
            this.versionLbl.Text = "Version 1.0.0";
            // 
            // copyrLbl
            // 
            this.copyrLbl.AutoSize = true;
            this.copyrLbl.BackColor = System.Drawing.Color.White;
            this.copyrLbl.Location = new System.Drawing.Point(59, 134);
            this.copyrLbl.Name = "copyrLbl";
            this.copyrLbl.Size = new System.Drawing.Size(143, 17);
            this.copyrLbl.TabIndex = 2;
            this.copyrLbl.Text = "(C) 2021 Dion Helsby";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(249, 424);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Developed using Microsoft .NET Framework 4.7.1";
            // 
            // githubLbl
            // 
            this.githubLbl.AutoSize = true;
            this.githubLbl.BackColor = System.Drawing.Color.White;
            this.githubLbl.Location = new System.Drawing.Point(59, 160);
            this.githubLbl.Name = "githubLbl";
            this.githubLbl.Size = new System.Drawing.Size(391, 17);
            this.githubLbl.TabIndex = 4;
            this.githubLbl.TabStop = true;
            this.githubLbl.Text = "https://github.com/DionysiusDev/PWManager_CSharp/issues";
            this.githubLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.githubLbl_LinkClicked);
            // 
            // closeBtn
            // 
            this.closeBtn.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeBtn.Location = new System.Drawing.Point(704, 401);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(84, 37);
            this.closeBtn.TabIndex = 5;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // HelpAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.githubLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.copyrLbl);
            this.Controls.Add(this.versionLbl);
            this.Controls.Add(this.headerLbl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HelpAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About Password Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label headerLbl;
        private System.Windows.Forms.Label versionLbl;
        private System.Windows.Forms.Label copyrLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel githubLbl;
        private System.Windows.Forms.Button closeBtn;
    }
}