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
        public ViewAll()
        {
            InitializeComponent();
        }

        #region Form Events
        private void ViewAll_Load(object sender, EventArgs e)
        {
            PopulateGrid(); // populate grid
        }
        #endregion

        #region Control Events
        //  this loads the clicked cells password information in the password details screen
        private void Password_DoubleClick(object sender, EventArgs e)
        {
            if (dgvPassword.CurrentCell == null) return;

            long PKID = long.Parse(dgvPassword[0, dgvPassword.CurrentCell.RowIndex].Value.ToString());

            Details.Details frm = new Details.Details(PKID);
            if (frm.ShowDialog() == DialogResult.OK)
            PopulateGrid();
        }

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

            //added for removing columns from data table
            //dtbPassword.Columns.Remove("PwId");

            dgvPassword.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }


        #endregion
    }
}
