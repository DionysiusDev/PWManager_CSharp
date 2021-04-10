namespace PWManager.Details
{
    partial class Details
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
            this.backBtn = new System.Windows.Forms.Button();
            this.detailsLbl = new System.Windows.Forms.Label();
            this.pwTextBox = new System.Windows.Forms.TextBox();
            this.passwordLbl = new System.Windows.Forms.Label();
            this.adTextBox = new System.Windows.Forms.TextBox();
            this.infoLbl = new System.Windows.Forms.Label();
            this.emTextBox = new System.Windows.Forms.TextBox();
            this.emailLbl = new System.Windows.Forms.Label();
            this.wsTextBox = new System.Windows.Forms.TextBox();
            this.websiteLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // backBtn
            // 
            this.backBtn.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backBtn.Location = new System.Drawing.Point(293, 368);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(241, 39);
            this.backBtn.TabIndex = 32;
            this.backBtn.Text = "Back";
            this.backBtn.UseVisualStyleBackColor = true;
            // 
            // detailsLbl
            // 
            this.detailsLbl.AutoSize = true;
            this.detailsLbl.Font = new System.Drawing.Font("Impact", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailsLbl.Location = new System.Drawing.Point(256, 45);
            this.detailsLbl.Name = "detailsLbl";
            this.detailsLbl.Size = new System.Drawing.Size(299, 53);
            this.detailsLbl.TabIndex = 31;
            this.detailsLbl.Text = "Website Details";
            // 
            // pwTextBox
            // 
            this.pwTextBox.Location = new System.Drawing.Point(293, 329);
            this.pwTextBox.Name = "pwTextBox";
            this.pwTextBox.Size = new System.Drawing.Size(241, 22);
            this.pwTextBox.TabIndex = 30;
            // 
            // passwordLbl
            // 
            this.passwordLbl.AutoSize = true;
            this.passwordLbl.Font = new System.Drawing.Font("Agency FB", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordLbl.Location = new System.Drawing.Point(287, 293);
            this.passwordLbl.Name = "passwordLbl";
            this.passwordLbl.Size = new System.Drawing.Size(99, 33);
            this.passwordLbl.TabIndex = 29;
            this.passwordLbl.Text = "Password";
            // 
            // adTextBox
            // 
            this.adTextBox.Location = new System.Drawing.Point(293, 268);
            this.adTextBox.Name = "adTextBox";
            this.adTextBox.Size = new System.Drawing.Size(241, 22);
            this.adTextBox.TabIndex = 28;
            // 
            // infoLbl
            // 
            this.infoLbl.AutoSize = true;
            this.infoLbl.Font = new System.Drawing.Font("Agency FB", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLbl.Location = new System.Drawing.Point(287, 232);
            this.infoLbl.Name = "infoLbl";
            this.infoLbl.Size = new System.Drawing.Size(189, 33);
            this.infoLbl.TabIndex = 27;
            this.infoLbl.Text = "Additional Information";
            // 
            // emTextBox
            // 
            this.emTextBox.Location = new System.Drawing.Point(293, 207);
            this.emTextBox.Name = "emTextBox";
            this.emTextBox.Size = new System.Drawing.Size(241, 22);
            this.emTextBox.TabIndex = 26;
            // 
            // emailLbl
            // 
            this.emailLbl.AutoSize = true;
            this.emailLbl.Font = new System.Drawing.Font("Agency FB", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailLbl.Location = new System.Drawing.Point(287, 171);
            this.emailLbl.Name = "emailLbl";
            this.emailLbl.Size = new System.Drawing.Size(58, 33);
            this.emailLbl.TabIndex = 25;
            this.emailLbl.Text = "Email";
            // 
            // wsTextBox
            // 
            this.wsTextBox.Location = new System.Drawing.Point(293, 146);
            this.wsTextBox.Name = "wsTextBox";
            this.wsTextBox.Size = new System.Drawing.Size(241, 22);
            this.wsTextBox.TabIndex = 24;
            // 
            // websiteLbl
            // 
            this.websiteLbl.AutoSize = true;
            this.websiteLbl.Font = new System.Drawing.Font("Agency FB", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.websiteLbl.Location = new System.Drawing.Point(287, 110);
            this.websiteLbl.Name = "websiteLbl";
            this.websiteLbl.Size = new System.Drawing.Size(78, 33);
            this.websiteLbl.TabIndex = 23;
            this.websiteLbl.Text = "Website";
            // 
            // Details
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.detailsLbl);
            this.Controls.Add(this.pwTextBox);
            this.Controls.Add(this.passwordLbl);
            this.Controls.Add(this.adTextBox);
            this.Controls.Add(this.infoLbl);
            this.Controls.Add(this.emTextBox);
            this.Controls.Add(this.emailLbl);
            this.Controls.Add(this.wsTextBox);
            this.Controls.Add(this.websiteLbl);
            this.Name = "Details";
            this.Text = "Details";
            this.Load += new System.EventHandler(this.Details_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.Label detailsLbl;
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