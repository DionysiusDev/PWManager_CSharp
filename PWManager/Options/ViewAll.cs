using PWManager_Model.DLL;
using System;
using System.Data;
using System.Windows.Forms;
using Logging;

namespace PWManager.Options
{
    public partial class ViewAll : Form
    {
        #region Constructors
        /// <summary>
        /// Constructor for the view all form.
        /// </summary>
        public ViewAll()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Events

        /// <summary>
        /// this method is called when the form first loads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewAll_Load(object sender, EventArgs e)
        {
            PopulateGrid();
            SetDGVProperties();
        }

        private void ViewAll_FormClosing(object sender, FormClosingEventArgs e)
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

        #endregion

        #region Control Events

        /// <summary>
        /// Handles user interaction when double clicking a datagrid view cell.
        /// </summary>
        /// <param name="sender">the cell that was clicked</param>
        /// <param name="e"></param>
        private void dgvPassword_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPassword.CurrentCell == null) return;

            try
            {
                long PKID = long.Parse(dgvPassword[0, dgvPassword.CurrentCell.RowIndex].Value.ToString());

                //closes this view all form instance
                Close();
                //creates a new details form instance and passes the pkid as args
                Details.Details frm = new Details.Details(PKID);
                frm.Show();
            }
            catch(Exception ex)
            {
                Logger.LogError("[PWManager.ViewAll] [dgvPassword_CellDoubleClick] Error displaying record " + ex);
            }
        }

        /// <summary>
        /// handles user interaction with clicking the delete button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to delete the selected record?", "Delete Website",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    long PKID = long.Parse(dgvPassword[0, dgvPassword.CurrentCell.RowIndex].Value.ToString());

                    //Use the DeleteRecord method
                    PWManagerContext.DeleteRecord("PasswordInfo", "PwId", PKID.ToString());
                    PopulateGrid();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("[PWManager.ViewAll] [Delete Btn] Error deleting record " + ex);
            }
        }

        #region File Menu
        /// <summary>
        /// Handles user interaction with the file menu / home button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            Home.Home frm = new Home.Home();
            frm.Show();
        }
        /// <summary>
        /// Handles user interaction with the file menu / logout button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
            Login frm = new Login();
            frm.Show();
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
            Create frm = new Create();
            frm.Show();

            this.Close();
        }
        /// <summary>
        /// Handles user interaction with the options menu / enter existing button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterExistingPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Existing frm = new Existing();
            frm.Show();

            this.Close();
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
            Close();
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

        #region Helper Methods

        /// <summary>
        /// Method used for populating data to our data grid view.
        /// </summary>
        private void PopulateGrid()
        {
            PWManager_Model.DLL.PWManagerContext.ConnectionString = Properties.Settings.Default.ConnectionString;
            DataTable dtbPassword = new DataTable();
            dtbPassword = PWManager_Model.DLL.PWManagerContext.GetDataTable("PasswordInfo");
            dgvPassword.DataSource = dtbPassword;
        
            //removes columns from data table
            dtbPassword.Columns.Remove("Email");
            dtbPassword.Columns.Remove("AdditionalInfo");
            dtbPassword.Columns.Remove("Password");
        }

        /// <summary>
        /// Sets properties for the data grid view.
        /// </summary>
        private void SetDGVProperties()
        {
            //hides the id (primary key) from the data grid view
            dgvPassword.Columns[0].Visible = false;

            //sets the website column width
            dgvPassword.Columns[1].Width = dgvPassword.Width;
        }




        #endregion
    }
}
