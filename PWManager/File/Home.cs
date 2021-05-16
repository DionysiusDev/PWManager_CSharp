using PWManager.SessionUser;
using System;
using System.Windows.Forms;

namespace PWManager.Home
{
    public partial class Home : Form
    {
        CurrentUser _CurrentUser = new CurrentUser();

        #region Constructors
        /// <summary>
        /// Constructor for the home form. This constructor will run when the form loads
        /// </summary>
        public Home()
        {
            InitializeComponent();
        }
        #endregion

        #region Control Events

        #region File Menu
        /// <summary>
        /// Handles user interaction with the file menu / logout button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _CurrentUser.ResetUser();   // resets the user

            Dispose();                  // disposes the home screen
            Login frm = new Login();    //create a new instance of the login sceen from file menu
            frm.ShowDialog();           //displays the login screen
        }
        #endregion

        #region Options Menu
        /// <summary>
        /// Handles user interaction with the options menu / create new button.
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
        /// Handles user interaction with the options menu / enter existing button.
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
        /// Handles user interaction with the options menu / view all button.
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
        /// Handles user interaction with the help menu / create new button.
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
        /// Handles user interaction with the help menu / enter existing button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterExistingPasswordToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Dispose();
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
            Dispose();
            Help.HelpAbout frm = new Help.HelpAbout();
            frm.ShowDialog();
        }
        #endregion

        #endregion

        #region Form Events
        /// <summary>
        /// prompts the user before quitting the application.
        /// Quits the application when the user clicks the forms close button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            // shutdowns the application if windows is shutting down
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // shutsdown the application when the user clicks the close button
            if (e.CloseReason == CloseReason.UserClosing && !IsDisposed)
            {
                switch (MessageBox.Show(this, "Are you sure you want to quit?", "Quit Application?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case DialogResult.No:           // Stay on this form
                        e.Cancel = true;
                        break;
                    case DialogResult.Yes:          // exit application
                        _CurrentUser.ResetUser();   // resets the user
                        Application.Exit();
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
