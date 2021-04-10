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
    public partial class HelpCreate : Form
    {
        public HelpCreate()
        {
            InitializeComponent();
        }

        #region Control Events
        /// <summary>
        /// Handles user interaction with the password button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pwBtn_Click(object sender, EventArgs e)
        {
            pwTextBox.Text = "GeneratedPassword";
        }
        /// <summary>
        /// Handles user interaction with the save button
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
            Close();
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
            Close();
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
            Close();
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
            Close();
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
            Close();
            Options.ViewAll frm = new Options.ViewAll();
            frm.ShowDialog();
        }
        #endregion

        #region Help Menu
        /// <summary>
        /// Handles user interaction with the help menu / enter existing button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterExistingPasswordToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
            Help.HelpExisting frm = new Help.HelpExisting();
            frm.ShowDialog();
        }

        /// <summary>
        /// Handles user interaction with the help menu / about button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutPasswordManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
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

            if (hasWebsite && hasEmail && hasAdditional && hasPassword)
            {
                MessageBox.Show("Congratulations you have completed this help task.");
            }
        }
    }
}
