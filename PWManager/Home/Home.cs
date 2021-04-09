using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PWManager.Home
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        #region File
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //closes the home screen
            this.Close();
            //create a new instance of the login sceen from file menu
            Login frm = new Login();
            //displays the login screen
            frm.ShowDialog();
        }
        #endregion

        #region Options
        private void createNewPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            //create a new instance of the create sceen from options menu
            Options.Create frm = new Options.Create();
            //displays the create screen
            frm.ShowDialog();
        }

        private void enterExistingPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            //create a new instance of the enter exisiting sceen from options menu
            Options.Existing frm = new Options.Existing();
            //displays the enter exisiting screen
            frm.ShowDialog();
        }

        private void viewAllPasswordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            //create a new instance of the view all sceen from options menu
            Options.ViewAll frm = new Options.ViewAll();
            //displays the view all screen
            frm.ShowDialog();
        }
        #endregion
    }
}
