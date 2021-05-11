using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PWManager.Help
{
    public partial class HelpExisting : Form
    {
        public HelpExisting()
        {
            InitializeComponent();
        }

        #region Control Events
        /// <summary>
        /// Handles user interaction with the save button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveBtn_Click(object sender, EventArgs e)
        {
            ValidateData();
        }

        #region File Menu
        /// <summary>
        /// Handles user interaction with the file menu / home button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
            Home.Home frm = new Home.Home();
            frm.ShowDialog();
        }
        /// <summary>
        /// Handles user interaction with the file menu / logout button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
            Login frm = new Login();
            frm.ShowDialog();
        }
        #endregion

        #region Options Menu
        /// <summary>
        /// Handles user interaction with the options menu / create new button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createNewPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
            Options.Create frm = new Options.Create();
            frm.ShowDialog();
        }

        /// <summary>
        /// Handles user interaction with the options menu / enter existing button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterExistingPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
            Options.Existing frm = new Options.Existing();
            frm.ShowDialog();
        }

        /// <summary>
        /// Handles user interaction with the options menu / view all button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewAllPasswordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
            Options.ViewAll frm = new Options.ViewAll();
            frm.ShowDialog();
        }
        #endregion

        #region Help Menu
        /// <summary>
        /// Handles user interaction with the help menu / create new button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createNewPasswordToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Dispose();
            Help.HelpCreate frm = new Help.HelpCreate();
            frm.ShowDialog();
        }
        /// <summary>
        /// Handles user interaction with the help menu / about button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutPasswordManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
            Help.HelpAbout frm = new Help.HelpAbout();
            frm.ShowDialog();
        }
        #endregion

        #endregion

        /// <summary>
        /// Validates data in the text fields and ensure required fields aren't empty
        /// </summary>
        private void ValidateData()
        {
            bool hasWebsite = false;
            bool hasEmail = false;
            bool hasAdditional = false;
            bool hasPassword = false;

            if (wsTextBox.Text.Length < 1)
            {
                MessageBox.Show("1. Enter a website.");
            }
            else
            {
                hasWebsite = true;
            }
            if (emTextBox.Text.Length < 1)
            {
                MessageBox.Show("2. Enter an email.");
            }
            else
            {
                hasEmail = true;
            }
            if (adTextBox.Text.Length < 1)
            {
                MessageBox.Show("3. Enter Additional Information.");
            }
            else
            {
                hasAdditional = true;
            }
            if (pwTextBox.Text.Length < 1)
            {
                MessageBox.Show("4. Enter Existing Password.");
            }
            else
            {
                hasPassword = true;
            }

            if(hasWebsite && hasEmail && hasAdditional && hasPassword)
            {
                MessageBox.Show("Congratulations you have completed this help task.");
            }
        }

        private void HelpExisting_FormClosing(object sender, FormClosingEventArgs e)
        {
            // shutdowns the application if windows is shutting down
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // shutsdown the application when the user clicks the close button
            if (e.CloseReason == CloseReason.UserClosing)
            {
                switch (MessageBox.Show(this, "Are you sure you want to quit?", "Quit Application?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    // Stay on this form
                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                    // exit application
                    case DialogResult.Yes:
                        Application.Exit();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
