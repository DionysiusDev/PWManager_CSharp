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
    public partial class HelpAbout : Form
    {
        public HelpAbout()
        {
            InitializeComponent();
        }

        private void githubLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(githubLbl.Text);
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
            Home.Home frm = new Home.Home();
            frm.Show();
        }
    }
}
