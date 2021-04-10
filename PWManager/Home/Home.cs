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

        #region File Menu
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

        #region Options Menu
        private void createNewPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Options.Create frm = new Options.Create();
            frm.ShowDialog();
        }

        private void enterExistingPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Options.Existing frm = new Options.Existing();
            frm.ShowDialog();
        }

        private void viewAllPasswordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Options.ViewAll frm = new Options.ViewAll();
            frm.ShowDialog();
        }
        #endregion
    }
}
