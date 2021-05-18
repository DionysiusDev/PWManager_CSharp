using PWManager_Model.DLL;
using System;
using System.Data;
using System.Windows.Forms;
using Logging;
using System.Collections.Generic;
using PWManager.SessionUser;
using SecurityAccessLayer;

namespace PWManager.Options
{
    public partial class ViewAll : Form
    {
        #region Variable Declarations
        CurrentUser _CurrentUser = new CurrentUser();

        private DataTable _dtbPassword = null;
        private DataTable _dtbDecrypted = null;
        #endregion

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
            DecryptData();
        }

        private void ViewAll_FormClosing(object sender, FormClosingEventArgs e)
        {
            // shutdowns the application if windows is shutting down
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // shutsdown the application when the user clicks the close button
            if (e.CloseReason == CloseReason.UserClosing && !IsDisposed)
            {
                switch (MessageBox.Show(this, "Are you sure you want to quit?", "Quit Application?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    // Stay on this form
                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                    // exit application
                    case DialogResult.Yes:
                        _CurrentUser.ResetUser();
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

                // creates a new details form instance and passes the pkid as args
                Details.Details frm = new Details.Details(PKID);
                frm.Show();

                // disposes this view all form instance
                Dispose();
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

                    DecryptData();
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
            Dispose();
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
            _CurrentUser.ResetUser();

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
            Dispose();
            Create frm = new Create();
            frm.Show();
        }
        /// <summary>
        /// Handles user interaction with the options menu / enter existing button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterExistingPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
            Existing frm = new Existing();
            frm.Show();
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

        #region Data Decryption

        private void DecryptData()
        {
            string strCurrentUser = CurrentUser._UserName;
            string _TableName = $"{strCurrentUser}Passwords";

            try
            {
                PWManagerContext.ConnectionString = Properties.Settings.Default.ConnectionString;
                _dtbPassword = new DataTable();
                _dtbPassword = PWManagerContext.GetDataTable(_TableName);

            }
            catch (Exception ex)
            {
                Logger.LogError("[View All] [Decrypt Data] Error getting data table " + ex);
            }

            try
            {
                _dtbDecrypted = new DataTable();
                _dtbDecrypted.TableName = _TableName;
                _dtbDecrypted.Clear();

                _dtbDecrypted.Columns.Add("PwId");
                _dtbDecrypted.Columns.Add("Website");
                _dtbDecrypted.Columns.Add("Email");
                _dtbDecrypted.Columns.Add("AdditionalInfo");
                _dtbDecrypted.Columns.Add("Password");

                for (int i = 0; i < _dtbPassword.Rows.Count; i++)
                {
                    DataRow _row = _dtbDecrypted.NewRow();
                    _row["PwId"] = _dtbPassword.Rows[i].ItemArray[0];
                    _row["Website"] = SecurityAccessor.SimpleDecrypt(_dtbPassword.Rows[i].ItemArray[1].ToString());
                    _row["Email"] = SecurityAccessor.SimpleDecrypt(_dtbPassword.Rows[i].ItemArray[2].ToString());
                    _row["AdditionalInfo"] = SecurityAccessor.SimpleDecrypt(_dtbPassword.Rows[i].ItemArray[3].ToString());
                    _row["Password"] = SecurityAccessor.SimpleDecrypt(_dtbPassword.Rows[i].ItemArray[4].ToString());

                    _dtbDecrypted.Rows.Add(_row);
                }

                //removes columns from data table
                _dtbDecrypted.Columns.Remove("Email");
                _dtbDecrypted.Columns.Remove("AdditionalInfo");
                _dtbDecrypted.Columns.Remove("Password");
            }
            catch (Exception ex)
            {
                Logger.LogError("[View All] [Decrypt Data] Error decrypting table " + ex);
            }

            PopulateGrid(_dtbDecrypted);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Method used for populating data to our data grid view.
        /// </summary>
        private void PopulateGrid(DataTable Table)
        {
            dgvPassword.DataSource = Table;

            SetDGVProperties();
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
