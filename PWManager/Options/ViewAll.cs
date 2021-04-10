using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        #endregion

        #region Control Events

        /// <summary>
        /// loads the clicked cells password information in the password details screen
        /// </summary>
        /// <param name="sender">the cell that was clicked</param>
        /// <param name="e"></param>
        private void dgvPassword_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPassword.CurrentCell == null) return;

            long PKID = long.Parse(dgvPassword[0, dgvPassword.CurrentCell.RowIndex].Value.ToString());

            //closes this view all form instance
            this.Close();
            //creates a new details form instance and passes the pkid as args
            Details.Details frm = new Details.Details(PKID);
            frm.Show();
        }

        #region File Menu
        /// <summary>
        /// Handles user interaction with the file menu / home button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Home.Home frm = new Home.Home();
            frm.Show();
            this.Close();
        }
        /// <summary>
        /// Handles user interaction with the file menu / logout button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login frm = new Login();
            frm.Show();

            this.Close();
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
