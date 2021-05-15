namespace PWManager.Home
{
    partial class Home
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.PWManagerLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewPasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enterExistingPasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewAllPasswordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewPasswordToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.enterExistingPasswordToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutPasswordManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PWManagerLabel
            // 
            this.PWManagerLabel.AutoSize = true;
            this.PWManagerLabel.Font = new System.Drawing.Font("Impact", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PWManagerLabel.Location = new System.Drawing.Point(215, 175);
            this.PWManagerLabel.Name = "PWManagerLabel";
            this.PWManagerLabel.Size = new System.Drawing.Size(358, 53);
            this.PWManagerLabel.TabIndex = 4;
            this.PWManagerLabel.Text = "Password Manager";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logOutToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(44, 24);
            this.toolStripMenuItem1.Text = "File";
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(135, 26);
            this.logOutToolStripMenuItem.Text = "Log out";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewPasswordToolStripMenuItem,
            this.enterExistingPasswordToolStripMenuItem,
            this.viewAllPasswordsToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(73, 24);
            this.toolStripMenuItem2.Text = "Options";
            // 
            // createNewPasswordToolStripMenuItem
            // 
            this.createNewPasswordToolStripMenuItem.Name = "createNewPasswordToolStripMenuItem";
            this.createNewPasswordToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.createNewPasswordToolStripMenuItem.Text = "Create New Password";
            this.createNewPasswordToolStripMenuItem.Click += new System.EventHandler(this.createNewPasswordToolStripMenuItem_Click);
            // 
            // enterExistingPasswordToolStripMenuItem
            // 
            this.enterExistingPasswordToolStripMenuItem.Name = "enterExistingPasswordToolStripMenuItem";
            this.enterExistingPasswordToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.enterExistingPasswordToolStripMenuItem.Text = "Enter Existing Password";
            this.enterExistingPasswordToolStripMenuItem.Click += new System.EventHandler(this.enterExistingPasswordToolStripMenuItem_Click);
            // 
            // viewAllPasswordsToolStripMenuItem
            // 
            this.viewAllPasswordsToolStripMenuItem.Name = "viewAllPasswordsToolStripMenuItem";
            this.viewAllPasswordsToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.viewAllPasswordsToolStripMenuItem.Text = "View All Websites";
            this.viewAllPasswordsToolStripMenuItem.Click += new System.EventHandler(this.viewAllPasswordsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.howToToolStripMenuItem,
            this.aboutPasswordManagerToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // howToToolStripMenuItem
            // 
            this.howToToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewPasswordToolStripMenuItem1,
            this.enterExistingPasswordToolStripMenuItem1});
            this.howToToolStripMenuItem.Name = "howToToolStripMenuItem";
            this.howToToolStripMenuItem.Size = new System.Drawing.Size(253, 26);
            this.howToToolStripMenuItem.Text = "How To...";
            // 
            // createNewPasswordToolStripMenuItem1
            // 
            this.createNewPasswordToolStripMenuItem1.Name = "createNewPasswordToolStripMenuItem1";
            this.createNewPasswordToolStripMenuItem1.Size = new System.Drawing.Size(238, 26);
            this.createNewPasswordToolStripMenuItem1.Text = "Create New Password";
            this.createNewPasswordToolStripMenuItem1.Click += new System.EventHandler(this.createNewPasswordToolStripMenuItem1_Click);
            // 
            // enterExistingPasswordToolStripMenuItem1
            // 
            this.enterExistingPasswordToolStripMenuItem1.Name = "enterExistingPasswordToolStripMenuItem1";
            this.enterExistingPasswordToolStripMenuItem1.Size = new System.Drawing.Size(238, 26);
            this.enterExistingPasswordToolStripMenuItem1.Text = "Enter Existing Password";
            this.enterExistingPasswordToolStripMenuItem1.Click += new System.EventHandler(this.enterExistingPasswordToolStripMenuItem1_Click);
            // 
            // aboutPasswordManagerToolStripMenuItem
            // 
            this.aboutPasswordManagerToolStripMenuItem.Name = "aboutPasswordManagerToolStripMenuItem";
            this.aboutPasswordManagerToolStripMenuItem.Size = new System.Drawing.Size(253, 26);
            this.aboutPasswordManagerToolStripMenuItem.Text = "About Password Manager";
            this.aboutPasswordManagerToolStripMenuItem.Click += new System.EventHandler(this.aboutPasswordManagerToolStripMenuItem_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PWManagerLabel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Home_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PWManagerLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem createNewPasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enterExistingPasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewAllPasswordsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem howToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewPasswordToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem enterExistingPasswordToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutPasswordManagerToolStripMenuItem;
    }
}