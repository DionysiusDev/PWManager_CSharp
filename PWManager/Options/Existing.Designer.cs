namespace PWManager.Options
{
    partial class Existing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Existing));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.homeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.saveBtn = new System.Windows.Forms.Button();
            this.existingLbl = new System.Windows.Forms.Label();
            this.pwTextBox = new System.Windows.Forms.TextBox();
            this.passwordLbl = new System.Windows.Forms.Label();
            this.adTextBox = new System.Windows.Forms.TextBox();
            this.infoLbl = new System.Windows.Forms.Label();
            this.emTextBox = new System.Windows.Forms.TextBox();
            this.emailLbl = new System.Windows.Forms.Label();
            this.wsTextBox = new System.Windows.Forms.TextBox();
            this.websiteLbl = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.menuStrip1.TabIndex = 23;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homeToolStripMenuItem,
            this.logOutToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(44, 24);
            this.toolStripMenuItem1.Text = "File";
            // 
            // homeToolStripMenuItem
            // 
            this.homeToolStripMenuItem.Name = "homeToolStripMenuItem";
            this.homeToolStripMenuItem.Size = new System.Drawing.Size(137, 26);
            this.homeToolStripMenuItem.Text = "Home";
            this.homeToolStripMenuItem.Click += new System.EventHandler(this.homeToolStripMenuItem_Click);
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(137, 26);
            this.logOutToolStripMenuItem.Text = "Log Out";
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
            // saveBtn
            // 
            this.saveBtn.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveBtn.Location = new System.Drawing.Point(295, 358);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(241, 39);
            this.saveBtn.TabIndex = 22;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // existingLbl
            // 
            this.existingLbl.AutoSize = true;
            this.existingLbl.Font = new System.Drawing.Font("Impact", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.existingLbl.Location = new System.Drawing.Point(185, 34);
            this.existingLbl.Name = "existingLbl";
            this.existingLbl.Size = new System.Drawing.Size(435, 53);
            this.existingLbl.TabIndex = 20;
            this.existingLbl.Text = "Enter Existing Password";
            // 
            // pwTextBox
            // 
            this.pwTextBox.Location = new System.Drawing.Point(295, 319);
            this.pwTextBox.Name = "pwTextBox";
            this.pwTextBox.Size = new System.Drawing.Size(241, 22);
            this.pwTextBox.TabIndex = 19;
            // 
            // passwordLbl
            // 
            this.passwordLbl.AutoSize = true;
            this.passwordLbl.Font = new System.Drawing.Font("Agency FB", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordLbl.Location = new System.Drawing.Point(289, 283);
            this.passwordLbl.Name = "passwordLbl";
            this.passwordLbl.Size = new System.Drawing.Size(99, 33);
            this.passwordLbl.TabIndex = 18;
            this.passwordLbl.Text = "Password";
            // 
            // adTextBox
            // 
            this.adTextBox.Location = new System.Drawing.Point(295, 258);
            this.adTextBox.Name = "adTextBox";
            this.adTextBox.Size = new System.Drawing.Size(241, 22);
            this.adTextBox.TabIndex = 17;
            // 
            // infoLbl
            // 
            this.infoLbl.AutoSize = true;
            this.infoLbl.Font = new System.Drawing.Font("Agency FB", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLbl.Location = new System.Drawing.Point(289, 222);
            this.infoLbl.Name = "infoLbl";
            this.infoLbl.Size = new System.Drawing.Size(189, 33);
            this.infoLbl.TabIndex = 16;
            this.infoLbl.Text = "Additional Information";
            // 
            // emTextBox
            // 
            this.emTextBox.Location = new System.Drawing.Point(295, 197);
            this.emTextBox.Name = "emTextBox";
            this.emTextBox.Size = new System.Drawing.Size(241, 22);
            this.emTextBox.TabIndex = 15;
            // 
            // emailLbl
            // 
            this.emailLbl.AutoSize = true;
            this.emailLbl.Font = new System.Drawing.Font("Agency FB", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailLbl.Location = new System.Drawing.Point(289, 161);
            this.emailLbl.Name = "emailLbl";
            this.emailLbl.Size = new System.Drawing.Size(58, 33);
            this.emailLbl.TabIndex = 14;
            this.emailLbl.Text = "Email";
            // 
            // wsTextBox
            // 
            this.wsTextBox.Location = new System.Drawing.Point(295, 136);
            this.wsTextBox.Name = "wsTextBox";
            this.wsTextBox.Size = new System.Drawing.Size(241, 22);
            this.wsTextBox.TabIndex = 13;
            // 
            // websiteLbl
            // 
            this.websiteLbl.AutoSize = true;
            this.websiteLbl.Font = new System.Drawing.Font("Agency FB", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.websiteLbl.Location = new System.Drawing.Point(289, 100);
            this.websiteLbl.Name = "websiteLbl";
            this.websiteLbl.Size = new System.Drawing.Size(78, 33);
            this.websiteLbl.TabIndex = 12;
            this.websiteLbl.Text = "Website";
            // 
            // Existing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.existingLbl);
            this.Controls.Add(this.pwTextBox);
            this.Controls.Add(this.passwordLbl);
            this.Controls.Add(this.adTextBox);
            this.Controls.Add(this.infoLbl);
            this.Controls.Add(this.emTextBox);
            this.Controls.Add(this.emailLbl);
            this.Controls.Add(this.wsTextBox);
            this.Controls.Add(this.websiteLbl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Existing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter Existing Password";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Existing_FormClosing);
            this.Load += new System.EventHandler(this.Existing_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem homeToolStripMenuItem;
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
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Label existingLbl;
        private System.Windows.Forms.TextBox pwTextBox;
        private System.Windows.Forms.Label passwordLbl;
        private System.Windows.Forms.TextBox adTextBox;
        private System.Windows.Forms.Label infoLbl;
        private System.Windows.Forms.TextBox emTextBox;
        private System.Windows.Forms.Label emailLbl;
        private System.Windows.Forms.TextBox wsTextBox;
        private System.Windows.Forms.Label websiteLbl;
    }
}