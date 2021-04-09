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
    public partial class Existing : Form
    {
        public Existing()
        {
            InitializeComponent();
        }

        #region File
        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //closes the existing screen
            this.Close();
            //creates a new instance of the home screen
            Home.Home frmload = new Home.Home();
            //displays the home screen
            frmload.Show();
        }
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Login frm = new Login();
            frm.ShowDialog();
        }

        #endregion

        #region Options
        private void createNewPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //closes the existing screen
            this.Close();
            //creates a new instance of the create screen from the options menu
            Options.Create frmload = new Options.Create();
            //displays the create screen
            frmload.Show();
        }

        private void viewAllPasswordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Options.ViewAll frmload = new Options.ViewAll();
            frmload.Show();
        }
        #endregion
    }
}
